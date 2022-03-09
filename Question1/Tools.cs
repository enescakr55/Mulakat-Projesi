using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Question1
{
    public class Tools
    {
        public int M_min;
        public int M_max;
        public int V_max;
        public int V_min;
        public int N;
        public Tools(int m_min, int m_max, int v_max, int v_min,int n)
        {
            this.M_min = m_min;
            this.M_max = m_max;
            this.V_min = v_min;
            this.V_max = v_max;
            this.N = n;
        }
        public Tools(int m_min, int m_max, int v_max, int v_min)
        {
            this.M_min = m_min;
            this.M_max = m_max;
            this.V_min = v_min;
            this.V_max = v_max;
        }
        public Tools()
        {

        }
        Random random = new Random();
        public static List<double[]> Liste = new List<double[]>();
        public static double[] C;
        public static List<double[]> SortedList;
        public double[] GetRandomArray()
        {
            int arraySize = random.Next(M_min, M_max+1);
            double[] arr = new double[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                arr[i] = Math.Round(random.Next(V_min, V_max) * random.NextDouble(), 2);
            }
            return arr;
        }
        public void CreateC()
        {
            double[] arr = new double[M_max];
            for (int i = 0; i < M_max; i++)
            {
                arr[i] = Math.Round(random.Next(V_min, V_max) * random.NextDouble(), 2);
            }
            C = arr;
        }
        public void CreateCWithConsole()
        {
            var max = 0;
            foreach (var l in Liste)
             {
                if (l.Length > max)
                {
                    max = l.Length;
                }
            }
            int[] arr = new int[max];
            Console.WriteLine(max + " elemanlı bir sayı listesi girin (Sayıları boşluklar ile ayırın)");
            var str = Console.ReadLine();
            C = CreateArrayWithStr(str);
        }
        public void CreateDataList(int N)
        {
            Liste.Clear();
            for (var i = 0; i < N; i++)
            {
                Liste.Add(GetRandomArray());
            }
        }
        public void CreateDataListWithConsole(int N)
        {
            Liste.Clear();
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Sayıları girin (Sayıları boşluklar ile ayırın)");
                var str = Console.ReadLine();
                Liste.Add(CreateArrayWithStr(str));
            }
        }
        private double[] CreateArrayWithStr(string str)
        {
            var parsedStr = str.Split(" ");
            double[] x = new double[parsedStr.Length];
            for (int i = 0; i < parsedStr.Length; i++)
            {
                x[i] = double.Parse(parsedStr[i]);
            }
            return x;
        }
        public void WriteSortedListToConsole()
        {
            Console.Write("{");
            foreach (var eleman in SortedList)
            {
                Console.Write("{");
                foreach (var sayi in eleman)
                {
                    Console.Write(sayi + " ");
                }
                Console.Write("}");
            }
            Console.Write("}");
            Console.WriteLine();
        }
        public void WriteCToFile()
        {
            var CStr = "C : {";
            foreach (var sayi in C)
            {
                CStr = CStr + sayi + " ";
            }
            CStr += "}";
            FileWriter.WriteLine(CStr);
        }
        public int MinListValue()
        {
            int min = Liste[0].Length;
            foreach (var list in Liste)
            {
                if (list.Length < min)
                {
                    min = list.Length;
                }
            }
            min = M_min != 0 ? M_min : min;
            return min;
        }
        public int MaxListValue()
        {
            int max = 0;
            foreach (var list in Liste)
            {
                if (list.Length > max)
                {
                    max = list.Length;
                }
            }
            max = M_max != 0 ? M_max : max;
            return max;
        }

        public List<KeyValuePair<int, double>> CalculateList()
        {
            List<KeyValuePair<int, double>> values = new List<KeyValuePair<int, double>>();
            int n = Liste.Count;
            for (int i = 0; i < n; i++)
            {
                values.Add(new KeyValuePair<int, double>(i, CalculateArrayValue(Liste[i])));
            }
            return values;
        }
        public double CalculateArrayValue(double[] arr)
        {
            string str = "";
            string LArray = "L : {";
            double toplam = 0;
            int max= MaxListValue();
            for (int i = 0; i < max; i++)
            {
                
                if(i < arr.Length)
                {
                    str = str + "(" + Math.Round(C[i],2) + " * " + Math.Round(arr[i],2) + ")";
                    toplam += C[i] * arr[i];
                }
                else
                {
                    str = str + "(" + Math.Round(C[i],2) + " * " + 0 + ")";
                }
                var getArr = arr.Length > i ? arr[i] : 0;
                LArray = LArray + getArr + " ";
            }
            LArray = LArray + "}";
            FileWriter.WriteLine(LArray);
            
            str = Math.Round(toplam,2) + "=>" + str;
            FileWriter.WriteLine(str);
            return toplam;
        }
        public void WriteMinAndMaxToFile()
        {
            FileWriter.WriteLine("M_Min : " + MinListValue() + " M_Max : " + MaxListValue());
        }
        public List<double[]> SortDescending()
        {

            List<double[]> newList = new List<double[]>();
            WriteMinAndMaxToFile();
            WriteCToFile();
            var calculatedList = CalculateList();
            double[] values = new double[calculatedList.Count];
            for (int i = 0; i < calculatedList.Count; i++)
            {
                values[i] = calculatedList[i].Value;
            }
            double temp;
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length - 1; j++)
                {
                    if (values[j] < values[j + 1])
                    {
                        temp = values[j + 1];
                        values[j + 1] = values[j];
                        values[j] = temp;
                    }
                }
            }
            for (int i = 0; i < values.Length; i++)
            {
                KeyValuePair<int, double> x = calculatedList.Where(k => k.Value == values[i]).First();
                newList.Add(Liste[x.Key]);
                calculatedList.Remove(x);
            }
            SortedList = newList;
            WriteSortedListToOutput();
            return newList;
        }
        public void WriteSortedListToOutput()
        {
            string str = "Sorted List : {";
            foreach (var l in SortedList)
            {
                str = str + "{";
                foreach (var number in l)
                {
                    str = str + number+" ";
                }
                str = str + "},";
            }
            str = str + "}";
            FileWriter.WriteLine(str);
        }

    }
}
