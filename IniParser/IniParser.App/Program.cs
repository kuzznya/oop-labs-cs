using System;
using System.IO;
using IniParser.Exception;

namespace IniParser.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new IniParser();

            try
            {
                var data = parser.Parse(args[0]);

                var property = data.Section(args[1]).GetProperty(args[2]);
                Console.WriteLine($"[{args[1]}]: {args[2]} = {property.GetStringValue()} ({property.Type})");
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