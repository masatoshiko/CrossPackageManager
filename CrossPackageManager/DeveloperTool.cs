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
        }

        private void UpdatePackageInfo(string[] args)
        {
            int database_id = -1;
            string new_package_name = null;
            string new_package_id = null;

            while(true)
            {
                if (database_id == -1)
                {
                    Console.Write("データベースのID: ");
                    string temp_dbid = Console.ReadLine();
                    if (temp_dbid == null || int.TryParse(temp_dbid, out database_id) == false)
                    {
                        Console.WriteLine("値が入力されていないか、整数ではありません。\nもう一度入力してください。");
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
                    new_package_name = Console.ReadLine();
                }

                break;
            }

            
            Package package = new Package();
            PackageInfo base_pkginfo = package.FindPackageInfoById(database_id);
            if (base_pkginfo.ExistPackage == false)
            {
                Console.WriteLine($"データベースID {database_id} は存在しません。");
                return;
            }

            Console.WriteLine("-- 元のパッケージ情報 --");
            Console.WriteLine($"データベースID(変更不可): {base_pkginfo.Id}");
            Console.WriteLine($"パッケージ名: {base_pkginfo.PackageName}");
            Console.WriteLine($"パッケージID: {base_pkginfo.PackageId}");

            Console.WriteLine("----------------------");
            Console.WriteLine("新しい情報を代入しています…");

            if (new_package_name != null) base_pkginfo.PackageName = new_package_name;
            if (new_package_id != null) base_pkginfo.PackageId = new_package_id;

            Console.WriteLine("情報を更新しています…");
            package.UpdatePackageInfo(base_pkginfo);

            Console.WriteLine("完了");
            return;
        }

        private void ShowPackageInfo(string[] args)
        {

        }
    }
}