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
        static List<string> input_txt = File.ReadAllLines(@"D:\Coding\BWINF\2. Runde\Aufgabe 1\Materialien\flohmarkt1.txt").ToList();
        static int Voranmeldungen = Convert.ToInt32(input_txt[0]);
        static List<string> Numbers = new List<string>();
        static List<string> SortedList = new List<string>();

        //3 Zahlen -> Mietbeginn(volle stunden) | Mietende (volle Stunde) |länge des Standes (in Metern)
        //Flohmarkt Öffnungszeiten 8-18 | Flohmarkt länge = 1000m
        //1€ pro Stunde & Meter -> 1h & 1 Meter = 1€
        static void AddNumbers()
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
        static int ReadLinewithText(string text)
        {
            for (int i = 0; i < Voranmeldungen; i++)
            {
                string num_str = Numbers[i].Split(' ')[0] + " " + Numbers[i].Split(' ')[1];
                if (text.Contains(num_str))
                    return i;
            }
            return 0;
        }
        static int[] SortList_Index(int[] array)
        {
            int[] num = new int[Voranmeldungen];
            int num_len = 0;
            for (int i = 0; i < Voranmeldungen; i++)
            {
                num[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);
            }
            Array.Sort(num);
            for (int i = 0; i < Voranmeldungen; i++)
            {
                num_len = (int)num[i].ToString().Length;
                array[i] = Convert.ToInt32(ReadLinewithText(num[i].ToString().Substring(0, num_len / 2) + " " + num[i].ToString().Substring(num_len / 2)));
            }
            return array;
        }

        static void SortList(List<string> List)
        {
            int[] SortedList = new int[Voranmeldungen];
            for (int i = 0; i < Voranmeldungen; i++)
            {
                List.Add(ReadSpecificLine(SortList_Index(SortedList)[i]));
            }
        }

        static void InfoPrint()
        {
            int[] SortedList = new int[Voranmeldungen];
            for (int i = 0; i < Voranmeldungen; i++)
            {
                int Zeit = MathAbs_Cal(Convert.ToInt32(ReadSpecificLine(SortList_Index(SortedList)[i]).Split(' ')[0]), Convert.ToInt32(ReadSpecificLine(SortList_Index(SortedList)[i]).Split(' ')[1]));
                int Kosten = Zeit * Convert.ToInt32(ReadSpecificLine(SortList_Index(SortedList)[i]).Split(' ')[2]);

                Console.WriteLine($"{i + 1}. Von {ReadSpecificLine(SortList_Index(SortedList)[i]).Split(' ')[0]} bis {ReadSpecificLine(SortList_Index(SortedList)[i]).Split(' ')[1]} Uhr | Mietlänge: {ReadSpecificLine(SortList_Index(SortedList)[i]).Split(' ')[2]}m");
                Console.WriteLine($"=>Mietzeit: {Zeit}h| Kosten: {Kosten} Euro \n");
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

        static void Main(string[] args)
        {
            List<int> Test = new List<int>();
            AddNumbers();
            InfoPrint();
            SortList(SortedList);
            TimeCheck();

            //int[] SortedList_Index = new int[Voranmeldungen];
            //SortList_Index(SortedList_Index);

            #region Test
            /* for (int i = 0; i < Voranmeldungen; i++)
             {
                 int start_time = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[i]).Split(' ')[0]);
                 int end_time = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[i]).Split(' ')[1]);

                 for (int j = 0; j < Voranmeldungen; j++)
                 {
                     int other_start_time = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[j]).Split(' ')[0]);
                     int other_end_time = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[j]).Split(' ')[1]);
                     miet_laenge = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[j]).Split(' ')[2]);
                     //Console.WriteLine("other_start_time " + other_start_time + " other_end_time " + other_end_time + " miet_laenge " + miet_laenge);

                     if (start_time >= other_start_time)
                     {
                         Console.WriteLine($"{i}. {start_time}");

                     }
                 }
                 // Console.WriteLine(miete_laenge);
                 }
             }*/
            #endregion
            #region Test2
            /*for (int i = 0; i < Voranmeldungen; i++)
             {
                 for (int j = 8; j <= 18; j++)
                 {
                     for (int a = 0; a < Voranmeldungen; a++)
                     {
                         int other_start_time = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[0]);
                         int other_end_time = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[1]);
                         int Zeit = MathAbs_Cal(Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[0]), Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[1]));
                         int Kosten = Zeit * Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[2]);
                         Meter = Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[2]);
                         if (j >= other_start_time && j < other_end_time)
                         {
                             Console.WriteLine($"{j}  {other_start_time} {other_end_time} {Meter} {Kosten}");
                             test += Convert.ToInt32(ReadSpecificLine(SortList(SortedList)[a]).Split(' ')[2]);
                         }
                     }
                     Console.WriteLine($"{test}");
                     test = 0;
                 }
             }*/
            #endregion



            for (int i = 0; i < SortedList.Count; i++)
            {
                int start_time = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                int end_time = Convert.ToInt32(SortedList[i].Split(' ')[1]);


                //for (int a = 0; a < SortedList.Count; a++)
                {
                   // if (start_time >= 8 && start_time < 12)
                    {
                        Console.WriteLine($"{SortedList[i]}");
                    }
                }

            }

            Console.ReadKey();

        }
    }
}
