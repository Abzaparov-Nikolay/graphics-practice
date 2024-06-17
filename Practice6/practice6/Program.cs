using System;
using OpenTK.Windowing.Desktop;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace practice6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "myWindow",
                Size = new Vector2i(1200, 900)
            };

            using (var window = new Window(GameWindowSettings.Default, settings))
            {
                window.Run();
            };
        }
    }
}
