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
    /// This class is representing information about single SCCABLE element
    /// </summary>
    public class CableInfo
    {
        private string mCableName = string.Empty;
        private string mComponentName = string.Empty;
        private string mStartElconn = string.Empty;
        private string mEndElconn = string.Empty;
        private string mStartEqui = string.Empty;
        private string mEndEqui = string.Empty;
        private ArrayList mCores = new ArrayList();

        public CableInfo( Database.DbElement aCable )
        {
            //if given cable is valid in db:
            if( aCable.IsValid )
            {
                //get name of given cable
                mCableName = aCable.GetAsString( Database.DbAttributeInstance.NAMN );

                //get cable component name
                if( aCable.IsAttributeValid( Database.DbAttributeInstance.SPRE ) )
                {
                    Database.DbElement _spref = aCable.GetElement( Database.DbAttributeInstance.SPRE );
                    if( _spref.IsValid )
                        mComponentName = _spref.GetAsString( Database.DbAttributeInstance.NAMN );
                }

                Database.DbElement _staref = aCable.GetElement( Database.DbAttributeInstance.STAREF );
                if( _staref.IsValid )
                {
                    if( _staref.GetElementType() == Database.DbElementTypeInstance.SCELCONNECTION )
                    {
                        mStartElconn = MiscUtilities.GetElemPresName( _staref );
                        mStartEqui = MiscUtilities.GetElemPresName( _staref.Owner );
                    }
                    else
                        mStartEqui = MiscUtilities.GetElemPresName( _staref );
                }

                Database.DbElement _endref = aCable.GetElement( Database.DbAttributeInstance.ENDREF );
                if( _endref.IsValid )
                {
                    if( _endref.GetElementType() == Database.DbElementTypeInstance.SCELCONNECTION )
                    {
                        mEndElconn = MiscUtilities.GetElemPresName( _endref );
                        mEndEqui = MiscUtilities.GetElemPresName( _endref.Owner );
                    }
                    else
                        mEndEqui = MiscUtilities.GetElemPresName( _endref );
                }

                Database.DbElement [] _cores = aCable.Members( Database.DbElementTypeInstance.SCCORE );

                //get informaton about cores
                foreach( Database.DbElement _core in _cores )
                {
                    CoreInfo _coreInf = new CoreInfo( _core, this );
                    mCores.Add( _coreInf );
                }
            }
        }

        /// <summary>
        /// gets name of represented cable
        /// </summary>
        public string CableName
        {
            get{    return mCableName;      }
        }

        /// <summary>
        /// gets name of component reference
        /// </summary>
        public string ComponentName
        {
            get{    return mComponentName;      }
        }

        /// <summary>
        /// gets array of object representing informations about cable cores
        /// </summary>
        public ArrayList CoreInfo
        {
            get{    return mCores;      }
        }

        /// <summary>
        /// gets number of cable cores
        /// </summary>
        public int CoreNo
        {
            get{    return mCores.Count;    }
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
