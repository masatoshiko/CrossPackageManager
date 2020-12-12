using System.Collections.Generic;
using System.Linq;

namespace LibCrossPackageManager
{
    public struct ParseResult
    {
        public string Command { get; }
        public List<string> Arguments { get; }
        public List<string> SettedOptions { get; }
        public List<string> InvalidOptions { get; }

        public ParseResult(string command, List<string> arguments, List<string> settedoptions,
            List<string> invalidoptions)
        {
            Command = command;
            Arguments = arguments;
            SettedOptions = settedoptions;
            InvalidOptions = invalidoptions;
        }
    }

    public class ArgumentParser
    {
        /// <summary>
        /// コマンドと短縮コマンドを定義する辞書です<br/>
        /// TKeyにはコマンド名を、TValueには短縮コマンド名(なければnull)を代入してください
        /// </summary>
        public Dictionary<string, string> Commands { get; set; }

        /// <summary>
        /// オプションと短縮オプションを定義する辞書です<br/>
        /// TKeyにはオプション名を、TValueには短縮オプション名(なければnull)を代入してください<br/>
        /// 必ず、オプション名の最初に"-"、短縮オプション名の最初に"--"をつけてください
        /// </summary>
        public Dictionary<string, string> Options { get; set; }

        public ParseResult Parse(string[] args)
        {
            string command = null;
            List<string> arguments = new List<string>();
            List<string> settedoptions = new List<string>();
            List<string> invalidoptions = new List<string>();

            // コマンド解析
            if (Commands.ContainsKey(args[0])) command = args[0];
            else if (Commands.ContainsValue(args[0]))
            {
                KeyValuePair<string, string> parse = Commands.FirstOrDefault(Commands => Commands.Value == args[0]);
                command = parse.Key;
            }

            // オプション解析
            if (args.Length >= 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        if (Options.ContainsValue(args[i]))
                        {
                            KeyValuePair<string, string> parse = Options.FirstOrDefault(Options => Options.Value == args[0]);
                            settedoptions.Add(parse.Key);
                        }
                        else invalidoptions.Add(args[i]);
                    }
                    else if (args[i].StartsWith("--"))
                    {
                        if (Options.ContainsKey(args[i])) settedoptions.Add(args[i]);
                        else invalidoptions.Add(args[i]);
                    }
                    else settedoptions.Add(args[i]);
                }
            }

            settedoptions = settedoptions.Distinct().ToList();

            ParseResult parseResult = new ParseResult(command, arguments, settedoptions, invalidoptions);
            return parseResult;
        }
    }
}
