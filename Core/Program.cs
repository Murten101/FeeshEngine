using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;

namespace Feesh
{
    class Program
    {
        private static Game game;
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Init();
        }

        private void Init()
        {
            using (game = new Game("Feesh"))
            {
                game.VSync = VSyncMode.Off;
                game.Run();
            }
        }

    }
}
