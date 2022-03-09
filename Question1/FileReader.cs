using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Question1
{
    public class FileReader
    {
        public static string[] ReadInputFile()
        {
            string appPath = Directory.GetCurrentDirectory();
            string inputPath = appPath + "\\listinput.txt";
            string[] lines = File.ReadAllLines(inputPath);
            return lines;
            
        }
        public static Tools ReadParametersFile()
        {
            int m_min=-1, m_max=-1, v_max=-1, v_min=-1, n=-1;
            string appPath = Directory.GetCurrentDirectory();
            string inputPath = appPath + "\\parameters.txt";
            string[] lines = File.ReadAllLines(inputPath);
            for (int i = 0; i < lines.Length; i++)
            {
                var splitted = lines[i].Split(":");
                switch (splitted[0].ToLower().Trim())
                {
                    case "n":
                        n = int.Parse(splitted[1]);
                        break;
                    case "m_min":
                        m_min = int.Parse(splitted[1]);
                        break;
                    case "m_max":
                        m_max = int.Parse(splitted[1]);
                        break;
                    case "v_max":
                        v_max = int.Parse(splitted[1]);
                        break;
                    case "v_min":
                        v_min = int.Parse(splitted[1]);
                        break;
                    default:
                        
                        break;
                }
            }
            Tools tools = new Tools(m_min, m_max, v_max, v_min, n);
            return tools;
        }
        public static double[] CreateArrayFromInput(string inputL)
        {
            
            string[] strList = inputL.Split(' ');
            double[] dList = new double[strList.Length];
            for (int i = 0; i < strList.Length; i++)
            {
                dList[i] = double.Parse(strList[i]);
            }
            return dList;
        }
        public static void CreateListFromFile()
        {
            var cNumber = 0;
            string FileErrorMsg = "Girdi dosyası hatalı";
            string[] inputs = ReadInputFile();
            foreach (var input in inputs)
            {
                var type = input.Substring(0, 2);
                if(type == "L:")
                {
                    var value = input.Substring(2, input.Length - 2).Trim();
                    Tools.Liste.Add(CreateArrayFromInput(value));
                }else if(type == "C:")
                {
                    cNumber += 1;
                    var value = input.Substring(2, input.Length - 2).Trim();
                    Tools.C = CreateArrayFromInput(value);
                }
                else
                {
                    throw new Exception(FileErrorMsg);
                }
            }
            if (cNumber != 1)
            {
                throw new Exception(FileErrorMsg);
            }
        }
    }
}
