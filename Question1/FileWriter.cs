using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Question1
{
    public class FileWriter
    {
        public static string OutputPath = Directory.GetCurrentDirectory() + @"\output.txt";
        public static void WriteLine(string str)
        {

                using (var writer = new StreamWriter(OutputPath, true))
                {
                    writer.WriteLine(str);
                }
            
        }
    }
}
