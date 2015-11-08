//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using AxMicrosoft.Office.Interop.VisOcx;
using Visio = Microsoft.Office.Interop.Visio;
using Database = Aveva.Pdms.Database;

namespace Aveva.Pdms.Presentation.CoreConnectionsDiagramAddin
{
	/// <summary>
	/// Summary description for CoreConnectionsDiagram.
	/// </summary>
    public class CoreConnectionsDiagram
    {
        #region "Constants"

        private const string FONT_SIZE = "8pt";
        private const string CELL_PAGE_WIDTH = "PageWidth";
        private const string CELL_PAGE_HEIGHT = "PageHeight";
        private const string CELL_TEXT_BKGND_TRANS = "TextBkgndTrans";

        private const int PAGE_WIDTH_MM = 210;
        private const int PAGE_HEIGHT_MM = 297;

        private const double MARGIN_MM = 5;
		private const double TABLE_HEADER_CELL_HEIGHT_MM = 10;

        private const double SINGLE_WIDTH_MM = 28;
        private const double SINGLE_HEIGHT_MM = 6;
        private const double INITIAL_POS_X_MM = MARGIN_MM;
        private const double INITIAL_POS_Y_MM = PAGE_HEIGHT_MM;

        private const double CABLE_CELL_POS_X_MM = 71;
        private const double CABLE_CELL_WIDTH_MM = 59;

        private const double CORE_CELL_WIDTH_MM = 11;
        private const double CORE_CELL_HEIGHT_MM = 5;
        private const double ELCONN_CELL_WIDTH_MM = 13;
        private const double EQUI_CELL_WIDTH_MM = 30;
        private const double SPACE_BETWEEN_CELLS_MM = 3;

        #endregion "Constants"

        #region "Private members"

        private AxDrawingControl mDwgCtrl = null;

        private double mMargin = 0.0;
		private double mTableHeaderCellWidth = 0.0;

        private double mSingleWidth = 0.0;
        private double mSingleHeight = 0.0;
        private double mInitialPosX = 0.0;
        private double mInitialPosY = 0.0;

        private double mCablePosX = 0.0;
        private double mCableWidth = 0.0;

        private double mCoreWidth = 0.0;
        private double mCoreHeight = 0.0;
        private double mElconnWidth = 0.0;
        private double mEquiWidth = 0.0;
        private double mSpaceBetween = 0.0;

        #endregion "Private members"

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="aDwgCtrl">drawing control on which the core connections diagram should be drawn</param>
        public CoreConnectionsDiagram( AxDrawingControl aDwgCtrl )
        {
            mDwgCtrl = aDwgCtrl;
            Visio.Application _appl = aDwgCtrl.Document.Application;

            // convert constants from millimeters to inches
            mMargin = _appl.ConvertResult( MARGIN_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mTableHeaderCellWidth = _appl.ConvertResult( TABLE_HEADER_CELL_HEIGHT_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );

            mSingleWidth = _appl.ConvertResult( SINGLE_WIDTH_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mSingleHeight = _appl.ConvertResult( SINGLE_HEIGHT_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mInitialPosX = _appl.ConvertResult( INITIAL_POS_X_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mInitialPosY = _appl.ConvertResult( INITIAL_POS_Y_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches ) - mMargin;

            mCablePosX = _appl.ConvertResult( CABLE_CELL_POS_X_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches ) + mMargin;
            mCableWidth = _appl.ConvertResult( CABLE_CELL_WIDTH_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );

            mCoreWidth = _appl.ConvertResult( CORE_CELL_WIDTH_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mCoreHeight = _appl.ConvertResult( CORE_CELL_HEIGHT_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mElconnWidth = _appl.ConvertResult( ELCONN_CELL_WIDTH_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mEquiWidth = _appl.ConvertResult( EQUI_CELL_WIDTH_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
            mSpaceBetween = _appl.ConvertResult( SPACE_BETWEEN_CELLS_MM, Visio.VisUnitCodes.visMillimeters, Visio.VisUnitCodes.visInches );
        }

        /// <summary>
        /// This method is drawing core connections diagram for given cables
        /// </summary>
        /// <param name="aCables">list of cables for which diagram should be drawn</param>
        public void DrawSchema( ArrayList aCables )
        {
            // check if drawing control for schema is set and correct
            if( mDwgCtrl == null || mDwgCtrl.IsDisposed )
                return;

            try
            {
                mDwgCtrl.Document.Application.QueueMarkerEvent( "E_OFF" );

                // prepare first page for drawing
                SetPageProperties( mDwgCtrl.Document.Pages[ 1 ].PageSheet );
                // draw diagram header
                double _posY = DrawDiagramHeader( mDwgCtrl.Document.Pages[ 1 ] );
                // draw header of connections table
                _posY = DrawTableHeader( mDwgCtrl.Document.Pages[ 1 ], _posY - mMargin );
                // set current page
                Visio.Page _currPage = mDwgCtrl.Document.Pages[ 1 ];

                // draw connections diagram for all cables in list
                foreach( Database.DbElement _dbCable in aCables )
                {
                    // get info for current cable
                    CableInfo _cableInfo = new CableInfo( _dbCable );

                    // check if new page is needed and if so create it
                    if( GetCableHeight( _cableInfo ) > _posY - mMargin * 2 )
                    {
                        Visio.Page _newPage = mDwgCtrl.Document.Pages.Add();
                        SetPageProperties( _newPage.PageSheet );
                        _posY = DrawTableHeader( _newPage, _newPage.PageSheet.get_Cells( CELL_PAGE_HEIGHT ).ResultIU - mMargin );
                        _currPage = _newPage;
                    }
                    // draw schema for current cable
                    _posY = DrawCable( _currPage, _cableInfo, _posY - mMargin );
                }
                // clear selection
                mDwgCtrl.Window.DeselectAll();
                // at the end set first page as active
                mDwgCtrl.Window.Page = mDwgCtrl.Document.Pages[ 1 ];
                // mark document as saved
                mDwgCtrl.Document.Saved = true;
            }
            finally
            {
                mDwgCtrl.Document.Application.QueueMarkerEvent( "E_ON" );
            }
        }

        /// <summary>
        /// This method is preparing page for core connections diagram
        /// </summary>
        /// <param name="aPageSheet">Page shape sheet to set proper cells values</param>
        private void SetPageProperties( Visio.Shape aPageSheet )
        {
            //set page width
            aPageSheet.get_Cells( CELL_PAGE_WIDTH ).set_ResultFromIntForce( Visio.VisUnitCodes.visMillimeters, PAGE_WIDTH_MM );
            //set page height
            aPageSheet.get_Cells( CELL_PAGE_HEIGHT ).set_ResultFromIntForce( Visio.VisUnitCodes.visMillimeters, PAGE_HEIGHT_MM );
        }

        /// <summary>
        /// This method is drawing core connections diagram header
        /// </summary>
        /// <param name="aPage">page on which header should be placed</param>
        /// <returns>vertical position after drawing</returns>
        private double DrawDiagramHeader( Visio.Page aPage )
        {
            Visio.Shape _newShape = aPage.DrawRectangle( mInitialPosX, mInitialPosY, mInitialPosX + mSingleWidth * 3, mInitialPosY - mSingleHeight );
            SetTextProperties( _newShape, "TITLE", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 3, mInitialPosY, mInitialPosX + mSingleWidth * 4, mInitialPosY - mSingleHeight );
            SetTextProperties( _newShape, "DRWN BY/DATE", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 4, mInitialPosY, mInitialPosX + mSingleWidth * 5, mInitialPosY - mSingleHeight );
            SetTextProperties( _newShape, string.Empty, FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 5, mInitialPosY, mInitialPosX + mSingleWidth * 7 + mMargin, mInitialPosY - mSingleHeight );
            SetTextProperties( _newShape, "SHEET OF", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX, mInitialPosY - mSingleHeight, mInitialPosX + mSingleWidth * 3, mInitialPosY - mSingleHeight * 3 );
            SetTextProperties( _newShape, string.Empty, FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 3, mInitialPosY - mSingleHeight, mInitialPosX + mSingleWidth * 4, mInitialPosY - mSingleHeight * 2 );
            SetTextProperties( _newShape, "CHKD BY/DATE", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 4, mInitialPosY - mSingleHeight, mInitialPosX + mSingleWidth * 5, mInitialPosY - mSingleHeight * 2 );
            SetTextProperties( _newShape, string.Empty, FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 5, mInitialPosY - mSingleHeight, mInitialPosX + mSingleWidth * 7 + mMargin, mInitialPosY - mSingleHeight * 2 );
            SetTextProperties( _newShape, "SHIP NO:", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 3, mInitialPosY - mSingleHeight * 2, mInitialPosX + mSingleWidth * 4, mInitialPosY - mSingleHeight * 3 );
            SetTextProperties( _newShape, "APPR BY/DATE", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 4, mInitialPosY - mSingleHeight * 2, mInitialPosX + mSingleWidth * 5, mInitialPosY - mSingleHeight * 3 );
            SetTextProperties( _newShape, string.Empty, FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 5, mInitialPosY - mSingleHeight * 2, mInitialPosX + mSingleWidth * 6 + mMargin / 2.0, mInitialPosY - mSingleHeight * 3 );
            SetTextProperties( _newShape, "DRG NO:", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            _newShape = aPage.DrawRectangle( mInitialPosX + mSingleWidth * 6 + mMargin / 2.0, mInitialPosY - mSingleHeight * 2, mInitialPosX + mSingleWidth * 7 + mMargin, mInitialPosY - mSingleHeight * 3 );
            SetTextProperties( _newShape, "REV NO:", FONT_SIZE, (int)Visio.VisCellVals.visHorzLeft );

            return ( mInitialPosY - mSingleHeight * 3 );
        }

        /// <summary>
        /// This method is drawing header of core connections table
        /// </summary>
        /// <param name="aPage">page to drawn on</param>
        /// <param name="aStartPosY">position in vertical in which table header should start</param>
        /// <returns>vertical position after drawing</returns>
        private double DrawTableHeader( Visio.Page aPage, double aStartPosY )
        {
            double _currentX = mMargin;
            double _nextX = mMargin + mEquiWidth;

            Visio.Shape _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Equipment Name", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mElconnWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Conn", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mCoreWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Terminator", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mSpaceBetween;

            aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );

            _currentX = _nextX;
            _nextX = _currentX + mCoreWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Core", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mSpaceBetween;

            aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );

            _currentX = _nextX;
            _nextX = _currentX + mCableWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth / 2.0 );
            SetTextProperties( _newShape, "Cable Name", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY - mTableHeaderCellWidth / 2.0, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Cable Component", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mSpaceBetween;

            aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );

            _currentX = _nextX;
            _nextX = _currentX + mCoreWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Core", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mSpaceBetween;

            aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );

            _currentX = _nextX;
            _nextX = _currentX + mCoreWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Terminator", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mElconnWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Conn", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _currentX = _nextX;
            _nextX = _currentX + mEquiWidth;

            _newShape = aPage.DrawRectangle( _currentX, aStartPosY, _nextX, aStartPosY - mTableHeaderCellWidth );
            SetTextProperties( _newShape, "Equipment Name", FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            return ( aStartPosY - mTableHeaderCellWidth );
        }

        /// <summary>
        /// This method is drawing schema for single cable
        /// </summary>
        /// <param name="aPage">page to draw on</param>
        /// <param name="aCableInfo">information about cable to draw</param>
        /// <param name="aPosY">vertical position in which schema should start</param>
        /// <returns>ertical position after drawing</returns>
        private double DrawCable( Visio.Page aPage, CableInfo aCableInfo, double aPosY )
        {
            if( aCableInfo.CoreNo == 0 )
                aCableInfo.CoreInfo.Add( new CoreInfo( Database.DbElement.GetElement(), aCableInfo ) );

            double _cableHeight = aCableInfo.CoreNo * mCoreHeight;

            Visio.Shape _newShape = aPage.DrawLine( mCablePosX, aPosY - _cableHeight / 2.0, mCablePosX + mCableWidth, aPosY - _cableHeight / 2.0 );
            SetTextProperties( _newShape, aCableInfo.CableName + Environment.NewLine + aCableInfo.ComponentName, FONT_SIZE, (int)Visio.VisCellVals.visHorzCenter );

            _newShape = aPage.DrawLine( mCablePosX, aPosY, mCablePosX, aPosY - _cableHeight );
            _newShape = aPage.DrawLine( mCablePosX + mCableWidth, aPosY, mCablePosX + mCableWidth, aPosY - _cableHeight );

            int _lastBeginEqui = 0, _lastEndEqui = 0, _lastBeginElconn = 0, _lastEndElconn = 0, _current = 0;
            string _lastBeginEquiName = string.Empty, _lastEndEquiName = string.Empty, _lastBeginElconnName = string.Empty, _lastEndElconnName = string.Empty;

            foreach( CoreInfo _core in aCableInfo.CoreInfo )
            {
                //draw core boxes
                _newShape = aPage.DrawRectangle( mCablePosX - mCoreWidth - mSpaceBetween, aPosY - _current * mCoreHeight, mCablePosX - mSpaceBetween, aPosY - ( _current + 1 ) * mCoreHeight );
                SetTextProperties( _newShape, _core.CoreName, "6pt", (int)Visio.VisCellVals.visHorzCenter );
                _newShape = aPage.DrawRectangle( mCablePosX + mCableWidth + mSpaceBetween, aPosY - _current * mCoreHeight, mCablePosX + mCableWidth + mSpaceBetween + mCoreWidth, aPosY - ( _current + 1 ) * mCoreHeight );
                SetTextProperties( _newShape, _core.CoreName, "6pt", (int)Visio.VisCellVals.visHorzCenter );

                //draw pin boxes
                _newShape = aPage.DrawRectangle( mCablePosX - ( mCoreWidth + mSpaceBetween ) * 2, aPosY - _current * mCoreHeight, mCablePosX - mCoreWidth - mSpaceBetween * 2, aPosY - ( _current + 1 ) * mCoreHeight );
                SetTextProperties( _newShape, _core.StartPin, "6pt", (int)Visio.VisCellVals.visHorzCenter );
                _newShape = aPage.DrawRectangle( mCablePosX + mCableWidth + mSpaceBetween * 2 + mCoreWidth, aPosY - _current * mCoreHeight, mCablePosX + mCableWidth + ( mSpaceBetween + mCoreWidth ) * 2, aPosY - ( _current + 1 ) * mCoreHeight );
                SetTextProperties( _newShape, _core.EndPin, "6pt", (int)Visio.VisCellVals.visHorzCenter );

                //check/draw elconns
                if( _core.StartElconn == string.Empty || _lastBeginElconnName != _core.StartElconn )
                {
                    if( _current > 0 )
                    {
                        double _posX = mCablePosX - ( mSpaceBetween + mCoreWidth ) * 2;
                        double _posY = aPosY - _lastBeginElconn * mCoreHeight;
                        //draw elconn
                        _newShape = aPage.DrawRectangle( _posX - mElconnWidth, _posY, _posX, _posY - ( _current - _lastBeginElconn ) * mCoreHeight );
                        SetTextProperties( _newShape, _lastBeginElconnName, "6pt", (int)Visio.VisCellVals.visHorzCenter );
                        _lastBeginElconn = _current;
                    }
                    _lastBeginElconnName = _core.StartElconn;
                }
                if( _core.EndElconn == string.Empty || _lastEndElconnName != _core.EndElconn )
                {
                    if( _current > 0 )
                    {
                        //draw elconn
                        double _posX = mCablePosX + ( mSpaceBetween + mCoreWidth ) * 2 + mCableWidth;
                        double _posY = aPosY - _lastEndElconn * mCoreHeight;
                        //draw elconn
                        _newShape = aPage.DrawRectangle( _posX, _posY, _posX + mElconnWidth, _posY - ( _current - _lastEndElconn ) * mCoreHeight );
                        SetTextProperties( _newShape, _lastEndElconnName, "6pt", (int)Visio.VisCellVals.visHorzCenter );
                        _lastEndElconn = _current;
                    }
                    _lastEndElconnName = _core.EndElconn;
                }

                //check/draw equips
                if( _core.StartEqui == string.Empty || _lastBeginEquiName != _core.StartEqui )
                {
                    if( _current > 0 )
                    {
                        //draw equi
                        double _posX = mCablePosX - ( mSpaceBetween + mCoreWidth ) * 2 - mElconnWidth;
                        double _posY = aPosY - _lastBeginEqui * mCoreHeight;

                        _newShape = aPage.DrawRectangle( _posX - mEquiWidth, _posY, _posX, _posY - ( _current - _lastBeginEqui ) * mCoreHeight );
                        SetTextProperties( _newShape, _lastBeginEquiName, "6pt", (int)Visio.VisCellVals.visHorzCenter );
                        _lastBeginEqui = _current;
                    }
                    _lastBeginEquiName = _core.StartEqui;
                }
                if( _core.EndEqui == string.Empty || _lastEndEquiName != _core.EndEqui )
                {
                    if( _current > 0 )
                    {
                        //draw equi
                        double _posX = mCablePosX + ( mSpaceBetween + mCoreWidth ) * 2 + mCableWidth + mElconnWidth;
                        double _posY = aPosY - _lastEndEqui * mCoreHeight;

                        _newShape = aPage.DrawRectangle( _posX, _posY, _posX + mEquiWidth, _posY - ( _current - _lastEndEqui ) * mCoreHeight );
                        SetTextProperties( _newShape, _lastEndEquiName, "6pt", (int)Visio.VisCellVals.visHorzCenter );
                        _lastEndEqui = _current;
                    }
                    _lastEndEquiName = _core.EndEqui;
                }
                _current++;
            }
            //draw last elconns
            double _pX = mCablePosX - ( mSpaceBetween + mCoreWidth ) * 2;
            double _pY = aPosY - _lastBeginElconn * mCoreHeight;

            _newShape = aPage.DrawRectangle( _pX - mElconnWidth, _pY, _pX, _pY - ( _current - _lastBeginElconn ) * mCoreHeight );
            SetTextProperties( _newShape, _lastBeginElconnName, "6pt", (int)Visio.VisCellVals.visHorzCenter );

            _pX = mCablePosX + ( mSpaceBetween + mCoreWidth ) * 2 + mCableWidth;
            _pY = aPosY - _lastEndElconn * mCoreHeight;

            _newShape = aPage.DrawRectangle( _pX, _pY, _pX + mElconnWidth, _pY - ( _current - _lastEndElconn ) * mCoreHeight );
            SetTextProperties( _newShape, _lastEndElconnName, "6pt", (int)Visio.VisCellVals.visHorzCenter );

            //draw last equips
            _pX = mCablePosX - ( mSpaceBetween + mCoreWidth ) * 2 - mElconnWidth;
            _pY = aPosY - _lastBeginEqui * mCoreHeight;

            _newShape = aPage.DrawRectangle( _pX - mEquiWidth, _pY, _pX, _pY - ( _current - _lastBeginEqui ) * mCoreHeight );
            SetTextProperties( _newShape, _lastBeginEquiName, "6pt", (int)Visio.VisCellVals.visHorzCenter );

            _pX = mCablePosX + ( mSpaceBetween + mCoreWidth ) * 2 + mCableWidth + mElconnWidth;
            _pY = aPosY - _lastEndEqui * mCoreHeight;

            _newShape = aPage.DrawRectangle( _pX, _pY, _pX + mEquiWidth, _pY - ( _current - _lastEndEqui ) * mCoreHeight );
            SetTextProperties( _newShape, _lastEndEquiName, "6pt", (int)Visio.VisCellVals.visHorzCenter );

            return ( aPosY - _cableHeight );
        }

        /// <summary>
        /// This method is setting shape text properties
        /// </summary>
        /// <param name="aShape">shape to work upon</param>
        /// <param name="aText">text to assign to shape</param>
        /// <param name="aFontSize">font size of shape text</param>
        /// <param name="aHorizAlignment">text alignment</param>
        private void SetTextProperties( Visio.Shape aShape, string aText, string aFontSize, int aHorizAlignment )
        {
            // set shape text
            aShape.Text = aText;
            // set font size
            aShape.get_CellsSRC((short)Visio.VisSectionIndices.visSectionCharacter, (short)Visio.VisRowIndices.visRowFirst, (short)Visio.VisCellIndices.visCharacterSize).FormulaForceU = aFontSize;
            // set alignment
            aShape.get_CellsSRC((short)Visio.VisSectionIndices.visSectionParagraph, (short)Visio.VisRowIndices.visRowFirst, (short)Visio.VisCellIndices.visHorzAlign).set_ResultFromIntForce( Visio.VisUnitCodes.visNoCast, aHorizAlignment );
            // set transparent text background
            aShape.get_Cells( CELL_TEXT_BKGND_TRANS ).FormulaForceU = "100%";
        }

        /// <summary>
        /// This method is calculating height of schema for single cable
        /// </summary>
        /// <param name="aCableInfo">information about cable to calculate height</param>
        /// <returns>height of schema for given cable</returns>
        private double GetCableHeight( CableInfo aCableInfo )
        {
            double _height = 0.0;

            if( aCableInfo.CoreNo == 0 )
                _height = mCoreHeight;
            else
                _height = aCableInfo.CoreNo * mCoreHeight;

            return _height;
        }

    }
}
