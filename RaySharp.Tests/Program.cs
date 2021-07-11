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
        
            for (int i = 0; i < 1000; i++)
                Console.WriteLine($"hello {Window.Monitor} {Monitor.GetPhysicalSize(Window.Monitor)}");
            await Task.Delay(2000);

            Window.Close();
        }
    }
}
