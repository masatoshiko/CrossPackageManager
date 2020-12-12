using System;
using System.Collections.Generic;
using LibCrossPackageManager;

namespace CrossPackageManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentParser argparse = new ArgumentParser();
            argparse.Commands = new Dictionary<string, string>()
            {
                {"install","-i"},
                {"remove","-r"},
                {"sync","-s"},
                {"search","-s"},
            };

            argparse.Options = new Dictionary<string, string>()
            {
                {"--allyes", "-y"}
            };

            ParseResult parseResult = argparse.Parse(args);
        }
    }
}
