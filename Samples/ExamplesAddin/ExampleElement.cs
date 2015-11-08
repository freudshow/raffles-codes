//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;

namespace Aveva.Pdms.Examples
{
    public class ExampleElement
    {
        public static void Run()
        {
            //Get element type
            int hash = DbElementTypeInstance.EQUIPMENT.GetHashCode();
            DbElementType type = DbElementType.GetElementType(hash);

            // Get system attributes
            DbAttribute[] atts = DbElementTypeInstance.EQUIPMENT.SystemAttributes();
            int size = atts.Length;

            //Construct equi1
            DbElement equi1 = DbElement.GetElement("/ExampleEqui");

            //Create equi2 after equi1
            DbElement equi2 = equi1.CreateAfter(DbElementTypeInstance.EQUIPMENT);

            //Copy equi1 to equi2
            equi2.Copy(equi1);

            //Copy hierarchy
            DbCopyOption options = new DbCopyOption();
            options.FromName = "ExampleEqui";
            options.ToName = "equi2";
            options.Rename = true;
            equi2.CopyHierarchy(equi1, options);
            DbElement first = equi2.FirstMember();

            //Copy after last
            equi1.InsertAfterLast(Example.Instance.mZone);

            //Branch to head tube
            DbElement headTube = Example.Instance.mBran.FirstMember();
            string headTubeName = headTube.GetString(DbAttributeInstance.NAME);

            //Next Prev
            DbElement nextElement = headTube.Next();
            DbElement prevElement = nextElement.Previous;

            //First Member of given type
            DbElement nozz1 = Example.Instance.mEqui.FirstMember(DbElementTypeInstance.NOZZLE);

            //Next element of given type
            DbElement nozz2 = nozz1.Next(DbElementTypeInstance.NOZZLE);

            //Get Members
            DbElement[] members = Example.Instance.mBran.Members();

            //Get Members of given type
            DbElement[] nozzles = Example.Instance.mEqui.Members(DbElementTypeInstance.NOZZLE);

            //Get nth Member
            DbElement mem = Example.Instance.mEqui.Member(2);

            //Expressions
            DbElement[] eles;
            eles = Example.Instance.mEqui.GetElementArray(DbAttribute.GetDbAttribute("MEMB"), DbElementType.GetElementType("NOZZ"));
            nozz1 = eles[1];
            DbExpression expr1 = DbExpression.Parse("HEIGHT OF PREV * 2");
            string val = expr1.ToString();
            double dval;
            DbAttributeUnit units = DbAttributeUnit.DIST;
            dval = nozz1.EvaluateDouble(expr1, units);
            DbExpression expr2 = DbExpression.Parse("12");
            dval = nozz1.EvaluateDouble(expr2, units);

            //Rules
            DbExpression expr = DbExpression.Parse("HEIGHT * 2.0");
            DbRuleStatus status = DbRuleStatus.DYNAMIC;
            DbExpressionType etype = DbExpressionType.REAL;
            DbRule rule = DbRule.CreateDbRule(expr, status, etype);
            Example.Instance.mCyli.SetRule(DbAttribute.GetDbAttribute("DIAM"), rule);
            DbRule rule1 = Example.Instance.mCyli.GetRule(DbAttribute.GetDbAttribute("DIAM"));
            string text = rule1.ToString();

            //Delete/Exists Rule
            Example.Instance.mCyli.DeleteRule(DbAttribute.GetDbAttribute("DIAM"));
            bool exists = Example.Instance.mCyli.ExistRule(DbAttribute.GetDbAttribute("DIAM"));
            Example.Instance.mCyli.SetRule(DbAttribute.GetDbAttribute("DIAM"), rule1);
            exists = Example.Instance.mCyli.ExistRule(DbAttribute.GetDbAttribute("DIAM"));

            //Change attribute
            Example.Instance.mCyli.SetAttribute(DbAttribute.GetDbAttribute("DIAM"), 1000.0F);
            double diam = Example.Instance.mCyli.GetDouble(DbAttribute.GetDbAttribute("DIAM"));

            //verify rule
            bool diff = Example.Instance.mCyli.VerifyRule(DbAttribute.GetDbAttribute("DIAM"));

            //execute rule
            Example.Instance.mCyli.ExecuteRule(DbAttribute.GetDbAttribute("DIAM"));
            diam = Example.Instance.mCyli.GetDouble(DbAttribute.GetDbAttribute("DIAM"));

            //verify rule again
            diff = Example.Instance.mCyli.VerifyRule(DbAttribute.GetDbAttribute("DIAM"));

            //change some attributes
            Example.Instance.mCyli.SetAttribute(DbAttribute.GetDbAttribute("DIAM"), 1000.0F);
            diam = Example.Instance.mCyli.GetDouble(DbAttribute.GetDbAttribute("DIAM"));

            //Now execute all rules under equi
            Example.Instance.mEqui.ExecuteAllRules();
            diam = Example.Instance.mCyli.GetDouble(DbAttribute.GetDbAttribute("DIAM"));

            //Propagate rules
            Example.Instance.mCyli.SetAttribute(DbAttribute.GetDbAttribute("DIAM"), 1000.0F);
            Example.Instance.mCyli.PropagateRules(DbAttribute.GetDbAttribute("DIAM"));

            //Claim/Release
            Example.Instance.mEqui.Claim();
            bool claimed = Example.Instance.mEqui.GetBool(DbAttribute.GetDbAttribute("LCLM"));

            //release equi
            MDB.CurrentMDB.SaveWork("Save Example");
            Example.Instance.mEqui.Release();
            claimed = Example.Instance.mEqui.GetBool(DbAttribute.GetDbAttribute("LCLM"));

            //Claim hierarchy
            try
            {
                Example.Instance.mEqui.ClaimHierarchy();
            }
            catch (PdmsException ex)
            {

            }
            claimed = Example.Instance.mEqui.GetBool(DbAttribute.GetDbAttribute("LCLMH"));

            //release all equi
            Example.Instance.mEqui.ReleaseHierarchy();
            claimed = Example.Instance.mEqui.GetBool(DbAttribute.GetDbAttribute("LCLM"));

            //change type
            string stype = Example.Instance.mTee.GetElementType().ToString();
            Example.Instance.mTee.ChangeType(DbElementType.GetElementType("OLET"));
            stype = Example.Instance.mTee.GetElementType().ToString();

            //UDA at default
            string special = "Test UDA";
            DbAttribute uda = DbAttribute.GetDbAttribute(":SPECIAL");
			if (uda != null)
			{
				Example.Instance.mElbo.SetAttribute(uda, special);
				special = Example.Instance.mElbo.GetString(uda);
				bool dflt = Example.Instance.mElbo.AtDefault(uda);

				//is one element above another in the hierarchy
				bool result = Example.Instance.mSite.IsDescendant(Example.Instance.mZone);
				result = Example.Instance.mZone.IsDescendant(Example.Instance.mSite);

				//Set uda to default
				Example.Instance.mElbo.SetAttributeDefault(uda);
				special = Example.Instance.mElbo.GetString(uda);
				dflt = Example.Instance.mElbo.AtDefault(uda);
			}
        }
    }
}
