//Copyright 1974 to current year. AVEVA Solutions Limited and its subsidiaries. All rights reserved in original code only.
using System;
using System.Collections.Generic;
using System.Text;

using Aveva.Pdms.Database;
using Aveva.Pdms.Geometry;
using Aveva.Pdms.Clasher;

// only for this example class library there is a conflict between
// namespace Aveva.Pdms.Examples and namespace Aveva.Pdms.Clasher
// resolved using an alias or fully qualifying Clasher class
using CLASHER = Aveva.Pdms.Clasher.Clasher;

namespace Aveva.Pdms.Examples
{
    class ExampleClasher
    {
        public static void Run()
        {
            //
            // Spatial Maps
            //
            // Check spatial maps for every DB
            Db[] dbChk = MDB.CurrentMDB.GetDBArray(DbType.Design);
            foreach (Db db in dbChk)
            {
                SpatialMapStatus stat = SpatialMap.Instance.CheckSpatialMap(db);
            }

            // Build spatial maps for MDB
            try
            {
                SpatialMap.Instance.BuildSpatialMap();
            }
            catch (Exception ex)
            {

            }

            //
            // Clasher
            //
            // CheckAll
            ClashOptions opt = ClashOptions.Create();
            opt.IncludeTouches = true;
            opt.Clearance = 10.0;

            ObstructionList obst = ObstructionList.Create();
            ClashSet cs = ClashSet.Create();

            bool bResult = CLASHER.Instance.CheckAll(opt, obst, cs);
            if (bResult)
            {
                // inspect clashes
                Clash[] aClashes = cs.Clashes;
                for (int i = 0; i < aClashes.Length; i++)
                {
                    DbElement first = aClashes[i].First;
                    DbElement second = aClashes[i].Second;
                    ClashType ctp = aClashes[i].Type;
                    Position pos = aClashes[i].ClashPosition;
                }
            }
        }
    }
}
