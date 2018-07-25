using System;
using System.Linq;
using System.Threading;

namespace EnigmaConsole
{
    public static class Program
    {
        public static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static Plugboard P = new Plugboard();
        static RotorDials R = new RotorDials();

        static void Main(string[] args)
        {
            Console.WriteLine("Enigma Console 2018\nRyota B. Okura");
            Console.WriteLine("========================================");
            int ct = Console.CursorTop;
            Console.WriteLine("Initializing settings...");
            P.SetPorts();
            R.SetRotors();
            Console.WriteLine("\nInitializing complete.");
            Console.WriteLine("========================================\n");

            Wait();
            ClearConsole(ct);

            while (true)
            {
                switch (MainMenu())
                {
                    case 1:
                        ClearConsole(ct);
                        Enigma();
                        break;
                    case 2:
                        ClearConsole(ct);
                        Plugboard();
                        break;
                    case 3:
                        ClearConsole(ct);
                        RotorDials();
                        break;
                    default:
                        return;
                }
                
                ClearConsole(ct);
            }
        }

        static int MainMenu()
        {
            Console.WriteLine("\nMain Menu Controls:\n\tF1 - Use Enigma Machine | F2 - Plugboard Manager | F3 - Rotors Manager | Esc - Quit");
            Console.Write("\n\n:>>");

            ConsoleKey input;
            ConsoleKey[] functions = { ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.Escape };
            while (!functions.Contains(input = Console.ReadKey(true).Key))
            {
            }
            Console.WriteLine(input.ToString());

            if (input == ConsoleKey.F1)
                return 1;
            else if (input == ConsoleKey.F2)
                return 2;
            else if (input == ConsoleKey.F3)
                return 3;
            else if (input == ConsoleKey.Escape)
                return 0;
            else
                return 0;
        }

        static void Enigma()
        {
            int ct = Console.CursorTop;
            ConsoleKeyInfo input = new ConsoleKeyInfo();
            string ciphertext = "";
            do
            {
                ClearConsole(ct);
                Console.WriteLine("\n| " + IntToChar(R.RotorPositions[0]) + " | " +
                    IntToChar(R.RotorPositions[1]) + " | " +
                    IntToChar(R.RotorPositions[2]) + " | " +
                    IntToChar(R.RotorPositions[3]) + " |\n");
                Console.Write(ciphertext);

                if (input.Key != 0)
                {

                }
            } while ((input = Console.ReadKey(true)).Key != ConsoleKey.Escape);
        }

        static void Plugboard()
        {
            if (P.PortsSet)
                P.ViewPorts();
            else
            {
                Console.WriteLine("\nPlugs not set!");
                P.SetPorts();
                Console.WriteLine();
                P.ViewPorts();
            }
        }

        static void RotorDials()
        {
            if (R.RotorsSet)
                R.ViewRotors();
            else
            {
                Console.WriteLine("\nRotors not set!");
                R.SetRotors();
                Console.WriteLine();
                R.ViewRotors();
            }
        }

        public static string GetIntRankingString(int num)
        {
            string numString = num.ToString();
            return numString.EndsWith("1") ? num + "st" :
                numString.EndsWith("2") ? num + "nd" :
                numString.EndsWith("3") ? num + "rd" : num + "th";
        }


        public static int CharToInt(char c)
        {
            return Alphabet.IndexOf(c);
        }

        public static char IntToChar(int i)
        {
            return Program.Alphabet[i];
        }

        public static void ClearConsole(int startIndex)
        {
            int ct = Console.CursorTop;
            for (int i = startIndex; i < ct; i++)
            {
                Console.CursorTop = i;
                for (int i2 = 0; i2 < 100; i2++)
                    Console.Write('\0');
            }

            Console.CursorTop = startIndex;
            Console.CursorLeft = Console.WindowTop = 0;
        }

        public static void Wait()
        {
            Console.CursorVisible = false;
            Thread.Sleep(1000);
            Console.CursorVisible = true;
        }
    }
}
