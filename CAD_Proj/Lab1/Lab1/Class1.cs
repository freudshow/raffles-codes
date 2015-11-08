// (C) Copyright 2002-2005 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to 
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.EditorInput;

[assembly: CommandClass(typeof(ClassLibrary.Class))]

namespace ClassLibrary
{
    /// <summary>
    /// Summary description for Class.
    /// </summary>
    public class Class
    {
        public Class()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //This function returns the ObjectId for the BlockTableRecord called "EmployeeBlock",
        //creating it if necessary.  The block contains three entities - circle, text 
        //and ellipse.
        public ObjectId CreateEmployeeDefinition()
        {
            ObjectId newBtrId = new ObjectId(); //The return value for this function
            Database db = HostApplicationServices.WorkingDatabase; //save some space

            // The "using" keyword used below automatically calls "Dispose" 
            // on the "trans" object.
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //Now, drill into the database and obtain a reference to the BlockTable
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);
                if ((bt.Has("EmployeeBlock")))
                {
                    newBtrId = bt["EmployeeBlock"];
                }
                else
                {
                    Point3d center = new Point3d(10, 10, 0); // convenient declaration...
                    //  Declare and define the entities we want to add:
                    //Circle:
                    Circle circle = new Circle(center, Vector3d.ZAxis, 2);
                    //Text:
                    MText text = new MText();
                    text.Contents = "Earnest Shackleton";
                    text.Location = center;
                    //Ellipse:
                    Ellipse ellipse = new Ellipse(center, Vector3d.ZAxis, new Vector3d(3, 0, 0), 0.5, 0, 0);

                    //Next, create a layer with the helper function, and assign
                    //the layer to our entities.
                    ObjectId empId = CreateLayer();
                    text.LayerId = empId;
                    circle.LayerId = empId;
                    ellipse.LayerId = empId;
                    //Set the color for each entity irrespective of the layer's color.
                    text.ColorIndex = 2;
                    circle.ColorIndex = 1;
                    ellipse.ColorIndex = 3;

                    //Create a new block definition called EmployeeBlock
                    BlockTableRecord newBtr = new BlockTableRecord();
                    newBtr.Name = "EmployeeBlock";
                    newBtrId = bt.Add(newBtr); //Add the block, and set the id as the return value of our function
                    trans.AddNewlyCreatedDBObject(newBtr, true); //Let the transaction know about any object/entity you add to the database!

                    newBtr.AppendEntity(circle); //Append our entities...
                    newBtr.AppendEntity(text);
                    newBtr.AppendEntity(ellipse);
                    trans.AddNewlyCreatedDBObject(circle, true); //Again, let the transaction know about our newly added entities.
                    trans.AddNewlyCreatedDBObject(text, true);
                    trans.AddNewlyCreatedDBObject(ellipse, true);
                }
                trans.Commit(); //All done, no errors?  Go ahead and commit!
            }
            return newBtrId;
        }


        //This function creates a new BlockReference to the "EmployeeBlock" object,
        //and adds it to ModelSpace.
        [CommandMethod("CREATE")]
        public void CreateEmployee()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            // The try/finally idiom is another approach for using, commiting
            // and disposing of transactions.  This technique is demonstrated once here.
            // However, the 'Using' approach will be used from here on.

            Transaction trans = db.TransactionManager.StartTransaction();
            try
            {
                BlockTable bt = (BlockTable)(trans.GetObject(db.BlockTableId, OpenMode.ForWrite));
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                //Create the block reference...use the return from CreateEmployeeDefinition directly!
                BlockReference br = new BlockReference(new Point3d(10, 10, 0), CreateEmployeeDefinition());
                btr.AppendEntity(br); //Add the reference to ModelSpace
                trans.AddNewlyCreatedDBObject(br, true); //Let the transaction know about it
                trans.Commit(); // Commit is always required to indicate success.
            }
            catch (System.Exception ex)
            {
                // The calling, top-level method (such as this) should be used 
                // to report errors that occur even in called methods.
                ed.WriteMessage("Error Creating Employee Block: " + ex.Message);
            }
            finally
            {
                trans.Dispose(); // Manual cleanup necessary in this transaction model
            }
        }
        // This function returns the objectId for the "EmployeeLayer", creating it if necessary.
        private ObjectId CreateLayer()
        {
            ObjectId layerId;
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // open the layer table for read first, to check to see if the requested layer exists
                LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);
                // Check if EmployeeLayer exists...
                if (lt.Has("EmployeeLayer"))
                {
                    layerId = lt["EmployeeLayer"];
                }
                else
                {
                    // if not, create the layer here.
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = "EmployeeLayer"; // Set the layer name
                    ltr.Color = Color.FromColorIndex(ColorMethod.ByAci, 2);
                    // upgrade the open from read to write
                    lt.UpgradeOpen();
                    // now add the new layer
                    layerId = lt.Add(ltr);
                    trans.AddNewlyCreatedDBObject(ltr, true);
                    trans.Commit(); // Only need to commit when we have made a change!
                }
            }
            return layerId;
        }


    }
}