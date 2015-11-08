// $Header: /dev/PDMSNet/Shared/DrawListCommands/AddCommand.cs 9     7/01/05 15:31 David.stevenson $
//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections;
using System.Windows.Forms;
using Aveva.ApplicationFramework.Presentation;

namespace Aveva.Pdms.Examples
{
    /// <summary>
    /// Example list command
    /// </summary>
    public class ExampleCommand : Aveva.ApplicationFramework.Presentation.Command
    {
        public ExampleCommand()
        {
            Key = "ExampleCommand";
            this.List.Add("Attributes");
            this.List.Add("PseudoAttributes");
            this.List.Add("CE");
            this.List.Add("Collections");
            this.List.Add("Database");
            this.List.Add("Element");
            this.List.Add("Events");
            this.List.Add("Filters");
            this.List.Add("MDB");
            this.List.Add("NameTable");
            this.List.Add("Project");
            this.List.Add("Spatial");
            this.List.Add("Clasher");
            this.List.Add("TreeNavigator");
            this.List.Add("Undo");
            this.List.Add("UserChanges");
            this.Value = "Attributes";
        }

        public override void Execute()
        {
            //ExamplesForm f = new ExamplesForm();
            //f.Show();
            string example = (string)this.Value;
            try
            {
                Example.Instance.Create();
                if (example == "Attributes")
                {
                    ExampleAttributes.Run();
                }
                else if (example == "PseudoAttributes")
                {
                    ExamplePseudoAttribute.Run();
                }
                else if (example == "CE")
                {
                    ExampleCE.Run();
                }
                else if (example == "Collections")
                {
                    ExampleCollections.Run();
                }
                else if (example == "Database")
                {
                    ExampleDatabase.Run();
                }
                else if (example == "Element")
                {
                    ExampleElement.Run();
                }
                else if (example == "Events")
                {
                    ExampleEvents.Run();
                }
                else if (example == "Filters")
                {
                    ExampleFilters.Run();
                }
                else if (example == "MDB")
                {
                    ExampleMDB.Run();
                }
                else if (example == "NameTable")
                {
                    ExampleNameTable.Run();
                }
                else if (example == "Project")
                {
                    ExampleProject.Run();
                }
                else if (example == "Spatial")
                {
                    ExampleSpatial.Run();
                }
                else if (example == "Clasher")
                {
                    ExampleClasher.Run();
                }
                else if (example == "TreeNavigator")
                {
                    ExampleTreeNavigator.Run();
                }
                else if (example == "Undo")
                {
                    ExampleUndo.Run();
                }
                else if (example == "UserChanges")
                {
                    ExampleUserChanges.Run();
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
