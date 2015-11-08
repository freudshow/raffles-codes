//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using Database = Aveva.Pdms.Database;
using SchematicUtilities;

namespace Aveva.Pdms.Presentation.CoreConnectionsDiagramAddin
{
    /// <summary>
    /// This class is representing information about single SCCORE element
    /// </summary>
    public class CoreInfo
    {
        private const string PIN_TEMPLATE = "T_{0}";
        private const string UNSET_TEXT = "";

        #region "Private members"

        private string mCoreName = string.Empty;
        private string mStartPin = string.Empty;
        private string mEndPin = string.Empty;
        private string mStartElconn = string.Empty;
        private string mEndElconn = string.Empty;
        private string mStartEqui = string.Empty;
        private string mEndEqui = string.Empty;

        #endregion "Private members"

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="aCore">database core element to get information from</param>
        public CoreInfo( Database.DbElement aCore, CableInfo aCableInfo )
        {
            mStartElconn = aCableInfo.StartElconn;
            mStartEqui = aCableInfo.StartEqui;
            mEndElconn = aCableInfo.EndElconn;
            mEndEqui = aCableInfo.EndEqui;

            if( aCore.IsValid )
            {
                // get core name
                mCoreName = aCore.GetAsString( Database.DbAttributeInstance.NAMN );

                // get information about elements connected to core head
                Database.DbElement _elconn = aCore.GetElement( Database.DbAttributeInstance.STAREF );
                if( _elconn.IsValid )
                {
                    mStartElconn = MiscUtilities.GetElemPresName( _elconn );
                    mStartEqui = MiscUtilities.GetElemPresName( _elconn.Owner );
                }

                int _point = aCore.GetInteger( Database.DbAttributeInstance.STAPOI );
                if( _point == 0 )
                    mStartPin = UNSET_TEXT;
                else
                    mStartPin = string.Format( PIN_TEMPLATE, _point.ToString() );

                // get information about elements connected to core tail
                _elconn = aCore.GetElement( Database.DbAttributeInstance.ENDREF );
                if( _elconn.IsValid )
                {
                    mEndElconn = MiscUtilities.GetElemPresName( _elconn );
                    mEndEqui = MiscUtilities.GetElemPresName( _elconn.Owner );
                }

                _point = aCore.GetInteger( Database.DbAttributeInstance.ENDPOI );
                if( _point == 0 )
                    mEndPin = UNSET_TEXT;
                else
                    mEndPin = string.Format( PIN_TEMPLATE, _point.ToString() );
            }
        }

        /// <summary>
        /// gets name of represented core
        /// </summary>
        public string CoreName
        {
            get{    return mCoreName;   }
        }

        /// <summary>
        /// gets string expression of head pin
        /// </summary>
        public string StartPin
        {
            get{    return mStartPin;   }
        }

        /// <summary>
        /// gets string expression of tail pin
        /// </summary>
        public string EndPin
        {
            get{    return mEndPin;     }
        }

        /// <summary>
        /// gets name of head elconn
        /// </summary>
        public string StartElconn
        {
            get{    return mStartElconn;    }
        }

        /// <summary>
        /// gets name of tail elconn
        /// </summary>
        public string EndElconn
        {
            get{    return mEndElconn;      }
        }

        /// <summary>
        /// gets name of head equipment
        /// </summary>
        public string StartEqui
        {
            get{    return mStartEqui;      }
        }

        /// <summary>
        /// gets name of tail equipment
        /// </summary>
        public string EndEqui
        {
            get{    return mEndEqui;    }
        }

    }
}
