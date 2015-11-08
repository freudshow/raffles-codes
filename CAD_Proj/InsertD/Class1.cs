using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System;

namespace AppMenus
{

    public class ExtApp : IExtensionApplication
    {
        // String constants
        const string appText = "Browse Photosynth";
        const string appDesc = "Browse the Photosynth site and import point " + "clouds into AutoCAD.";
        const string smallFile = "Browser-16x16.ico";
        const string largeFile = "Browser-32x32.ico";
        const string bpCmd = "_.BP";

        public void Initialize()
        {
            // We defer the creation of our Application Menu to when
            // the menu is next accessed
            ComponentManager.ApplicationMenu.Opening += new EventHandler<EventArgs>(ApplicationMenu_Opening);
            // We defer the creation of our Quick Access Toolbar item
            // to when the application is next idle
            Application.Idle += new EventHandler(Application_OnIdle);
        }

        public void Terminate()
        {
            // Assuming these events have fired, they have already
            // been removed
            ComponentManager.ApplicationMenu.Opening -= new EventHandler<EventArgs>(ApplicationMenu_Opening);
            Application.Idle -= new EventHandler(Application_OnIdle);
        }

        void Application_OnIdle(object sender, EventArgs e)
        {
            // Remove the event when it is fired
            Application.Idle -= new EventHandler(Application_OnIdle);
            // Add our Quick Access Toolbar item
            AddQuickAccessToolbarItem();
        }

        void ApplicationMenu_Opening(object sender, EventArgs e)
        {
            // Remove the event when it is fired
            ComponentManager.ApplicationMenu.Opening -= new EventHandler<EventArgs>(ApplicationMenu_Opening);
            // Add our Application Menu
            AddApplicationMenu();
        }

        private void AddApplicationMenu()
        {
            ApplicationMenu menu = ComponentManager.ApplicationMenu;
            if (menu != null && menu.MenuContent != null)
            {
                // Create our Application Menu Item
                ApplicationMenuItem mi = new ApplicationMenuItem();
                mi.Text = appText;
                mi.Description = appDesc;
                mi.LargeImage = GetIcon(largeFile);
                // Attach the handler to fire out command
                mi.CommandHandler = new AutoCADCommandHandler(bpCmd);
                // Add it to the menu content
                menu.MenuContent.Items.Add(mi);
            }
        }

        private void AddQuickAccessToolbarItem()
        {
            Autodesk.Windows.ToolBars.QuickAccessToolBarSource qat = ComponentManager.QuickAccessToolBar;
            if (qat != null)
            {
                // Create our Ribbon Button
                RibbonButton rb = new RibbonButton();
                rb.Text = appText;
                rb.Description = appDesc;
                rb.Image = GetIcon(smallFile);
                // Attach the handler to fire out command
                rb.CommandHandler = new AutoCADCommandHandler(bpCmd);
                // Add it to the Quick Access Toolbar
                qat.AddStandardItem(rb);
            }
        }

        private System.Windows.Media.ImageSource GetIcon(string ico)
        {

            // We'll look for our icons in the folder of the assembly

            // (we could also use a resources, of course)



            string path =

              Path.GetDirectoryName(

                Assembly.GetExecutingAssembly().Location

              );



            // Check our .ico file exists



            string fileName = path + "\\" + ico;

            if (File.Exists(fileName))
            {

                // Get access to it via a stream



                Stream fs =

                  new FileStream(

                    fileName,

                    FileMode.Open,

                    FileAccess.Read,

                    FileShare.Read

                  );

                using (fs)
                {

                    // Decode the contents and return them



                    IconBitmapDecoder dec =

                      new IconBitmapDecoder(

                        fs,

                        BitmapCreateOptions.PreservePixelFormat,

                        BitmapCacheOption.Default

                      );

                    return dec.Frames[0];

                }

            }

            return null;

        }

    }



    // A class to fire commands to AutoCAD



    public class AutoCADCommandHandler: System.Windows.Input.ICommand
    {
        private string _command = "";
        public AutoCADCommandHandler(string cmd)
        {
            _command = cmd;
        }

        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!String.IsNullOrEmpty(_command))
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute(_command + " ", false, false, false);
            }
        }
    }
}