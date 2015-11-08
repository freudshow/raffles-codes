using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

namespace ClassLibrary1
{
    public class Class1
    {
        [CommandMethod("de")]
        public static void test()
        {
            Document doc=ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database CurrDB = doc.Database;
            using (Transaction tr = CurrDB.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)tr.GetObject(CurrDB.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord btrup = (BlockTableRecord)tr.GetObject(bt["tk"], OpenMode.ForRead);
                        foreach(ObjectId tmpID in btrup)
                        {
                            Autodesk.AutoCAD.DatabaseServices.DBObject tmpObj = tr.GetObject(tmpID, OpenMode.ForRead);
                            if (tmpObj is Autodesk.AutoCAD.DatabaseServices.MText)
                            {
                                Autodesk.AutoCAD.DatabaseServices.MText mText = (MText)tmpObj;
                                mText.Contents = "dfadfafdaf";
                            }
                        }
                        tr.Commit();
                       // bref.ExplodeToOwnerSpace();
                    }
        }
    }
}
