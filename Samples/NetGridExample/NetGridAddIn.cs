//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Aveva.ApplicationFramework;
using Aveva.ApplicationFramework.Presentation;

namespace Aveva.Pdms.Examples
{
	/// </summary>
	public class NetGridAddin : Aveva.ApplicationFramework.IAddin
	{
		private DockedWindow mAddinWindow;
		private StateButtonTool mAddinButton;
		private NetGridAddinControl mAddinControl;
		private ServiceManager mServiceManager;
		private CommandBarManager mCommandBarManager;
		private WindowManager mWindowManager;

        public NetGridAddin()
		{
		}

		void Aveva.ApplicationFramework.IAddin.Start(ServiceManager serviceManager)
		{
			mServiceManager	= serviceManager;
			mWindowManager	= (WindowManager)serviceManager.GetService(typeof(WindowManager));
			mCommandBarManager	= (CommandBarManager)serviceManager.GetService(typeof(CommandBarManager));

			mCommandBarManager.RootTools.ToolAdded+=new ToolEventHandler(RootTools_ToolAdded);

			//Create an instance of Addin control
			mAddinControl = new NetGridAddinControl();			

			//Add the Addin
			mAddinWindow = (DockedWindow)mWindowManager.CreateDockedWindow("Grid Control Addin", "Grid Control Addin", mAddinControl, DockedPosition.Left);
			mAddinWindow.Width = 225;

			mAddinWindow.Closing +=new System.ComponentModel.CancelEventHandler(mAddinWindow_Closing);

			//Hide
			mAddinWindow.Hide();

			//Load custom menus
			mCommandBarManager.UILoaded +=new EventHandler(mCommandBarManager_UILoaded);
		}

		void Aveva.ApplicationFramework.IAddin.Stop()
		{

		}		

		String Aveva.ApplicationFramework.IAddin.Name
		{
			get
			{
				return "Addin";
			}
		}
		String Aveva.ApplicationFramework.IAddin.Description
		{
			get
			{
				return "Addin";
			}
		}

		private void ShowAddin_ToolClick(object sender, EventArgs e)
		{
			StateButtonTool tool = (StateButtonTool)sender;
			string[] toolNameSplit = tool.Key.Split('.');
			string toolName = toolNameSplit[toolNameSplit.Length - 1];
			if (toolName.ToUpper(System.Globalization.CultureInfo.InvariantCulture) == "NEW ADDIN")
			{
				if (tool.Checked)
				{
					mAddinWindow.Show();
				}
				else
				{
					mAddinWindow.Hide();
				}
			}
		}

		private void mAddinWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (mAddinButton != null)
			{
				mAddinButton.Checked = false;
			}
		}
		
		private void RootTools_ToolAdded(object sender, ToolEventArgs args)
		{
			ITool tool = mCommandBarManager.RootTools[args.Key];
			string[] toolNameSplit = tool.Key.Split('.');
			string toolName = toolNameSplit[toolNameSplit.Length - 1];

			if (toolName.ToUpper(System.Globalization.CultureInfo.InvariantCulture) == "NEW ADDIN")
			{
				//Addin button
				mAddinButton = (StateButtonTool)tool;
				mAddinButton.Checked = mAddinWindow.Visible;
				mAddinButton.ToolClick +=new EventHandler(ShowAddin_ToolClick);
			}
		}

		private void mCommandBarManager_UILoaded(object sender, EventArgs e)
		{
			//Load menus for selection and header
			String contextMenuKey = "NewAddin.SelectMenu";
			if(mCommandBarManager.RootTools.Contains(contextMenuKey))
			{
				MenuTool menu = (MenuTool)mCommandBarManager.RootTools[contextMenuKey];
				mAddinControl.SelectionMenu = menu;
			}

			contextMenuKey = "NewAddin.HeaderMenu";
			if(mCommandBarManager.RootTools.Contains(contextMenuKey))
			{
				MenuTool menu = (MenuTool)mCommandBarManager.RootTools[contextMenuKey];
				mAddinControl.HeaderMenu = menu;
			}
		}
	}
}
