using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibCrossPackageManager
{
    public struct PackageInfo
    {
        public int Id { get; set; }
        public bool ExistPackage { get; set; }
        public string PackageName { get; set; }
        public string PackageId { get; set; }
        public bool IsInstalled { get; set; }
        public List<string> DependentPackages { get; set; }
    }

    /// <summary>
    /// パッケージデータベースへのアクセス・操作を行うクラスです。
    /// </summary>
    public class PackageDB
    {
        public PackageDB()
        {
            if (Directory.Exists("./database/") == false) Directory.CreateDirectory("./database/");
        }

        /// <summary>
        /// パッケージ名からパッケージを完全一致検索で探し、その情報を返します。
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
        /// データベースIDからパッケージを探し、その情報を返します。<br/>
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

        /// <summary>
        /// パッケージ情報を更新します。
        /// </summary>
        /// <param name="new_pkginfo">新しいパッケージ情報</param>
        /// <returns></returns>
        public bool UpdatePackageInfo(PackageInfo new_pkginfo)
        {
            using (LiteDatabase db = new LiteDatabase(@"./database/packages.db"))
            {
                ILiteCollection<PackageInfo> pkginfo_col = db.GetCollection<PackageInfo>("package_info");
                pkginfo_col.Update(new_pkginfo);
            }

            return true;
        }

        /// <summary>
        /// 新しいパッケージ情報を登録します。
        /// </summary>
        /// <param name="package_info">新しいパッケージ情報</param>
        /// <returns></returns>
        public bool RegisterPackageInfo(PackageInfo package_info)
        {
            using (LiteDatabase db = new LiteDatabase(@"./database/packages.db"))
            {
                ILiteCollection<PackageInfo> pkginfo_col = db.GetCollection<PackageInfo>("package_info");
                pkginfo_col.Insert(package_info);
            }

            return true;
        }
    }
}
