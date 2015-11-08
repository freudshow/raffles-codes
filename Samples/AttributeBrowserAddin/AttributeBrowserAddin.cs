//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;
// Add additional using statements
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;
using Aveva.Pdms.Shared;
using Aveva.Pdms.Database;

namespace Aveva.Presentation.AttributeBrowserAddin
{
    public class AttributeBrowserAddin : IAddin
    {
        private DockedWindow attributeListWindow;
        private AttributeListControl attributeListControl;
        #region IAddin Members

        public string Description
        {
            get
            {
                return "Provides a simple attribute browser";
            }
        }

        public string Name
        {
            get
            {
                return "AttributeBrowserAddin";
            }
        }

        public void Start(ServiceManager serviceManager)
        {
            // Create Addins Windows
            // Get the WindowManager service
            WindowManager windowManager = (WindowManager)serviceManager.GetService(typeof(WindowManager));
            attributeListControl = new AttributeListControl();
            // Create a docked window to host an AttributeListControl
            attributeListWindow = windowManager.CreateDockedWindow("Aveva.AttributeBrowser.AttributeList", "Attributes", attributeListControl, DockedPosition.Right);
            attributeListWindow.Width = 200;
            // Docked windows created at addin start should ensure their layout is saved between sessions.
            attributeListWindow.SaveLayout = true;

            // Create and register addins commands
            // Get the CommandManager
            CommandManager commandManager = (CommandManager)serviceManager.GetService(typeof(CommandManager));
            ShowAttributeBrowserCommand showCommand = new ShowAttributeBrowserCommand(attributeListWindow);
            commandManager.Commands.Add(showCommand);

            // Add event handler for current element changed event.
            CurrentElement.CurrentElementChanged += new CurrentElementChangedEventHandler(CurrentElement_CurrentElementChanged);
            
            // Get the ResourceManager service.
            ResourceManager resourceManager = (ResourceManager)serviceManager.GetService(typeof(ResourceManager));
            resourceManager.LoadResourceFile("AttributeBrowserAddin");

            // Add a new panel to contain the project name.
            StatusBar statusBar = windowManager.StatusBar;
            StatusBarTextPanel projectNamePanel = statusBar.Panels.AddTextPanel("Aveva.ProjectName", "Project : " + Project.CurrentProject.Name);
            projectNamePanel.SizingMode = PanelSizingMode.Automatic;
            // Get the panel image from the addins resource file.
            projectNamePanel.Image = resourceManager.GetImage("ID_PROJECT_ICON");

            // Load a UIC file for the AttributeBrowser.
            CommandBarManager commandBarManager = (CommandBarManager)serviceManager.GetService(typeof(CommandBarManager));
            commandBarManager.AddUICustomizationFile("AttributeBrowser.uic", "AttributeBrowser");
        }

        void CurrentElement_CurrentElementChanged(object sender, CurrentElementChangedEventArgs e)
        {
            // Set the window title to the name of the element.
            string windowTitle = "Attributes of element " + CurrentElement.Element.GetAsString(DbAttributeInstance.FLNM);
            attributeListWindow.Title = windowTitle;
            // Clear attribute list
            attributeListControl.Clear();
            // Populate the attribute list with attributes of the current element
            foreach (DbAttribute attribute in CurrentElement.Element.GetAttributes())
            {
                attributeListControl.AddAttribute(attribute.Name, CurrentElement.Element.GetAsString(attribute)); 
            }

        }

        public void Stop()
        {
        }

        #endregion
    }
}
