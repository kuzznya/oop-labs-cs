using System;
using System.IO;
using IniParser.Exception;

namespace IniParser.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3 || args.Length > 4)
            {
                Console.Error.WriteLine("Invalid args count");
                return;
            }
            
            var parser = new IniParser();

            try
            {
                var data = parser.Parse(args[0]);

                var property = data.Section(args[1]).GetProperty(args[2]);
                
                if (args.Length == 3)
                    Console.WriteLine($"{property.GetStringValue()} ({property.Type})");
                else
                {
                    switch (args[3])
                    {
                        case "int":
                            Console.WriteLine($"{data.Section(args[1]).GetInt(args[2])}");
                            break;
                        case "double":
                            Console.WriteLine($"{data.Section(args[1]).GetDouble(args[2])}");
                            break;
                        case "string":
                            Console.WriteLine($"{data.Section(args[1]).GetString(args[2])}");
                            break;
                        default:
                            Console.Error.WriteLine("Invalid type");
                            break;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("File not found");
            }
            catch (ParserException pex)
            {
                Console.Error.WriteLine($"Parsing error: {pex.Message}");
                Console.Error.WriteLine(pex.StackTrace);
            }
        }
    }
}