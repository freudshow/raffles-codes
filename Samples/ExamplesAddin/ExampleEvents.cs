//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;

namespace Aveva.Pdms.Examples
{
    public class ExampleEvents
    {
        public static void Run()
        {
            //Add delegates
            DbPostElementChange.AddPostCreateElement(DbElementTypeInstance.PIPE, new DbPostElementChange.PostCreateDelegate(PostCreateSubscriber));
            DbPostElementChange.AddPreDeleteElement(DbElementTypeInstance.PIPE, new DbPostElementChange.PreDeleteDelegate(PreDeleteSubscriber));
            DbPostElementChange.AddPostMoveElement(DbElementTypeInstance.PIPE, new DbPostElementChange.PostMoveDelegate(PostMoveSubscriber));
            DbPostElementChange.AddPostAttributeChange(DbAttributeInstance.AREA, new DbPostElementChange.PostAttributeChangeDelegate(PostSetAttSubscriber));
            DbPostElementChange.AddPostAttributeChange(DbAttributeInstance.NAME, new DbPostElementChange.PostAttributeChangeDelegate(PostSetAttSubscriber));
            DbPostElementChange.AddPostRefAttributeChange(DbAttributeInstance.CREF, new DbPostElementChange.PostRefAttributeChangeDelegate(PostSetRefAttSubscriber));

            //Post create
            DbElement newele = Example.Instance.mZone.Create(1, DbElementTypeInstance.PIPE);

            //Pre Delete
            newele.Delete();

            //Post move
            Example.Instance.mPipe.InsertAfter(Example.Instance.mEqui);

            //Post set attribute
            int area = Example.Instance.mEqui.GetInteger(DbAttributeInstance.AREA) + 1;
            Example.Instance.mEqui.SetAttribute(DbAttributeInstance.AREA, area);

            //Post set ref attribute
            Example.Instance.mNozz1.SetAttribute(DbAttributeInstance.CREF, Example.Instance.mBran);
        }

        static private void PostCreateSubscriber(DbElement ele)
        {
            Console.WriteLine("PostCreate");
        }

        static private void PreDeleteSubscriber(DbElement ele)
        {
            Console.WriteLine("PreDelete");
        }

        static private void PostMoveSubscriber(DbElement ele, DbElement oldOwner, int oldpos)
        {
            Console.WriteLine("PostMove");
        }
        
        static private void PostSetAttSubscriber(DbElement ele, DbAttribute att)
        {
            Console.WriteLine("PostSetAtt");
        }

        static private void PostSetRefAttSubscriber(DbElement ele, DbAttribute att, DbElement oldref)
        {
            Console.WriteLine("PostRefAtt");
        }
    }
}