using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Assimp;
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

        public static List<Vector3> ConvertV3DToV3(List<Vector3D> input)
        {
            List<Vector3> output = new List<Vector3>(input.Count);

            foreach (var vector3D in input)
            {
                output.Add(new Vector3(vector3D.X, vector3D.Y, vector3D.Z));
            }

            return output;
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}