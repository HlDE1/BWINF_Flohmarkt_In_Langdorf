using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flohmarkt_In_Langdorf
{
    class Program
    {
        // static List<string> input_txt = File.ReadAllLines(@"D:\Coding\BWINF\2. Runde\Aufgabe 1\Materialien\flohmarkt7.txt").ToList();
        // static int Voranmeldungen = Convert.ToInt32(input_txt[0]);
        // static int[] Info_SortedList_Index = new int[Voranmeldungen];

        static List<string> input_txt = new List<string>();
        static int Voranmeldungen;
        static List<string> Numbers = new List<string>();
        static List<string> SortedList = new List<string>();
        static int[] Info_SortedList_Index = new int[Voranmeldungen];



        //3 Zahlen -> Mietbeginn(volle stunden) | Mietende (volle Stunde) |länge des Standes (in Metern)
        //Flohmarkt Öffnungszeiten 8-18 | Flohmarkt länge = 1000m
        //1€ pro Stunde & Meter -> 1h & 1 Meter = 1€
        static void AddNumbers() // Fügt Mietbegin, Mietende und länge des Standes in die Liste Numbers
        {
            for (int i = 1; i < input_txt.Count; i++)
            {
                Numbers.Add(input_txt[i]);
            }
        }
        static int MathAbs_Cal(int n1, int n2)
        {
            return Math.Abs(n1 - n2);
        }
        static string ReadSpecificLine(int line)
        {
            return Numbers.Skip(line).First();
        }

        static void SortList_Index_Fix(int[] array) // Die minimum Indizes werden im array abgelagert
        {
            int[] num = new int[Voranmeldungen];
            int[] num2 = new int[Voranmeldungen];
            List<string> List = new List<string>();
            List<string> Check_Num = new List<string>();

            for (int i = 0; i < Voranmeldungen; i++)  //Mietbegin und Mietende werden zu einer Zahl                                               
            {
                num[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);
                num2[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);

            }
            Array.Sort(num2);

            for (int i = 0; i < Voranmeldungen; i++)
            {
                for (int j = 0; j < Voranmeldungen; j++)
                {
                    if (num[j] == num2[i] && !Check_Num.Contains(j.ToString())) // Wird gechecked an welcher Position, die nächst Größere Zahl zu finden ist | Check_Num ist dafür da, um wiederholungen zu vermeiden 
                    {
                        List.Add(j.ToString());
                        Check_Num.Add(j.ToString());
                    }
                }
            }
            for (int i = 0; i < Voranmeldungen; i++)
            {
                array[i] = Convert.ToInt32(List[i]); // Die Zahlen in der Liste 'List' werden in 'array' übertragen
            }
        }

        static void SortList(List<string> List)
        {
            for (int i = 0; i < Voranmeldungen; i++)
            {
                List.Add(ReadSpecificLine(Info_SortedList_Index[i]));
            }
        }

        static void InfoPrint() //Printet die sortierten Mietzeiten, Mietenden und Mietlängen
        {
            for (int i = 0; i < Voranmeldungen; i++)
            {
                string[] tmp = ReadSpecificLine(Info_SortedList_Index[i]).ToString().Split(' ');

                int Zeit = MathAbs_Cal(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]));
                int Kosten = Zeit * Convert.ToInt32(tmp[2]);

                Console.WriteLine($"{i + 1}. Von {tmp[0]} bis {tmp[1]} Uhr | Mietlänge: {tmp[2]}m");
                Console.WriteLine($"=>Mietzeit: {Zeit}h | Kosten: {Kosten} Euro \n");
            }
        }

        static void TimeCheck() // Öffnungszeiten check | Falls die Öffnungszeiten nicht von 8 bis 18 sind, wird das Element entfernt
        {
            for (int i = 0; i < SortedList.Count; i++)
            {
                int start_time = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                int end_time = Convert.ToInt32(SortedList[i].Split(' ')[1]);
                if (start_time >= 8 && start_time <= 18 && start_time < end_time)
                {
                    //Console.WriteLine($"{i + 1}. {start_time}");
                }
                else
                {
                    SortedList.RemoveAt(i);
                }
            }
        }

        static int GesamtPreis(int Mietbegin, int Mietende, int Meter) // Mietende - Mietbegin = Stunden |  Stunden * Meter = Gesamtpreis
        {
            int Stunden = MathAbs_Cal(Mietbegin, Mietende);
            return Stunden * Meter;
        }


        static void Remove_MinPreis(int MietlängenCheck, decimal min, List<string> List) // Wenn die Mietlänge > 1000 wird das Element entfernt  
        {
            if (MietlängenCheck > 1000) // Den kleinsten Gesamtpreis entfernen
            {
                //Console.WriteLine("Den kleinsten Gesamtpreis entfernen\n");
                for (int i = 0; i < List.Count; i++)
                {
                    int start_time = Convert.ToInt32(List[i].Split(' ')[0]);
                    int end_time = Convert.ToInt32(List[i].Split(' ')[1]);
                    int Meter = Convert.ToInt32(List[i].Split(' ')[2]);
                    int Preis = GesamtPreis(start_time, end_time, Meter);
                    if (Preis == min) //min ist dafür da um die Person zu bestimmen der am wenigsten bezahlt 
                    {
                        Console.WriteLine($"{List[i]} wurde entfernt");
                        List.Remove(List[i]);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Bitte geben Sie den Pfad der Datei ein:");
            string input = Console.ReadLine();
            try
            {
                if (File.Exists(input))
                {

                    input_txt = File.ReadAllLines(input).ToList();
                    Voranmeldungen = Convert.ToInt32(input_txt[0]);
                    Console.WriteLine(input_txt[1]);
                    Console.WriteLine(Voranmeldungen);

                    AddNumbers();
                    SortList_Index_Fix(Info_SortedList_Index);
                    InfoPrint();
                    SortList(SortedList);
                    TimeCheck();


                    int sum = 0;
                    decimal min = Decimal.MaxValue;
                    List<string> Remove_nums = new List<string>();
                    for (int a = 0; a < SortedList.Count; a++)
                    {
                        int Mietbegin = Convert.ToInt32(SortedList[a].Split(' ')[0]);
                        int Mietende = Convert.ToInt32(SortedList[a].Split(' ')[1]);
                        int MietlängenCheck = 0;

                        for (int Uhr = Mietbegin; Uhr < Mietende; Uhr++)
                        {
                            for (int i = 0; i < SortedList.Count; i++)
                            {
                                int start_time = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                                int end_time = Convert.ToInt32(SortedList[i].Split(' ')[1]);
                                int Meter = Convert.ToInt32(SortedList[i].Split(' ')[2]);

                                if (end_time >= Mietbegin && end_time > Mietende || start_time < Mietende)
                                {
                                    int start_time_test = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                                    int end_time_test = Convert.ToInt32(SortedList[i].Split(' ')[1]);

                                    if (start_time_test <= Uhr && end_time_test > Uhr && Uhr < Mietende)
                                    {
                                        int Preis = GesamtPreis(start_time_test, end_time_test, Meter);
                                        if (Preis < min)// Den Kleinsten Gesamtpreis bestimmen
                                        {
                                            min = Preis;
                                        }
                                        sum += Meter;
                                    }
                                }
                            }
                            MietlängenCheck = sum;
                            Remove_MinPreis(MietlängenCheck, min, SortedList);
                            min = Decimal.MaxValue;
                            sum = 0;
                        }
                    }

                    Console.WriteLine("");
                    int tmp_Preis = 0;
                    for (int i = 0; i < SortedList.Count; i++)
                    {
                        int Mietbegin = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                        int Mietende = Convert.ToInt32(SortedList[i].Split(' ')[1]);
                        int Meter = Convert.ToInt32(SortedList[i].Split(' ')[2]);
                        Console.WriteLine($"{i}. {SortedList[i]}");
                        tmp_Preis += GesamtPreis(Mietbegin, Mietende, Meter);
                    }
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine($"Der Gesamtpreis der Anwesenden beträgt {tmp_Preis} Euro");
                }
                else
                {
                    Console.WriteLine("Error - Die Datei wurde nicht gefunden");
                }
            }
            catch
            {

            }
            Console.ReadKey();
        }
    }
}