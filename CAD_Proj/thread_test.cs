using System;
using System.Threading;
using System.Diagnostics;

namespace proccesstest
{
	class MyProcess
	{
		// These are the Win32 error code for file not found or access denied.
		const int ERROR_FILE_NOT_FOUND =2;
		const int ERROR_ACCESS_DENIED = 5;

		/// <summary>
		/// Prints a file with a .doc extension.
		/// </summary>
		public void PrintDoc()
		{
			Process myProcess = new Process();
			
			try
			{
				// Get the path that stores user documents.
				string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				Console.WriteLine(myDocumentsPath);
				myProcess.StartInfo.FileName = myDocumentsPath + "\\MyFile.doc"; 
				myProcess.StartInfo.Verb = "Print";
				myProcess.StartInfo.CreateNoWindow = true;
				myProcess.Start();
			}
			catch (System.ComponentModel.Win32Exception err)
			{
				if(err.NativeErrorCode == ERROR_FILE_NOT_FOUND)
				{
					Console.WriteLine(err.Message + "Check the path.");
				} 

				else if (err.NativeErrorCode == ERROR_ACCESS_DENIED)
				{
					// Note that if your word processor might generate exceptions
					// such as this, which are handled first.
					Console.WriteLine(err.Message + 
						". You do not have permission to print this file.");
				}
			}
		}
	}
	
	class Test
	{
		static void Main() 
		{
			// To start a thread using a static thread procedure, use the
			// class name and method name when you create the ThreadStart
			// delegate. Beginning in version 2.0 of the .NET Framework,
			// it is not necessary to create a delegate explicityly. 
			// Specify the name of the method in the Thread constructor, 
			// and the compiler selects the correct delegate. For example:
			//
			// Thread newThread = new Thread(Work.DoWork);
			//
			ThreadStart threadDelegate = new ThreadStart(Work.DoWork);
			Thread newThread = new Thread(threadDelegate);
			newThread.Start();

			// To start a thread using an instance method for the thread 
			// procedure, use the instance variable and method name when 
			// you create the ThreadStart delegate. Beginning in version
			// 2.0 of the .NET Framework, the explicit delegate is not
			// required.
			//
			Work w = new Work();
			w.Data = 42;
			threadDelegate = new ThreadStart(w.DoMoreWork);
			newThread = new Thread(threadDelegate);
			newThread.Start();
			
			MyProcess myProcess = new MyProcess();
			myProcess.PrintDoc();
			//Console.ReadKey();
		}
	}

	class Work 
	{
		public static void DoWork() 
		{
			Console.WriteLine("Static thread procedure."); 
		}
		public int Data;
		public void DoMoreWork() 
		{
			Console.WriteLine("Instance thread procedure. Data={0}", Data); 
		}
	}
}