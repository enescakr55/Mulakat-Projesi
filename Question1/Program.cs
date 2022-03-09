using System;
using System.IO;

namespace Question1
{
    class Program
    {
        static void Main(string[] args)
        {
            int N, M_min, M_max;
            int V_max, V_min;
            Tools tools = new Tools();
            Console.WriteLine("Veri Getirme Tercihi");
            Console.WriteLine("1 --> Otomatik Oluştur");
            Console.WriteLine("2 --> Konsoldan veri iste");
            Console.WriteLine("3 --> Dosyadan Getir");
            Console.Write("Seçim : ");
            var secim = Console.ReadLine(); 

            if (secim == "1")
            {
                Console.WriteLine("Parametre Getirme Tercihi");
                Console.WriteLine("1 --> Konsoldan İste");
                Console.WriteLine("2 --> Parametreler dosyasından getir");
                var getParams = Console.ReadLine();
                if(getParams == "1") {
                    Console.WriteLine("Veri listesi için eleman sayısı girin (N) : ");
                    N = int.Parse(Console.ReadLine());
                    Console.WriteLine("Minimum eleman sayısı girin (M_min) : ");
                    M_min = int.Parse(Console.ReadLine());
                    Console.WriteLine("Maksimum eleman sayısı girin (M_max) : ");
                    M_max = int.Parse(Console.ReadLine());
                    Console.WriteLine("Maksimum değer (V_max) : ");
                    V_max = int.Parse(Console.ReadLine());
                    Console.WriteLine("Minimum değer (V_min) : ");
                    V_min = int.Parse(Console.ReadLine());
                    tools = new Tools(M_min, M_max, V_max, V_min);
                    tools.CreateDataList(N);
                    tools.CreateC();
                }else if(getParams == "2")
                {
                    tools = FileReader.ReadParametersFile();
                    tools.CreateDataList(tools.N);
                    tools.CreateC();
                }

            }
            else if (secim == "2")
            {
                Console.WriteLine("Veri listesi için eleman sayısı girin (N) : ");
                N = int.Parse(Console.ReadLine());
                tools.CreateDataListWithConsole(N);
                tools.CreateCWithConsole();
            }else if(secim == "3")
            {
                FileReader.CreateListFromFile();
            }
            else
            {
                throw new Exception("Geçersiz Giriş");
            }

            tools.SortDescending();
            Console.WriteLine("Oluşturulan Sıralı Liste");
            tools.WriteSortedListToConsole();
            Console.WriteLine("Detaylar "+ FileWriter.OutputPath +" dosyasına kayıt edildi");
            Console.ReadKey();

        }
    }
}
