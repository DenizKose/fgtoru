﻿using System;
using System.IO;
using System.Text;

namespace RU_fo_FG
{
    class Program
    {

        static void Main(string[] args)
        {
            // Encoding info
            Encoding utf8 = Encoding.UTF8;
            Encoding iso = Encoding.GetEncoding("iso-8859-1");

            //Program name and version
            string progam_name = "FG to RU";
            string version = "0.1.1 v";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{progam_name} {version} \n");
            Console.ResetColor();

            //Directory to files
            string[] paths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");



            if (paths.Length <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("В папке нет xml файлов", Console.ForegroundColor);
                Console.ResetColor();
            }
            else
            {
                foreach (string path in paths)
                {
                    string file_name = Path.GetFileName(path);

                    if (args.Length != 0)
                    {
                        if (args[0] == "i")
                        {
                            //Conversation file from iso to utf
                            Console.WriteLine($"Файл по пути: {path} \n--\nКонвератция в UTF8\n--");
                            string text_file = File.ReadAllText(path, iso);
                            File.WriteAllText(path, text_file, utf8);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\n######################\nФайл {file_name} сконвертирован в UTF8\n######################\n\n", Console.ForegroundColor);
                            Console.ResetColor();
                        }
                        else if (args[0] == "u")
                        {
                            //Conversation file from utf to iso
                            Console.WriteLine($"Файл по пути: {path} \n--\nКонвератция в ISO-8859-1 (логика конвертации @kreodont)\n--");
                            string new_file = File.ReadAllText(path, utf8);
                            string translate = ToIso(new_file);
                            File.WriteAllText(path, translate, iso);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\n######################\nФайл {file_name} сконвертирован в ISO-8859-1\n######################\n\n", Console.ForegroundColor);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Запустите bat файл в папке с программой", Console.ForegroundColor);
                        Console.ResetColor();
                    }
                }
            }

            Console.WriteLine("\nДля завершения нажмите ENTER");
            Console.ReadKey();
        }

        //Thank's for function in python Dmitry Masanov (@kreodont)
        public static string ToIso(string text)
        {
            int first_letter_code = 192;
            var all_letters = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюя";
            string result_text = "";

            foreach (char c in text)
            {
                if (c.Equals("ё"))
                {
                    result_text += "&#184;";
                }
                else if (c.Equals("Ё"))
                {
                    result_text += "&#168;";
                }
                else if (all_letters.Contains(c.ToString()))
                {
                    int char_position = all_letters.IndexOf(c);
                    int code = first_letter_code + char_position;
                    result_text += $"&#{code};";
                }
                else
                {
                    result_text += c;
                }
            }
            return result_text;
        }

    }
}
