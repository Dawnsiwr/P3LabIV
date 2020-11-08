using System;

namespace P3LabIV
{
    public class DataReader
    {
        public string readSentence()
        {
            string sentence;
            do
            {
                sentence = Console.ReadLine();
            } while (sentence == null);

            return sentence;
        }
        public int readIntValue()
        {
            bool valueError = false;
            int value = 0;
            do
            {
                valueError = false;
                if (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.Write("Niepoprawna wartość");
                    valueError = true;
                    continue;
                }

            } while (valueError);

            return value;
        }

        public int readIntValue(int range)
        {
            bool valueError = false;
            int value = 0;
            do
            {
                valueError = false;
                if (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.Write("Niepoprawna wartość");
                    valueError = true;
                    continue;
                }

            } while (valueError || value>range || value < 1);

            return value;
        }
    }
}