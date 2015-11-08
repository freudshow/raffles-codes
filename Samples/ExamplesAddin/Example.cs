//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;

using Aveva.Pdms.Database;

namespace Aveva.Pdms.Examples
{
    class Example
    {
        public static Example Instance = new Example();

        public  DbElement mWorld;
        public  DbElement mSite;
        public  DbElement mZone;
        public  DbElement mPipe;
        public  DbElement mBran;
        public  DbElement mGask;
        public  DbElement mElbo;
        public  DbElement mEqui;
        public  DbElement mNozz1;
        public  DbElement mNozz2;
        public  DbElement mCyli;
        public  DbElement mTee;
        public  DbElement mBox;

        private Example()
        {
        }

        public void Create()
        {
            //Delete Example Site if it already exists
            DbElement site = DbElement.GetElement("/ExampleSite");
            if (site.IsValid)
                site.Delete();

            //Get world element
            mWorld = DbElement.GetElement("/*");

            //ExampleSite
            mSite = mWorld.Create(0, DbElementTypeInstance.SITE);
            mSite.SetAttribute(DbAttributeInstance.NAME, "/ExampleSite");

            //ExampleZone
            mZone = mSite.Create(0, DbElementTypeInstance.ZONE);
            mZone.SetAttribute(DbAttributeInstance.NAME, "/ExampleZone");

            //ExampleEqui
            mEqui = mZone.Create(0, DbElementTypeInstance.EQUIPMENT);
            mEqui.SetAttribute(DbAttributeInstance.NAME, "/ExampleEqui");

            //ExampleCyli
            mCyli = mEqui.Create(0, DbElementTypeInstance.CYLINDER);
            mCyli.SetAttribute(DbAttributeInstance.NAME, "/ExampleCyli");

            //ExampleBox
            mBox = mEqui.Create(0, DbElementTypeInstance.BOX);
            mBox.SetAttribute(DbAttributeInstance.NAME, "/ExampleBox");

            //ExampleNozz1
            mNozz1 = mEqui.Create(0, DbElementTypeInstance.NOZZLE);
            mNozz1.SetAttribute(DbAttributeInstance.NAME, "/ExampleNozz1");

            //ExampleNozz2
            mNozz2 = mEqui.Create(0, DbElementTypeInstance.NOZZLE);
            mNozz2.SetAttribute(DbAttributeInstance.NAME, "/ExampleNozz2");

            //ExamplePipe
            mPipe = mZone.Create(0, DbElementTypeInstance.PIPE);
            mPipe.SetAttribute(DbAttributeInstance.NAME, "/ExamplePipe");

            //ExampleBran
            mBran = mPipe.Create(0, DbElementTypeInstance.BRANCH);
            mBran.SetAttribute(DbAttributeInstance.NAME, "/ExampleBran");

            //ExampleGask
            mGask = mBran.Create(0, DbElementTypeInstance.GASKET);
            mGask.SetAttribute(DbAttributeInstance.NAME, "/ExampleGask");

            //ExampleElbow          
            mElbo = mBran.Create(0, DbElementTypeInstance.ELBOW);
            mElbo.SetAttribute(DbAttributeInstance.NAME, "/ExampleElbow");

            //ExampleTee         
            mTee = mBran.Create(0, DbElementTypeInstance.TEE);
            mTee.SetAttribute(DbAttributeInstance.NAME, "/ExampleTee");
        }

        private void CloseCurrentProject()
        {
            //Close project
            if (Project.CurrentProject.IsOpen())
            {
                MDB.CurrentMDB.SaveWork("Changes made by tests");
                MDB.CurrentMDB.Close();
                Project.CurrentProject.Close();
            }
        }

        private void StartExampleProject()
        {
            //Open project
            Project.CurrentProject.Open("BAS", "SYSTEM", "/XXXXXX");
            MDBSetup setUp = MDBSetup.CreateMDBSetup("/CTBATEST");
            MDB.CurrentMDB.Open(setUp);
        }
    }
}
