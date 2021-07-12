using System;
using System.Numerics;
using System.Threading.Tasks;

namespace RaySharp.Tests
{
    class Program
    {
        static async Task Main(string[] _)
        {
            Window.Init(500, 500, "test");

            //Window.Monitor = 1;
            Window.Size = new Vector2(1000, 720);
            //Window.Position = new Vector2(100, 100);

            while (!Window.ShouldClose)
            {
                Window.BeginDrawing();
                Window.ClearBackground(Color.Black);
                Window.EndDrawing();
            }

            Window.Close();
        }
    }
} 
