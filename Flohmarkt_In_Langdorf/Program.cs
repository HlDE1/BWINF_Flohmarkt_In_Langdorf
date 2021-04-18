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
        static List<string> input_txt = File.ReadAllLines(@"D:\Coding\BWINF\2. Runde\Aufgabe 1\Materialien\flohmarkt7.txt").ToList();
        static int Voranmeldungen = Convert.ToInt32(input_txt[0]);
        static List<string> Numbers = new List<string>();
        static List<string> SortedList = new List<string>();
        static int[] Info_SortedList = new int[Voranmeldungen];

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
        static int GetMinIndex(int[] array, int i, int min)
        {
            int index = 0;
            //Console.WriteLine("array: " + array[i] + " min: " + min + " i: " + i);
            if (array[i] < min)
            {
                //Console.WriteLine("array2: " + array[i] + " min2: " + min + " i2: " + i);
                min = array[i];
                index = i;
            }
            return index;
        }


        static void SortList_Index_Fix(int[] array)
        {
            int[] num = new int[Voranmeldungen];
            int[] num2 = new int[Voranmeldungen];
            List<string> List = new List<string>();
            List<string> Check_Num = new List<string>();

            for (int i = 0; i < Voranmeldungen; i++)
            {
                num[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);
                num2[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);

            }
            Array.Sort(num2);
            int min = num[0];
            for (int i = 0; i < Voranmeldungen; i++)
            {
                for (int j = 0; j < Voranmeldungen; j++)
                {
                    if (num[j] == num2[i] && !Check_Num.Contains(j.ToString())) //!tmp.Contains(j.ToString()))
                    {
                        List.Add(j.ToString());
                        Check_Num.Add(j.ToString());
                        //tmp += j.ToString();
                    }
                }
                //Console.WriteLine("");
            }
            for (int i = 0; i < Voranmeldungen; i++)
            {
                array[i] = Convert.ToInt32(List[i]);
                // Console.WriteLine("List: " + List[i]);
            }
        }

        #region Old SortList_Index
        /*static int[] SortList_Index(int[] array)
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
        */
        #endregion






        static int[] SortList_Index(int[] array)
        {
            int[] num = new int[Voranmeldungen];
            int[] num2 = new int[Voranmeldungen];
            List<string> List = new List<string>();
            List<string> Check_Num = new List<string>();

            for (int i = 0; i < Voranmeldungen; i++)
            {
                num[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);
                num2[i] = Convert.ToInt32(Numbers[i].Split(' ')[0] + Numbers[i].Split(' ')[1]);

            }
            Array.Sort(num2);
            for (int i = 0; i < Voranmeldungen; i++)
            {
                for (int j = 0; j < Voranmeldungen; j++)
                {
                    if (num[j] == num2[i] && !Check_Num.Contains(j.ToString())) //!tmp.Contains(j.ToString()))
                    {
                        List.Add(j.ToString());
                        Check_Num.Add(j.ToString());
                    }
                }
                //Console.WriteLine("");
            }

            for (int i = 0; i < Voranmeldungen; i++)
            {
                array[i] = Convert.ToInt32(List[i]);
                // Console.WriteLine("List: " + List[i]);
            }
            return array;
        }

        static void SortList(List<string> List)
        {
            int[] SortedList = new int[Voranmeldungen];
            for (int i = 0; i < Voranmeldungen; i++)
            {
                List.Add(ReadSpecificLine(SortList_Index(SortedList)[i]));
                //Console.WriteLine("SortList: " + SortedList[i]);
            }
        }

        static void InfoPrint()
        {
            for (int i = 0; i < Voranmeldungen; i++)
            {
                string[] tmp = ReadSpecificLine(Info_SortedList[i]).Split(' ');

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

        static string RemoveAfterChar(string input, string character)
        {
            int index = input.LastIndexOf(character);
            if (index > 0)
                input = input.Substring(0, index);
            return input;
        }

        static void Remove_MinPreis(int MietlängenCheck, decimal min, List<string> List)
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
                    //Console.WriteLine("Preis: " + Preis + " Min: " + min);
                    if (Preis == min)
                    {
                        Console.WriteLine($"{List[i]} wird entfernt");
                        List.Remove(List[i]);
                    }
                    else
                    {
                        //Console.WriteLine("Fehler!!!");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            List<int> Test = new List<int>();
            AddNumbers();
            SortList_Index_Fix(Info_SortedList);
            InfoPrint();
            SortList(SortedList);
            TimeCheck();


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

            int sum = 0;
            decimal min = Decimal.MaxValue;
            List<string> Remove_nums = new List<string>();
            for (int a = 0; a < SortedList.Count; a++)
            {
                int Mietbegin = Convert.ToInt32(SortedList[a].Split(' ')[0]); //8;
                int Mietende = Convert.ToInt32(SortedList[a].Split(' ')[1]);// 15;
                int MietlängenCheck = 0;
                //Console.WriteLine($"=>Mietbegin: {Mietbegin} | Mietende: {Mietende} \n");

                for (int Uhr = Mietbegin; Uhr < Mietende; Uhr++)
                {
                    for (int i = 0; i < SortedList.Count; i++)
                    {
                        int start_time = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                        int end_time = Convert.ToInt32(SortedList[i].Split(' ')[1]);
                        int Meter = Convert.ToInt32(SortedList[i].Split(' ')[2]);

                        //int Mietbegin = 8;
                        //int Mietende = 12;

                        if (end_time >= Mietbegin && end_time > Mietende || start_time < Mietende) //&& end_time >= Mietende)
                        {
                            int start_time_test = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                            int end_time_test = Convert.ToInt32(SortedList[i].Split(' ')[1]);
                            //int Preis = GesamtPreis(start_time_test, end_time_test, Meter);

                            //int Uhr = 9;
                            if (start_time_test <= Uhr && end_time_test > Uhr && Uhr < Mietende)// Hier fehlt noch eine Schleife um alle Zeiten von start_time bis end_time abzuchecken
                            {
                                int Preis = GesamtPreis(start_time_test, end_time_test, Meter);
                                //Console.WriteLine($"{SortedList[i]} {Preis}");
                                if (Preis < min)// Kleinsten Gesamtpreis
                                {
                                    min = Preis;
                                }
                                //Console.WriteLine(min);
                                //Remove_nums.Add($"{SortedList[i]} {Preis}");
                                sum += Meter;
                            }
                        }
                    }
                    //Console.WriteLine($"Uhr:{Uhr}");
                    //Console.WriteLine($"Länge:{sum}");
                    //Console.WriteLine($"MinZahl: {min}");
                    MietlängenCheck = sum;
                    Remove_MinPreis(MietlängenCheck, min, SortedList);
                    min = Decimal.MaxValue;
                    sum = 0;
                    //Console.WriteLine("");
                }
            }
            //Console.WriteLine($"\nLänge:{sum}\n");
            #region RemoveMin
            /*if (MietlängenCheck > 1000) // Den kleinsten Gesamtpreis entfernen
            {
                Console.WriteLine("Den kleinsten Gesamtpreis entfernen");
                for (int i = 0; i < SortedList.Count; i++)
                {
                    int start_time = Convert.ToInt32(SortedList[i].Split(' ')[0]);
                    int end_time = Convert.ToInt32(SortedList[i].Split(' ')[1]);
                    int Meter = Convert.ToInt32(SortedList[i].Split(' ')[2]);
                    int Preis = GesamtPreis(start_time, end_time, Meter);
                    if (Preis == min)
                    {
                        Console.WriteLine($"{SortedList[i]} wird entfernt");
                        SortedList.Remove(SortedList[i]);
                    }
                }
            }*/
            #endregion

            Console.WriteLine("");
            for (int i = 0; i < SortedList.Count; i++)
            {
                Console.WriteLine(SortedList[i]);
            }

            Console.ReadKey();
        }
    }
}