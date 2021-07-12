using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace RaySharp.Tests
{
    class Program
    {
        static async Task Main(string[] _)
        {
            Window.Init(new Vector2(500, 500), "test");
            Camera3D camera = new Camera3D();

            camera.Mode = Camera3D.CameraMode.FREE;

            while (!Window.ShouldClose)
            {
                Window.BeginDrawing();
                Window.ClearBackground(Color.Black);

                camera.Begin();

                camera.End();

                new Rectangle(new Vector2(100, 100), new Vector2(300, 100)).DrawGradient(Color.RayWhite, Color.DarkBlue, Color.DarkGreen, Color.Gold);
                //new Rectangle(new Vector2(100, 100), new Vector2(300, 100)).Draw(Color.RayWhite, new Vector2(150, 50), 90);

                Window.EndDrawing();
            }

            Window.Close();
        }
    }
} 
