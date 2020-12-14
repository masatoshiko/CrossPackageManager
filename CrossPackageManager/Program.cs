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

            if (args.Length == 0)
            {
                CommandNotFoundError();
                return;
            }

            ParseResult parseResult = argparse.Parse(args);

            if (parseResult.InvalidOptions != null)
            {
                InvalidOptionError(parseResult.InvalidOptions.ToArray());
                return;
            }

            switch (parseResult.Command)
            {
                case "install":
                    Install install = new Install();
                    install.Main(parseResult.Arguments.ToArray());
                    break;

                default:
                    InvalidCommandError(parseResult.Command);
                    return;
            }
        }

        static private void InvalidOptionError(string[] invalid_options)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(T._("Invalid option(s): {0}", string.Join(',', invalid_options)));
            Console.WriteLine(T._("See 'crspkg help'"));

            Console.ResetColor();
        }

        static private void CommandNotFoundError()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(T._("Command is not found."));
            Console.WriteLine(T._("See 'crspkg help'"));

            Console.ResetColor();
        }

        static private void InvalidCommandError(string invalid_command)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(T._("Invalid command: {0}", invalid_command));
            Console.WriteLine(T._("See 'crspkg help'"));

            Console.ResetColor();
            return;
        }
    }
}
