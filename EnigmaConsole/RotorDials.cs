using System;
using System.Linq;

namespace EnigmaConsole
{
    public class RotorDials
    {
        public RotorConfig[] Rotors = new RotorConfig[4];
        public int[] RotorPositions = new int[4];
        public ReflectorConfig Reflector;
        public bool RotorsSet { get { return Rotors[0] != null; } }

        public RotorDials() { }

        public void SetRotors()
        {
            Console.WriteLine("\nSetting up Rotors...");
            int ct = Console.CursorTop;

            for (int i = 0; i < Rotors.Length; i++)
            {
                Console.WriteLine("\n\n\t" + Program.GetIntRankingString(i + 1) + " rotor:>");
                Console.Write("\t\tPick rotor number (1, 2, 3, 4, 5, 6, 7, 8):>>");
                char input;
                while (!"12345678".Contains(input = Console.ReadKey(true).KeyChar)) { }
                Rotors[i] = new RotorConfig(int.Parse(input.ToString()));

                Console.Write(input + "\n\t\tSet rotor orientation:>>");
                while (!Program.Alphabet.Contains(input = Console.ReadKey(true).KeyChar.ToString().ToUpper()[0])) { }
                RotorPositions[i] = Program.CharToInt(input);
                Console.WriteLine(input);

                Program.ClearConsole(ct);
            }
            Console.WriteLine();
            Program.ClearConsole(ct);
            Console.WriteLine("\nRotors setup complete.");
        }

        public void ViewRotors()
        {
            while (true)
            {
                int ct = Console.CursorTop;
                Console.WriteLine("\nRotor Dials Settings:\n--------------------");

                Console.WriteLine("\t|| Rotor # | Orientation | ABCDEFGHIJKLMNOPQRSTUVWXYZ | Notch A | Notch B ||");
                Console.WriteLine("\t++---------+-------------+----------------------------+---------+---------++");
                for (int i = 0; i < Rotors.Length; i++)
                    Console.WriteLine("\t|| " + 
                        Rotors[i].Number.ToString().PadRight(7) + " | " +
                        Program.IntToChar(RotorPositions[i]).ToString().PadLeft(11) + " | " + 
                        Rotors[i].AToZ + " | " +
                        Rotors[i].NotchA.ToString().PadLeft(7) + " | " +
                        (Rotors[i].NotchB != '\0' ? Rotors[i].NotchB : '-').ToString().PadLeft(7) + " ||");
                
                Console.WriteLine("\n\nControls:\n\tF1 - Setup Rotors | Esc - Back to Main Menu");
                Console.Write("\n\n:>>");

                ConsoleKey input;
                do
                    input = Console.ReadKey(true).Key;
                while (input != ConsoleKey.F1 && input != ConsoleKey.Escape);
                Console.WriteLine(input.ToString());

                Program.ClearConsole(ct);
                if (input == ConsoleKey.F1)
                {
                    SetRotors();
                    Program.Wait();
                    Program.ClearConsole(ct);
                }
                else
                    break;
            }
        }

        public char PassThrough(char c, int index)
        {
            if (index < Rotors.Length - 1)
            {
                char nextRotor = PassThrough(Rotors[index].AToZ[Program.CharToInt(c)], index++);
                return Rotors[index].AToZ[Program.CharToInt(nextRotor)];
            }
            else
            {
                char reflector = 
                return Rotors[index].AToZ[Program.CharToInt(PassThrough(Rotors[index].AToZ[Program.CharToInt(c)], index++))];
            }

        }
        //0-25
        private void rotorStep(int rotorIndex)
        {
            if ((Program.CharToInt(Rotors[rotorIndex].NotchA) == RotorPositions[0] ||
                Program.CharToInt(Rotors[rotorIndex].NotchB) == RotorPositions[0]) &&
                rotorIndex < Rotors.Length - 1)
                rotorStep(rotorIndex + 1);

            if (RotorPositions[rotorIndex] < 25)
                RotorPositions[rotorIndex]++;
            else
                RotorPositions[rotorIndex] = 0;
        }
    }

    public class RotorConfig
    {
        public RotorNum Number { get; }
        public string AToZ
        {
            get
            {
                switch (Number)
                {
                    case RotorNum.I:
                        return "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
                    case RotorNum.II:
                        return "AJDKSIRUXBLHWTMCQGZNPYFVOE";
                    case RotorNum.III:
                        return "BDFHJLCPRTXVZNYEIWGAKMUSQO";
                    case RotorNum.IV:
                        return "ESOVPZJAYQUIRHXLNFTGKDCMWB";
                    case RotorNum.V:
                        return "VZBRGITYUPSDNHLXAWMJQOFECK";
                    case RotorNum.VI:
                        return "JPGVOUMFYQBENHZRDKASXLICTW";
                    case RotorNum.VII:
                        return "NZJHGRCXMYSWBOUFAIVLPEKQDT";
                    case RotorNum.VIII:
                        return "FKQHTLXOCBJSPDZRAMEWNIUYGV";
                    default:
                        return "";
                }
            }
        }
        public char NotchA
        {
            get
            {
                switch (Number)
                {
                    case RotorNum.I:
                        return 'Q';
                    case RotorNum.II:
                        return 'E';
                    case RotorNum.III:
                        return 'V';
                    case RotorNum.IV:
                        return 'J';
                    case RotorNum.V:
                        return 'Z';
                    case RotorNum.VI:
                        return 'Z';
                    case RotorNum.VII:
                        return 'Z';
                    case RotorNum.VIII:
                        return 'Z';
                    default:
                        return '\0';
                }
            }
        }
        public char NotchB
        {
            get
            {
                switch (Number)
                {
                    case RotorNum.I:
                        return '\0';
                    case RotorNum.II:
                        return '\0';
                    case RotorNum.III:
                        return '\0';
                    case RotorNum.IV:
                        return '\0';
                    case RotorNum.V:
                        return '\0';
                    case RotorNum.VI:
                        return 'M';
                    case RotorNum.VII:
                        return 'M';
                    case RotorNum.VIII:
                        return 'M';
                    default:
                        return '\0';
                }
            }
        }

        public RotorConfig(int num)
        {
            switch (num)
            {
                case 1:
                    Number = RotorNum.I;
                    break;
                case 2:
                    Number = RotorNum.II;
                    break;
                case 3:
                    Number = RotorNum.III;
                    break;
                case 4:
                    Number = RotorNum.IV;
                    break;
                case 5:
                    Number = RotorNum.V;
                    break;
                case 6:
                    Number = RotorNum.VI;
                    break;
                case 7:
                    Number = RotorNum.VII;
                    break;
            }
        }

        public enum RotorNum { I, II, III, IV, VI, V, VII, VIII }
    }

    public class ReflectorConfig
    {
        public ReflectorID ID { get; }
        public string AToZ
        {
            get
            {
                switch (ID)
                {
                    case ReflectorID.A:
                        return "EJMZALYXVBWFCRQUONTSPIKHGD";
                    case ReflectorID.B:
                        return "YRUHQSLDPXNGOKMIEBFZCWVJAT";
                    case ReflectorID.C:
                        return "FVPJIAOYEDRZXWGCTKUQSBNMHL";
                    default:
                        return "";
                }
            }
        }

        public ReflectorConfig(ReflectorID id) { ID = id; }

        public enum ReflectorID { A, B, C }
    }
}
