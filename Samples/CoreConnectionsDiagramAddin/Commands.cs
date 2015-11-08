//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using AxMicrosoft.Office.Interop.VisOcx;
using Visio = Microsoft.Office.Interop.Visio;
using Aveva.Diagrams;
using Database = Aveva.Pdms.Database;
using Aveva.Diagrams.UI;

namespace Aveva.Pdms.Presentation.CoreConnectionsDiagramAddin
{
    /// <summary>
    /// command class for Core Connections Diagram Addin toolbar
    /// </summary>
    public class CoreConnectionsDiagramToolbar : Command
    {
        public CoreConnectionsDiagramToolbar()
        {
            base.Key = CoreConnectionsDiagramAddin.CORE_CONN_TOOLBAR_CMD;
            base.Visible = false;
        }
    }

    /// <summary>
    /// class representing command to create new core connections diagram
    /// </summary>
    public class CoreConnectionsDiagramNew : Command
    {
        private bool mExecuting = false;

        public CoreConnectionsDiagramNew()
        {
            this.Key = CoreConnectionsDiagramAddin.CORE_CONN_NEW_CMD;
            this.Visible = true;
        }

        public override void Execute()
        {
            // if is already executing do nothing
            if( mExecuting )
                return;

            mExecuting = true;

            try
            {
                AxDrawingControl _activeCtrl = null;

                //get drawing for which the core connections diagram should be generated
                DrawingControlWindow _wnd = diagApp.GetCurrentDrawingControl();
                if( _wnd != null )
                    _activeCtrl = _wnd.mVisioDrawingControl.ActiveXDrawingControl;

                ArrayList _cableList = new ArrayList();

                // if drawing is found
                if( _activeCtrl != null )
                {
                    // get active window
                    Visio.Window _actWindow = _activeCtrl.Document.Application.ActiveWindow;
                    // get selection
                    Visio.Selection _selection = _actWindow.Selection;
                    _selection.IterationMode &= ~(int)Visio.VisSelectMode.visSelModeSkipSub;

                    // get cables from selection or from all document (if nothing is selected)
                    if( _selection.Count > 0 )
                    {
                        foreach( Visio.Shape _shape in _selection )
                            CheckAddShape( _shape, _cableList );
                    }
                    else
                    {
                        foreach( Visio.Page _page in ( (AxDrawingControl)_activeCtrl ).Document.Pages )
                            foreach( Visio.Shape _shape in _page.Shapes )
                                CheckAddShape( _shape, _cableList );
                    }
                }

                if( _cableList.Count == 0 )
                {
                    System.Windows.Forms.MessageBox.Show( "Cannot get cables for Core Connections Diagram!", "Core Connections Diagram" );
                    return;
                }

                //create new empty diagram
                DrawingControlWindow _dwgInstance = new DrawingControlWindow();
                AxDrawingControl _newDwgCtrl = null;

                if( _dwgInstance != null )
                    _newDwgCtrl = _dwgInstance.NewDiagram();

                if( _newDwgCtrl != null )
                {
                    CoreConnectionsDiagram _diag = new CoreConnectionsDiagram( _newDwgCtrl );
                    _diag.DrawSchema( _cableList );
                    DrawingManager.SaveNewDrawingToDb( _dwgInstance );
                }
            }
            catch( Exception _ex )
            {
                Console.WriteLine( _ex.ToString() );
            }
            finally
            {
                mExecuting = false;
            }
        }

        /// <summary>
        /// This method is checking if given shape is cable shape or is group containing cable shapes
        /// and adds found cables to given list
        /// </summary>
        /// <param name="aShape">shape to examine</param>
        /// <param name="aList">list of shapes to update</param>
        private void CheckAddShape( Visio.Shape aShape, ArrayList aList )
        {
            ArrayList _vmdShapes = VmdShape.GetVmdShapes( aShape );

            foreach( Visio.Shape _vmdShape in _vmdShapes )
            {
                VmdElement _vmdElem = VmdObject.GetElement( _vmdShape );

            // try only for defined shapes
                if( _vmdElem == null || ! VmdElement.IsDefined( _vmdShape ) )
                    continue;

                // if shape is a cable shape check if it is valid in db
                if( _vmdElem is VmdCableElement )
                {
                    Database.DbElement _dbElem = VmdElement.FindRelatedDbElement( _vmdShape );
                    CheckAddDbCable( _dbElem, aList );
                }
            }
        } //checkAddShape

        private void CheckAddDbCable( Database.DbElement aDbCable, ArrayList aList )
        {
            if( aDbCable.GetElementType() == Database.DbElementTypeInstance.SCCABLE )
            {
                if( aDbCable.IsValid && aList.IndexOf( aDbCable ) == -1 )
                    aList.Add( aDbCable );
            }
            else if( aDbCable.GetElementType() == Database.DbElementTypeInstance.SCMCABLE )
            {
                Database.DbElement[] _cables = aDbCable.Members( Database.DbElementTypeInstance.SCCABLE );
                if( _cables != null )
                {
                    foreach( Database.DbElement _cable in _cables )
                    {
                        if( _cable.IsValid && aList.IndexOf( _cable ) == -1 )
                            aList.Add( _cable );
                    }
                }
            }

        } //CheckAddDbCable

    }
}
