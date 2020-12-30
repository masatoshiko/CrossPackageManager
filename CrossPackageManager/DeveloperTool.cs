using System;
using System.Collections.Generic;
using LibCrossPackageManager;

namespace CrossPackageManager
{
    public class DeveloperTool
    {
        public void Main(string[] args)
        {
            if (args[1] == "--update-pkginfo") UpdatePackageInfo(args);
            else if (args[1] == "--show-pkginfo") ShowPackageInfo(args);
            else if (args[1] == "--regist-pkginfo") RegistPackageInfo(args);
        }

        private void UpdatePackageInfo(string[] args)
        {
            int database_id = -1;
            string new_package_name = null;
            string new_package_id = null;

            while (true)
            {
                if (database_id < 0)
                {
                    Console.Write("データベースのID: ");
                    string temp_dbid = Console.ReadLine();
                    if (temp_dbid == null || !(int.TryParse(temp_dbid, out database_id) && database_id > 0))
                    {
                        Console.WriteLine("不正な入力です。もう一度入力してください。");
                        database_id = -1;
                        continue;
                    }
                }

                if (new_package_name == null)
                {
                    Console.Write("新しいパッケージ名: ");
                    new_package_name = Console.ReadLine();
                }

                if (new_package_id == null)
                {
                    Console.Write("新しいパッケージID: ");
                    new_package_id = Console.ReadLine();
                }

                break;
            }
            
            PackageDB package_db = new PackageDB();
            PackageInfo package_info = package_db.FindPackageInfoById(database_id);
            if (package_info.ExistPackage == false)
            {
                Console.WriteLine($"データベースID {database_id} は存在しません。");
                return;
            }

            Console.WriteLine("-- 元のパッケージ情報 --");
            Console.WriteLine($"データベースID(変更不可): {package_info.Id}");
            Console.WriteLine($"パッケージ名: {package_info.PackageName}");
            Console.WriteLine($"パッケージID: {package_info.PackageId}");

            Console.WriteLine("----");
            Console.WriteLine("新しい情報を代入しています…");

            if (new_package_name != null) package_info.PackageName = new_package_name;
            if (new_package_id != null) package_info.PackageId = new_package_id;

            Console.WriteLine("情報を更新しています…");
            package_db.UpdatePackageInfo(package_info);

            Console.WriteLine("完了");
            return;
        }

        private void ShowPackageInfo(string[] args)
        {
            PackageDB package_db = new PackageDB();

            if (args[2] == "--by-dbid")
            {
                int database_id = -1;
                Console.Write("データベースのID: ");
                string temp_dbid = Console.ReadLine();
                if (temp_dbid == null || (int.TryParse(temp_dbid, out database_id) == false && database_id < 0))
                {
                    Console.WriteLine("不正な入力です。");
                    return;
                }

                Console.WriteLine("-- 検索情報 --");
                Console.WriteLine($"データベースID: {database_id}");
                Console.WriteLine("----");
                Console.WriteLine("データベースIDから、パッケージ情報を検索しています…");

                PackageInfo pkginfo = package_db.FindPackageInfoById(database_id);
                if (pkginfo.ExistPackage == false)
                {
                    Console.WriteLine($"データベースID {database_id} は存在しません。");
                    return;
                }

                Console.WriteLine("パッケージ情報が見つかりました。\n");
                Console.WriteLine("-- パッケージ情報 --");
                Console.WriteLine($"データベースID(変更不可): {pkginfo.Id}");
                Console.WriteLine($"パッケージ名: {pkginfo.PackageName}");
                Console.WriteLine($"パッケージID: {pkginfo.PackageId}");
                Console.WriteLine("----");

                Console.WriteLine("完了しました。");
            }
        }

        private void RegistPackageInfo(string[] args)
        {
            string new_package_name = null;
            string new_package_id = null;

            while (true)
            {
                if (new_package_name == null)
                {
                    Console.Write("新しいパッケージ名: ");
                    new_package_name = Console.ReadLine();

                    if (new_package_name == null)
                    {
                        Console.WriteLine("新しいパッケージ名を入力してください。");
                        continue;
                    }
                }

                if (new_package_id == null)
                {
                    Console.Write("新しいパッケージID: ");
                    new_package_id = Console.ReadLine();

                    if (new_package_id == null)
                    {
                        Console.WriteLine("新しいパッケージIDを入力してください。");
                        continue;
                    }
                }

                break;
            }

            PackageDB package_db = new PackageDB();
            PackageInfo package_info = new PackageInfo()
            {
                PackageName = new_package_name,
                PackageId = new_package_id
            };

            Console.WriteLine("-- 登録するパッケージ情報 --");
            Console.WriteLine($"パッケージ名: {package_info.PackageName}");
            Console.WriteLine($"パッケージID: {package_info.PackageId}");

            Console.WriteLine("----");
            Console.WriteLine("新しいパッケージ情報を登録しています…");

            package_db.RegisterPackageInfo(package_info);

            Console.WriteLine("完了");
            return;
        }
    }
}