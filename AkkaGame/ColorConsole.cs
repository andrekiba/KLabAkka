using System.Drawing;
using Console = Colorful.Console;

namespace AkkaGame
{
    public static class ColorConsole
    {
        public static void WriteWhite(string message)
        {
            Console.WriteLine(message, Color.White);
        }

        public static void WriteRed(string message)
        {
            Console.WriteLine(message, Color.Red);
        }

        public static void WriteGreen(string message)
        {
            Console.WriteLine(message, Color.MediumSeaGreen);
        }

        public static void WriteOrange(string message)
        {
            //var beforeColor = Console.ForegroundColor;

            //Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message, Color.DarkOrange);

           //Console.ForegroundColor = beforeColor;
        }

        public static void WriteViolet(string message)
        {
            //var beforeColor = Console.ForegroundColor;

            //Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message, Color.BlueViolet);

            //Console.ForegroundColor = beforeColor;
        }
    }
}
