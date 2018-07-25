using System;
using System.Linq;

namespace EnigmaConsole
{
    public class Plugboard
    {
        public char[,] Ports = new char[10, 2];
        public bool PortsSet { get { return Ports[0, 0] != '\0'; } }
        private string usedLetters = "";

        public Plugboard() { }

        public void SetPorts()
        {
            Console.WriteLine("\nSetting up Plugboard...");
            int ct = Console.CursorTop;
            usedLetters = "";
            
            for (int i = 0; i < Ports.GetLength(0); i++)
            {
                Console.WriteLine("\n\n\t" + Program.GetIntRankingString(i + 1) + " plug:>");
                Console.Write("\t\tInput first letter port:>>");
                char input;
                string msg = "   Letter port taken!";
                while (usedLetters.Contains(input = getLetterInput()))
                {
                    Console.Write(msg);
                    Console.CursorLeft -= msg.Length;
                }
                usedLetters += Ports[i,0] = input;

                Console.Write(input + "\t\t\t\t\n\t\tInput second letter port:>>");
                while (usedLetters.Contains(input = getLetterInput()))
                {
                    Console.Write(msg);
                    Console.CursorLeft -= msg.Length;
                }
                usedLetters += Ports[i, 1] = input;
                Console.WriteLine(input);

                Program.ClearConsole(ct);
            }
            Console.WriteLine();
            Program.ClearConsole(ct);
            Console.WriteLine("\nPlugboard setup complete.");
        }

        public void ViewPorts()
        {
            while (true)
            {
                int ct = Console.CursorTop;
                Console.WriteLine("\nPlugboard Settings:\n--------------------");
                for (int x = 0; x < 10; x++)
                    Console.WriteLine("\t" + Ports[x, 0] + " <=====> " + Ports[x, 1]);

                Console.WriteLine("\nControls:\n\tF1 - Setup Plugboard | Esc - Back to Main Menu");
                Console.Write("\n\n:>>");

                ConsoleKey input;
                do
                    input = Console.ReadKey(true).Key;
                while (input != ConsoleKey.F1 && input != ConsoleKey.Escape);
                Console.WriteLine(input.ToString());

                Program.ClearConsole(ct);
                if (input == ConsoleKey.F1)
                {
                    SetPorts();
                    Program.Wait();
                    Program.ClearConsole(ct);
                }
                else
                    break;
            }
        }

        public char Passthrough(char c)
        {
            for (int i = 0; i < Ports.GetLength(0); i++)
            {
                if (Ports[i, 0] == c)
                    return Ports[0, 1];
                else if (Ports[i, 1] == c)
                    return Ports[i, 0];
            }
            return c;
        }

        private char getLetterInput()
        {
            ConsoleKeyInfo input;
            while (!Program.Alphabet.Contains((input = Console.ReadKey(true)).KeyChar.ToString().ToUpper())) { }

            return input.KeyChar.ToString().ToUpper()[0];
        }
    }
}
