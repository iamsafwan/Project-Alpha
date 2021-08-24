using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace Project_Alpha.Services
{
    class PasswordServices
    {
       
        List<Action> functions = new List<Action>();

        private static readonly string[] Capital_Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static readonly string[] Small_Alphabets = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static readonly string[] Numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        private static readonly string[] SpecialCharacters = { " ", "!", "/", "#", "@", "$", "%", "^", "&", "_", "+", "=", "-", "'", "(", ")", "{", "}", "[", "]", "\\", "|", ",", "<", ">", ".", "?", ";", ":", "`", "~", "X", "Y", "Z" };

        //alternatives to generating random strings
        //public static readonly string[] Characters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", " ", "!", "/", "#", "@", "$", "%", "^", "&", "_", "+", "=", "-", "'", "(", ")", "{", "}", "[", "]", "\\", "|", ",", "<", ">", ".", "?", ";", ":", "`", "~" };

        //private static readonly string[] RandomFunction = { "CARandom", "SARandom", "SPRandom", "NRandom", "CARandom", "SARandom", "SPRandom", "NRandom" };
        //look how to add """


        //public void randomfunctionselector()
        //{
        //    functions.Add(RandomFunction[RRandom.Next(9).ToString()]);


        //    foreach (Action func in functions)
        //        func();
        //}

        //    public static string CARandom(int number)
        //{
        //    string generated = string.Empty;
        //    for (int i = 0; i < number; i++)
        //    {
        //     //   var ss = RRandom.Next(9);
        //        var dd = 
        //        generated = generated + Capital_Alphabets[i];
        //        Debug.WriteLine(generated);
        //    }

        //    return generated;
        //}

        //public static void SARandom(int number)
        //{

        //    for (int i = 0; i < number; i++)
        //    {
        //        Debug.WriteLine(Small_Alphabets[i]);
        //    }
        //}

        //public static void SPRandom(int number)
        //{

        //    for (int i = 0; i < number; i++)
        //    {
        //        Debug.WriteLine(SpecialCharacters[i]);
        //    }
        //}

        //public static void NRandom(int number)
        //{

        //    for (int i = 0; i < number; i++)
        //    {
        //        Debug.WriteLine(Numbers[i]);
        //    }
        //}

       
        public static string Generator(int length)
        {
            Random RRandom = new Random();
            string generated = string.Empty;

            while (generated.Length != length)
            {
                switch (RRandom.Next(3))
                {
                    case 0:
                        generated += PasswordServices.Numbers[RRandom.Next(10)];
                        break;
                    case 1:
                        generated += PasswordServices.Small_Alphabets[RRandom.Next(26)];
                        break;
                    case 2:
                        generated += PasswordServices.SpecialCharacters[RRandom.Next(34)];
                        break;
                    case 3:
                        generated += PasswordServices.Capital_Alphabets[RRandom.Next(26)];
                        break;

                    default:
                        Debug.WriteLine("Generation failed try again add exception here");
                        break;


                }

            }
            if (generated != string.Empty)
            {
                return generated;
            }
            else
            {
                generated = "Error";
                return generated;
            }
           


            //method one half generate random numbers for index method
            //string randomnumbers = string.Empty;
            //for (int i = 0; i < length - 1; i++)
            //{
            //    randomnumbers += RRandom.Next(length + 1);
            //}
            //Debug.WriteLine(randomnumbers);

            //method2 but no randomizing
            //while (generated.Length != length)
            //{
            //    generated += Capital_Alphabets[RRandom.Next(27)];
            //    generated += Small_Alphabets[RRandom.Next(27)];
            //    generated += Numbers[RRandom.Next(11)];
            //    generated += SpecialCharacters[RRandom.Next(35)];
            //}
           

           

        }

    }
}
