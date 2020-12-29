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
        public bool ExistPackage{ get; set; }
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

        /// <summary>
        /// パッケージ名からパッケージを探し、その情報を返します。<br>
        /// 完全一致検索を行うので注意。
        /// </summary>
        /// <param name="package_name">パッケージ名</param>
        /// <returns>パッケージ情報</returns>
        public PackageInfo FindPackageInfoByName(string package_name)
        {
            PackageInfo pkginfo;

            using (LiteDatabase db = new LiteDatabase(@"./database/packages.db"))
            {
                ILiteCollection<PackageInfo> pkginfo_col = db.GetCollection<PackageInfo>("package_info");
                pkginfo = pkginfo_col.FindOne(x => x.PackageName == package_name);
            }

            if (pkginfo.PackageName != null) pkginfo.ExistPackage = true;
            return pkginfo;
        }

        /// <summary>
        /// データベースIDからパッケージを探し、その情報を返します。<br>
        /// </summary>
        /// <param name="database_id"></param>
        /// <returns></returns>
        public PackageInfo FindPackageInfoById(int database_id)
        {
            PackageInfo pkginfo;

            using (LiteDatabase db = new LiteDatabase(@"./database/packages.db"))
            {
                ILiteCollection<PackageInfo> pkginfo_col = db.GetCollection<PackageInfo>("package_info");
                pkginfo = pkginfo_col.FindOne(x => x.Id == database_id);
            }

            if (pkginfo.PackageName != null) pkginfo.ExistPackage = true;
            return pkginfo;
        }

        public bool UpdatePackageInfo(PackageInfo new_pkginfo)
        {
            using (LiteDatabase db = new LiteDatabase(@"./database/packages.db"))
            {
                ILiteCollection<PackageInfo> pkginfo_col = db.GetCollection<PackageInfo>("package_info");
                pkginfo_col.Update(new_pkginfo);
            }

            return true;
        }
    }
}