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
            Cursor.Enabled = true;
            Cursor.Hidden = false;
            while (!Window.ShouldClose)
                Console.WriteLine($"{Cursor.IsOnScreen}");

            Window.Close();
        }
    }
} 
