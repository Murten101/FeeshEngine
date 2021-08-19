using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK.Mathematics;

namespace Feesh
{
    static class Utils
    {
        public static Vector2 ScreentSpaceToNdc(Vector2 screenCoors, Game game)
        {
            Vector2 coords = new Vector2(screenCoors.X / game.Size.X * 2 - 1, screenCoors.Y / game.Size.Y * 2 - 1);
            return coords;
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
