using Autodesk.AutoCAD.ApplicationServices;

using Autodesk.AutoCAD.DatabaseServices;

using Autodesk.AutoCAD.EditorInput;

using Autodesk.AutoCAD.Runtime;

using Autodesk.AutoCAD.Windows;

using System;


namespace ContextMenuApplication
{

    public class Commands : IExtensionApplication
    {
        public void Initialize()
        {
            CountMenu.Attach();
        }

        public void Terminate()
        {
            CountMenu.Detach();
        }


        [CommandMethod("COUNT", CommandFlags.UsePickSet)]
        static public void CountSelection()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptSelectionResult psr = ed.GetSelection();
            if (psr.Status == PromptStatus.OK)
            {
                ed.WriteMessage("\nSelected {0} entities.", psr.Value.Count);
            }
        }
    }


    public class CountMenu
    {

        private static ContextMenuExtension cme;

        public static void Attach()
        {
            cme = new ContextMenuExtension();
            MenuItem mi = new MenuItem("Count");
            mi.Click += new EventHandler(OnCount);
            cme.MenuItems.Add(mi);
            RXClass rxc = Entity.GetClass(typeof(Entity));
            Application.AddObjectContextMenuExtension(rxc, cme);
        }

        public static void Detach()
        {
            RXClass rxc = Entity.GetClass(typeof(Entity));
            Application.RemoveObjectContextMenuExtension(rxc, cme);
        }

        private static void OnCount(Object o, EventArgs e)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            doc.SendStringToExecute("_.COUNT ", true, false, false);
        }
    }
}