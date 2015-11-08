//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;

using Aveva.Pdms.Database;
using Aveva.Pdms.Utilities.Messaging;
using Aveva.PDMS.Database.Filters;
using Aveva.Pdms.Geometry;
using Aveva.Pdms.Utilities.Undo;
using Aveva.Pdms.Shared;

namespace Aveva.Pdms.Examples
{
    public class ExampleUndo
    {
        public static void Run()
        {
            // Goto equi
            CurrentElement.Element = Example.Instance.mEqui;

            // set a a mark and then change a database value
            UndoTransaction trans = UndoTransaction.GetUndoTransaction();
            trans.StartTransaction("My Transaction");
            String s1 = Example.Instance.mEqui.GetString(DbAttributeInstance.DESC);
            String s2 = String.Concat(s1, "x");
            Example.Instance.mEqui.SetAttribute(DbAttributeInstance.DESC, s2);
            trans.EndTransaction();
            // now restore old value
            UndoTransaction.PerformUndo();

            String s3 = Example.Instance.mEqui.GetString(DbAttributeInstance.DESC);

            // restore new values
            UndoTransaction.PerformRedo();
            String s4 = Example.Instance.mEqui.GetString(DbAttributeInstance.DESC);

            // Add in a subscriber
            ExampleUndoSubscriber subscriber = new ExampleUndoSubscriber();
            UndoCaretaker.RegisterUndoSubscriber(subscriber);

            // set an initial value
            s1 = "Initial setting";
            s2 = "New setting";
            ExampleUndoSubscriber.val = s1;

            // Now do a transaction
            trans.StartTransaction("My Transaction");
            ExampleUndoSubscriber.val = s2;
            trans.EndTransaction();

            // restore old value
            UndoTransaction.PerformUndo();

            // restore new value
            UndoTransaction.PerformRedo();
            UndoCaretaker.RemoveUndoSubscriber(subscriber);
        }
    }

    public class ExampleUndoSubscriber : UndoSubscriber
    {

        public static string val;
        public override object GetBeginState()
        {
            return val;
        }

        /// <summary>
        /// GetStartState() will be called on end transaction. 
        /// It should return the current state.
        /// </summary>
        public override object GetEndState()
        {
            return val;
        }

        /// <summary>
        /// RestoreStartState() will be called on undo of that transaction. 
        /// It should restore the passed state.
        /// </summary>
        public override void RestoreBeginState(object obj)
        {
            val = (string)obj;
        }

        /// <summary>
        /// RestoreEndState() will be called on undo of that transaction. 
        /// It should restore the passed state.
        /// </summary>
        public override void RestoreEndState(object obj)
        {
            val = (string)obj;
        }
    }
}