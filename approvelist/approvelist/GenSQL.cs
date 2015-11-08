using System;
using System.Collections.Generic;
using System.Text;

namespace approvelist
{
    public class GenerateReportSQL
    {


        private List<string> _Cols;
        private List<string> _Tabs;
        private List<string> _Alias;
        private Dictionary<string, string> _AliasDict;

        private string ProjTab;
        private string DiscTab;

        private string _SQLHead;
        private List<string> _Conditions;
        private string _ConditionSQL;

        private string _ProjName;
        private string _ProjIDSQL ;

        private string _DiscName;
        private string _DiscIDSQL;

        private string _AppObject;
        private string _AppDate;
        private string _FinalSQL;

        public GenerateReportSQL(string projectName, string disciplineName, string AppObj, bool IsTP)
        {
            _ProjName = projectName;
            _DiscName = disciplineName;
            _SQLHead = @"SELECT COUNT(*) FROM PROJECT_DRAWING_TAB PDT";
            _ProjIDSQL = @"(SELECT PROJ.ID FROM PROJECT_TAB PROJ WHERE PROJ.name='" + projectName + "')";
            _DiscIDSQL = @"(SELECT DISC.ID FROM DISCIPLINE_TAB DISC WHERE DISC.name='" + disciplineName + "')";
            _Conditions = new List<string>();
            AddProjCondition();
            AddDiscCondition();
            _Conditions.Add(@"(PDT.DELETE_FLAG='N')");
            if ( !IsTP )
            {
                _Conditions.Add(@"PDT.DOCTYPE_ID IN ( SELECT DTT.DOCUMENT_TYPE_ID FROM DOCUMENT_TYPE_TAB DTT WHERE DTT.DESCRIPTION  NOT LIKE '%TP%')");
            }
            else
            {
                _Conditions.Add(@"PDT.DOCTYPE_ID IN ( SELECT DTT.DOCUMENT_TYPE_ID FROM DOCUMENT_TYPE_TAB DTT WHERE DTT.DESCRIPTION  LIKE '%TP%')");
            }
            _ConditionSQL = string.Empty;
            _AppObject = AppObj;
            _AppDate = string.Empty;
            _FinalSQL = _SQLHead + _ConditionSQL;
        }

        public GenerateReportSQL(string sqlhead, string projectName, string disciplineName, string AppObj, bool IsTP)
        {
            _ProjName = projectName;
            _DiscName = disciplineName;
            _SQLHead = sqlhead;
            _ProjIDSQL = @"(SELECT PROJ.ID FROM PROJECT_TAB PROJ WHERE PROJ.name='" + projectName + "')";
            _DiscIDSQL = @"(SELECT DISC.ID FROM DISCIPLINE_TAB DISC WHERE DISC.name='" + disciplineName + "')";
            _Conditions = new List<string>();
            AddProjCondition();
            AddDiscCondition();
            _Conditions.Add(@"(PDT.DELETE_FLAG='N')");
            if (!IsTP)
            {
                _Conditions.Add(@"PDT.DOCTYPE_ID IN ( SELECT DTT.DOCUMENT_TYPE_ID FROM DOCUMENT_TYPE_TAB DTT WHERE DTT.DESCRIPTION  NOT LIKE '%TP%')");
            }
            else
            {
                _Conditions.Add(@"PDT.DOCTYPE_ID IN ( SELECT DTT.DOCUMENT_TYPE_ID FROM DOCUMENT_TYPE_TAB DTT WHERE DTT.DESCRIPTION  LIKE '%TP%')");
            }
            _ConditionSQL = string.Empty;
            _AppObject = AppObj;
            _AppDate = string.Empty;
            _FinalSQL = _SQLHead + _ConditionSQL;
        }

        public string SQLHead
        {
            get
            {
                return _SQLHead;
            }
        }

        public string ProjName
        {
            get
            {
                return _ProjName;
            }
            set
            {
                _ProjName = value;
            }
        }

        public string ProjIDSQL
        {
            get
            {
                return _ProjIDSQL;
            }
        }

        public string DiscName
        {
            get
            {
                return _DiscName;
            }
            set
            {
                _DiscName = value;
            }
        }

        public string DiscIDSQL
        {
            get
            {
                return _DiscIDSQL;
            }
        }

        public List<string> Conditions
        {
            get
            {
                return _Conditions;
            }
            set
            {
                _Conditions = value;
            }
        }

        public string ConditionSQL
        {
            get
            {
                if (_Conditions.Count == 0)
                {
                    return string.Empty;
                }
                else
                {
                    if (_Conditions.Count == 1)
                    {
                        _ConditionSQL = " WHERE " + _Conditions[0];
                    }
                    else if (_Conditions.Count > 1)
                    {
                        _ConditionSQL = " WHERE \n";
                        for (int i = 0; i < _Conditions.Count-1; i++ )
                        {
                            _ConditionSQL += _Conditions[i] + " \n AND \n";
                        }
                        _ConditionSQL += _Conditions[_Conditions.Count - 1];
                    }
                    return _ConditionSQL;
                }
            }
            set
            {
                _ConditionSQL = value;
            }
        }

        public string AppDate 
        {
            get
            {
                switch (_AppObject)
                {
                    case "´¬¶«":
                        _AppDate = "CLASS_SUBMISSION_DATE";
                        break;
                    case "´¬¼ì":
                        _AppDate = "OWNER_SUBMISSION_DATE";
                        break;
                    default:
                        break;
                }
                return _AppDate;
            }
        }
            

        public string FinalSQL
        {
            get
            {
                _FinalSQL = SQLHead + ConditionSQL;
                return _FinalSQL;
            }
            set
            {
                _FinalSQL = value;
            }
        }

        public void AddProjCondition()
        {
            if (_ProjName==string.Empty)
            {
                return;
            }
            _Conditions.Add(@"PDT.project_id = (SELECT PRJTAB.ID FROM PROJECT_TAB PRJTAB WHERE PRJTAB.NAME='" + ProjName + "')" );
        }

        public void RemoveProjCondition()
        {
            if (_ProjName == string.Empty)
            {
                return;
            }
            _Conditions.Add(@"PDT.project_id = (SELECT PRJTAB.ID FROM PROJECT_TAB PRJTAB WHERE PRJTAB.NAME='" + ProjName + "')");
        }

        public void AddDiscCondition()
        {
            if (_DiscName == string.Empty)
            {
                return;
            }
            _Conditions.Add(@"PDT.DISCIPLINE_ID = (SELECT DISTAB.ID FROM DISCIPLINE_TAB DISTAB WHERE DISTAB.NAME='" + _DiscName + "')");
        }

        public void RemoveDiscCondition()
        {
            if (_DiscName == string.Empty)
            {
                return;
            }
            _Conditions.Remove(@"PDT.DISCIPLINE_ID = (SELECT DISTAB.ID FROM DISCIPLINE_TAB DISTAB WHERE DISTAB.NAME='" + _DiscName + "')");
        }

        public void AddPlanedAppCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND TO_DATE('" + EndDate + "','YYYY-MM-DD')"
                            );
        }

        public void RemovePlanedAppCondition(string StartDate, string EndDate)
        {
            _Conditions.Remove(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND TO_DATE('" + EndDate + "','YYYY-MM-DD')"
                            );
        }
        //*****************************************************************//
        public void AddActualAppCondition(string StartDate, string EndDate, string AppObject)
        {
            
            _Conditions.Add(@"PDT." + AppDate + @" IS NOT NULL 
                             AND 
                             (TO_DATE(PDT." + AppDate + @",'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND TO_DATE('" + EndDate + "','YYYY-MM-DD')"
                            );
        }
        public void AddDelayAppCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND " + EndDate + ")"
                            );
        }
        public void AddDelayNotAppCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND " + EndDate + ")"
                            );
        }
        public void AddRejectedCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND " + EndDate + ")"
                            );
        }
        public void AddRejectedCCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND " + EndDate + ")"
                            );
        }
        public void AddApprovedCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND " + EndDate + ")"
                            );
        }
        public void AddPlannedApprovedCondition(string StartDate, string EndDate)
        {
            _Conditions.Add(@"PDT.PLANNED_FINISH_DATE IS NOT NULL 
                             AND 
                             (TO_DATE(PDT. PLANNED_FINISH_DATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + StartDate + "','YYYY-MM-DD') AND " + EndDate + ")"
                            );
        }
        //**************************************************************************//



        //public Delegate GetDrawingCondition(string DwgType)
        //{
        //    switch DwgType
        //    {
        //        case 
        //    }
        //}



        public string AddOutCtr(string CtrName)
        {
            string SQLstring = string.Empty;
            return SQLstring;
        }

        public string AddInnerCtr(string InnerName)
        {
            string SQLstring = string.Empty;
            return SQLstring;
        }

    }
}
