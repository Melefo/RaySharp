using RaySharp.Camera;
using RaySharp.Textures;
using RaySharp.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace RaySharp.Tests
{
    class Program
    {
        static void Main(string[] _)
        {
            Window.Init(new(500, 500), "test");
            Camera3D camera = new()
            {
                Mode = Camera3D.CameraMode.FREE
            };

            Random rand = new();

            Image test = new("test.png");
            test.ColorGrayscale();
            test.Export("grey.png");
            test.LoadColors();

            List<Triangle> list = new();

            for (int i = 0; i < 100; i++)
                list.Add(new(new (rand.Next(0, 500), rand.Next(0, 500)), new(rand.Next(0, 500), rand.Next(0, 500)), new(rand.Next(0, 500), rand.Next(0, 500))));

            while (!Window.ShouldClose)
            {
                Window.BeginDrawing();
                Window.ClearBackground(Color.Black);

                camera.Begin();

                camera.End();
                list.DrawFan(Color.Red);

                //new Ellipse(new Vector2(200, 200), 100, 50, Color.Blue).DrawLines();
                //new Ring(new Vector2(250, 250), 100, 50, 0, 360, 20, Color.Gold).Draw();

                //new Shapes.Line(new Vector2(10, 10), new Vector2(210, 50), Color.Red, 5).Draw();
                //new Shapes.Line(new Vector2(10, 20), new Vector2(210, 170), Color.Red, 5).DrawBezierQuad(new Vector2(10, 20));
                //new Shapes.Rectangle(new Vector2(100, 100), new Vector2(300, 100)).DrawGradient(Color.RayWhite, Color.DarkBlue, Color.DarkGreen, Color.Gold);
                //new Rectangle(new Vector2(100, 100), new Vector2(300, 100)).Draw(Color.RayWhite, new Vector2(150, 50), 90);

                Window.EndDrawing();
            }

            Window.Close();
        }
    }
} 
