//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;

namespace Aveva.Pdms.Presentation.CoreConnectionsDiagramAddin
{
    /// <summary>
    /// Summary description for CoreConnectionsDiagramAddin.
    /// </summary>
    public class CoreConnectionsDiagramAddin : IAddin
    {
        #region "Constants"

        public const string CORE_CONN_ADDIN_NAME = "CoreConnectionsDiagram";
        public const string CORE_CONN_TOOLBAR = CORE_CONN_ADDIN_NAME + ".Toolbar";
        public const string CORE_CONN_COMMAND_SUFFIX = ".Command";
        public const string CORE_CONN_TOOLBAR_CMD = CORE_CONN_TOOLBAR + CORE_CONN_COMMAND_SUFFIX;
        public const string CORE_CONN_NEW_CMD = CORE_CONN_ADDIN_NAME + ".New" + CORE_CONN_COMMAND_SUFFIX;

        #endregion "Constants"

        public CoreConnectionsDiagramAddin()
        {
        }

        void IAddin.Start(Aveva.ApplicationFramework.ServiceManager serviceManager)
        {
            // add commands for addin
            CommandManager.Instance.Commands.Add( new CoreConnectionsDiagramToolbar() );
            CommandManager.Instance.Commands.Add( new CoreConnectionsDiagramNew() );
            // load customization file
			String _path = System.IO.Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).ToString();
			_path += "\\CoreConnectionsDiagramAddin.uic";

			CommandBarManager.Instance.AddUICustomizationFile( _path, CORE_CONN_ADDIN_NAME );
        }

        void IAddin.Stop()
        {
        }

        String IAddin.Name
        {
            get{    return CORE_CONN_ADDIN_NAME;     }
        }

        String IAddin.Description
        {
            get{    return CORE_CONN_ADDIN_NAME;     }
        }

    }
}
