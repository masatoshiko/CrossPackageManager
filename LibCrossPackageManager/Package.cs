using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LiteDB;

namespace LibCrossPackageManager
{
    /// <summary>
    /// 
    /// </summary>
    public struct PackageInfo
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string PackageId { get; set; }
        public bool IsInstalled { get; set; }
        public List<string> DependentPackages { get; set; }
    }

    public class Package
    {
        public Package()
        {
            if (Directory.Exists("./database/") == false) Directory.CreateDirectory("./database/");
        }

        public PackageInfo GetPackageInfoByName(string package_name)
        {
            PackageInfo pkginfo;

            using (LiteDatabase db = new LiteDatabase(@"./database/packages.db"))
            {
                ILiteCollection<PackageInfo> pkginfo_col = db.GetCollection<PackageInfo>("package_info");

                pkginfo = pkginfo_col.FindOne(x => x.PackageName == package_name);
            }

            return pkginfo;
        }
    }
}
