// $Header: /dev/PDMSNet/Shared/Presentation/ReferenceListAddIn/ReferenceListAddIn.cs 5     7/09/04 11:32 James.field $
//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;

namespace Aveva.Pdms.Examples
{
    /// <summary>
    /// ExampleAddin
    /// </summary>
    public class ExamplesAddin : Aveva.ApplicationFramework.IAddin
    {
        private static ServiceManager sServiceManager;
        private static CommandManager sCommandManager;
        private static CommandBarManager sCommandBarManager;

        public ExamplesAddin()
        {
        }

        void Aveva.ApplicationFramework.IAddin.Start(ServiceManager serviceManager)
        {
            sServiceManager = serviceManager;
            sCommandManager = (CommandManager)sServiceManager.GetService(typeof(CommandManager));
            sCommandBarManager = (CommandBarManager)sServiceManager.GetService(typeof(CommandBarManager));

            //Add ExampleCommand to Command Manager
            sCommandManager.Commands.Add(new ExampleCommand());

            //Create example toolbar menu
            CommandBar myToolBar = sCommandBarManager.CommandBars.AddCommandBar("ExampleCommandBar");
            //sCommandBarManager.RootTools.AddButtonTool("ExampleCommand", "ExampleCommand", null, "ExampleCommand");
            //myToolBar.Tools.AddTool("ExampleCommand");
            ComboBoxTool tool = sCommandBarManager.RootTools.AddComboBoxTool("ExampleCommand", "Examples", null, "ExampleCommand");
            tool.SelectedIndex = 0;
            myToolBar.Tools.AddTool("ExampleCommand");
        }

        void Aveva.ApplicationFramework.IAddin.Stop()
        {

        }

        String Aveva.ApplicationFramework.IAddin.Name
        {
            get
            {
                return "ExamplesAddin";
            }
        }
        String Aveva.ApplicationFramework.IAddin.Description
        {
            get
            {
                return "ExamplesAddin";
            }
        }
    }
}
