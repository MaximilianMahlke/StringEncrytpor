
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringEncrytpor
{
    public class StringEncrytpor
    {
        public static Random rd = new Random();
        public static string Text_Temp = "";
        public static string Text_Encrypted = "";
        public static string Text_Decrypted = "";
        public static char[] chars = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'q', 'w', 'e', 'r', 't', 'z', 'u', 'i', 'o', 'p', 'ü', '+', '#', 'ö', 'l', 'k', 'j', 'h', 'g', 'f', 'd', 's', 'a', '<', 'y', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '-', 'Q', 'W', 'E', 'R', 'T', 'Z', 'U', 'I', 'O', 'P', 'Ü', '*', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Ö', 'Ä', '<', 'Y', 'X', 'C', 'V', 'B', 'N', 'M', ';', ':', '-', '!', '"', '§', '$', '%', '&', '/', '(', ')', '=', '?', 'ä', '§', '$', '%', '&', '/', '[', ']', '}' }; //Alle außer '²'

        public static void Welcome()
        {
            Console.WriteLine("Info: StringEncryptor - Ver- und entschlüsselt Texte und Textdateien.\n");
        }

        public static void GetTextFromUser()
        {
            Console.WriteLine("\nMöchten Sie einen Text\n [1] Verschlüsseln\n [2] Entschlüsseln\n\nBitte wählen:\n[1/2]");
            string input = Console.ReadLine();
            if (input.Equals("1"))              //- Verschlsülleungs-Zweig
            {
                Console.WriteLine("\nWie möchten Sie den zu verschlüsselnden Text eingeben?\n [1] über die Tastatur eingeben\n [2] aus einer Datei importieren\n[1/2]");
                string _input = Console.ReadLine();
                if (_input.Equals("1"))                     //-- User will selbst schreiben
                {
                    Console.WriteLine("\nBitte geben Sie jetzt den zu verschlüsselnden Text ein und bestätigen Sie mit ENTER.");
                    Text_Temp = Console.ReadLine();
                    Console.WriteLine("\nFolgender Text wird verschlüsselt:\n" + Text_Temp);
                    Encrypt(Text_Temp, 0);
                    Console.WriteLine("\nErgebis nach Verschlüsselung:\n" + Text_Encrypted);
                    ExportTextToFile(Text_Encrypted);
                }
                else if (_input.Equals("2"))                 //-- User will aus Datei lesen
                {
                    Text_Temp = ImportTextFromFile();
                    Console.WriteLine("\nFolgender Text wird verschlüsselt:\n" + Text_Temp);
                    Encrypt(Text_Temp, 0);
                    Console.WriteLine("\nErgebis nach Verschlüsselung:\n" + Text_Encrypted);
                    ExportTextToFile(Text_Encrypted);
                }
                else                                        //-- User hat falsche Daten eingegeben
                {
                    Console.WriteLine("\nEingabe wurde nicht erkannt, bitte erneut eingeben!");
                    GetTextFromUser();
                }
            }
            else if (input.Equals("2"))         //- Entschlsülleungs-Zweig
            {
                Console.WriteLine("\n\nWie möchten Sie den zu entschlüsselnden Text eingeben?\n [1] über die Tastatur eingeben\n [2] aus einer Datei importieren\n[1/2]");
                string _input = Console.ReadLine();
                if (_input.Equals("1"))                     //-- User will selbst schreiben
                {
                    Console.WriteLine("\nBitte geben Sie jetzt den zu entschlüsselnden Text ein und bestätigen Sie mit ENTER.");
                    Text_Temp = Console.ReadLine();
                    Console.WriteLine("\nFolgender Text wird entschlüsselt:\n" + Text_Temp);
                    Decrypt(Text_Temp, 0);
                    ExportTextToFile(Text_Decrypted);
                }
                else if (_input.Equals("2"))                 //-- User will aus Datei lesen
                {
                    Text_Temp = ImportTextFromFile();
                    Console.WriteLine("\nFolgender Text wird entschlüsselt:\n" + Text_Temp);
                    Decrypt(Text_Temp, 0);
                    Console.WriteLine("\nErgebis nach Entschlüsselung:\n" + Text_Decrypted);
                    ExportTextToFile(Text_Decrypted);
                }
                else                                        //-- User hat falsche Daten eingegeben
                {
                    Console.WriteLine("\nEingabe wurde nicht erkannt, bitte erneut eingeben!");
                    GetTextFromUser();
                }
            }

            Console.WriteLine("\n\nBitte wählen: \n [1] weiteren Text bearbeiten\n [2] Programm verlassen\n[1/2]");
            input = Console.ReadLine();
            if (input.Equals("1"))              // Rekursion
            {
                GetTextFromUser();
            }

        }

        public static void Encrypt(string text, int counter)         // Verschüsseln (rekursiv)
        {
            if (counter < text.Length)
            {
                Text_Encrypted = Text_Encrypted + Text_Temp[counter] + GetRandomChar() + GetRandomChar();
                counter++;
                Encrypt(text, counter);
            }
        }

        public static void Decrypt(string text, int counter)         // Entschlüsseln (rekursiv)
        {
            if (counter < text.Length - 2)
            {
                Text_Decrypted = Text_Decrypted + text[counter];
                counter += 3;
                Decrypt(text, counter);
            }
        }

        public static char GetRandomChar()                           //Zufälligen Char erstellen/holen
        {
            return chars[(rd.Next(0, 98))];
        }    
        
        public static string GetPathFromUser()
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine("\nMöchten Sie\n [1] das aktuelle Verzeichnis benutzen\n [2] ein Verzeichnis eingeben\n");

            string input = Console.ReadLine();
            if (input.Equals("1"))
            {
                return currentPath;
            }
            else
            {
                Console.WriteLine("\nBitte geben Sie den gewünschten Pfad ein, z.B: C:\nPfad:");
                string userPath = Console.ReadLine();
                 if (userPath[userPath.Length - 1] == '\\')
                    return userPath;
                else
                    return userPath + "\\";
            }
        }

        public static string GetFileNameFromUser()
        {
            Console.WriteLine("\nBitte geben Sie den gewünschten Dateinamen ein, z.B: Test.txt\nDateiname:");
            string FileName = Console.ReadLine();
            return FileName;
        }

        public static string ImportTextFromFile()
        {
            string importpath = GetPathFromUser() + GetFileNameFromUser();
            StreamReader sr = new StreamReader(importpath);
            string _text = sr.ReadToEnd();
            sr.Close();
            return _text;
        }

        public static void ExportTextToFile(string text)
        {
            string _path = GetPathFromUser() + GetFileNameFromUser();
            StreamWriter sw = new StreamWriter(_path);
            sw.WriteLine(text);
            Console.WriteLine("\nDie Datei: \n" +  _path + "\nwurde erfolgreich erstellt.");
            sw.Close();
        }

        public static string DefaultDirectory()
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine("Möchten Sie das aktuelle Verzeichnis benutzen?");

            string input = Console.ReadLine();
            if (input.Equals("1"))
            {
                return currentPath;
            }
            else
            {
                Console.WriteLine("\nBitte geben Sie den gewünschten Pfad ein, z.B: C:\nPfad:");
                string userPath = Console.ReadLine();
                if (userPath[userPath.Length - 1] == '\\')
                    return userPath;
                else
                    return userPath + "\\";
            }
        }

        public static void Main(string[] args)
        {
            Welcome();
            GetTextFromUser();
        }

    }
}
