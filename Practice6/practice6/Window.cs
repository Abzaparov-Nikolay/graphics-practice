
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
using System.Windows.Input;

namespace practice6
{
    public class Window : GameWindow
    {
        private string vertPath = "shader.vert";
        private string fragPath = "shader.frag";
        private bool isLineMode = false;

        public static Shader shader;
        public GameObject[] figures;
        public int index;

        public Window(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings)
            : base(gameSettings, nativeSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            shader = new Shader(vertPath, fragPath);
            GL.ClearColor(Color4.Aquamarine);
            var view = Matrix4.LookAt(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0));
            shader.Use();
            shader.SetMatrix4("view", view);
            SetUpGLSettings();
            AddFigures();
        }

        private void AddFigures()
        {
            var figuresList = new List<GameObject>();


            var figure1 = new GameObject(new Cuboid(20, 10, 30)); //параллелепипед
            figure1.transform.Rotate(0, 0, 0, Vector3.Zero);
            figure1.transform.Translate(0, 10, -40);
            figuresList.Add(figure1);

            var paral = new GameObject(new Frustum(10, 4, 20, 1, new Vector3(5, 0, 0)));//скошен паралл
            paral.transform.Translate(0, 0, -50);
            figuresList.Add(paral);

            var figure2 = new GameObject(new Pyramid(10, 10, 30)); //пирамида
            figure2.transform.Rotate(0, 0, 0, Vector3.Zero);
            figure2.transform.Translate(0, 10, -40);
            figuresList.Add(figure2);

            var conus = new GameObject(new Pyramid(100, 10, 30)); //конус
            conus.transform.Rotate(0, 0, 0, Vector3.Zero);
            conus.transform.Translate(0, 10, -40);
            figuresList.Add(conus);

            var figure3 = new GameObject(new Frustum(20, 5, 30, 0.3f, Vector3.Zero)); // усеченная пирамида
            figure3.transform.Rotate(0, 0, 0, Vector3.Zero);
            figure3.transform.Translate(0, 10, -80);
            figuresList.Add(figure3);

            var figure4 = new GameObject(new Toroid(50f, 20f));  //пончик
            figure4.transform.Rotate(MathHelper.Pi / 2, 0, 0, Vector3.Zero);
            figure4.transform.Translate(0, 0, -150);
            figuresList.Add(figure4);

            var cylinder = new GameObject(new Frustum(20, 100, 7, 1, Vector3.Zero)); // цилиндр
            cylinder.transform.Rotate(0, 0, 0, Vector3.Zero);
            cylinder.transform.Translate(0, 5, -50);
            figuresList.Add(cylinder);

            var sphere = new GameObject(new Sphere(15)); // сфера
            sphere.transform.Rotate(0, 0, 0, Vector3.Zero);
            sphere.transform.Translate(0, 5, -50);
            figuresList.Add(sphere);

            var spiral = new GameObject(new Spiral(20, 5, 10)); // спираль
            spiral.transform.Rotate(0, 0, 0, Vector3.Zero);
            spiral.transform.Translate(0, 5, -150);
            figuresList.Add(spiral);

            var icosahedron = new GameObject(new Icosahedron(30)); // икосахедрон
            icosahedron.transform.Rotate(0, 0, 0, Vector3.Zero);
            icosahedron.transform.Translate(0, 10, -60);
            figuresList.Add(icosahedron);

            var dodecahedron = new GameObject(new Dodecahedron(30)); // додекаэдр
            dodecahedron.transform.Rotate(0, 0, 0, Vector3.Zero);
            dodecahedron.transform.Translate(0, 10, -160);
            figuresList.Add(dodecahedron);

            var cube = new GameObject(new Cuboid(20, 20, 20)); // гексадр
            cube.transform.Translate(0, 0, -50);
            figuresList.Add(cube);

            var a = 20;
            var R = a / (float)MathHelper.Sqrt(3);
            var sin = R / a;
            var arcsin = MathHelper.Asin(sin);
            var h = a * (float)MathHelper.Cos(arcsin);
            var tetraedr = new GameObject(new Pyramid(3, R, h));
            tetraedr.transform.Translate(0, 0, -30);
            figuresList.Add(tetraedr);

            figures = figuresList.ToArray();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            figures[index].Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;
            if (input.IsKeyReleased(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyReleased(Keys.L))
            {
                if (isLineMode)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                else
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                isLineMode = !isLineMode;
            }
            if (input.IsKeyReleased(Keys.KeyPadSubtract))
            {
                index = index <= 0 ? index : index - 1;
            }
            if (input.IsKeyReleased(Keys.KeyPadAdd))
            {
                index = index + 1 >= figures.Length ? index : index + 1;
            }

            if (input.IsKeyDown(Keys.Up))
            {
                figures[index].transform.AddRotate(-0.05f, 0, 0);
            }
            if (input.IsKeyDown(Keys.Down))
            {
                figures[index].transform.AddRotate(0.05f, 0, 0);
            }
            if (input.IsKeyDown(Keys.Left))
            {
                figures[index].transform.AddRotate(0, -0.05f, 0);
            }
            if (input.IsKeyDown(Keys.Right))
            {
                figures[index].transform.AddRotate(0, 0.05f, 0);
            }

            if (input.IsKeyDown(Keys.W))
            {
                figures[index].transform.AddTranslation(0, 0.5f, 0);
            }
            if (input.IsKeyDown(Keys.S))
            {
                figures[index].transform.AddTranslation(0, -0.5f, 0);
            }
            if (input.IsKeyDown(Keys.A))
            {
                figures[index].transform.AddTranslation(-0.5f, 0, 0);
            }
            if (input.IsKeyDown(Keys.D))
            {
                figures[index].transform.AddTranslation(0.5f, 0, 0);
            }
            if (input.IsKeyDown(Keys.Q))
            {
                figures[index].transform.AddTranslation(0, 0, -0.5f);
            }
            if (input.IsKeyDown(Keys.E))
            {
                figures[index].transform.AddTranslation(0, 0, 0.5f);
            }

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, (float)Size.X / Size.Y, 0.1f, 800, out var projection);
            shader.Use();
            shader.SetMatrix4("projection", projection);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        private void SetUpGLSettings()
        {
            shader.Use();
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.DepthClamp);
            GL.DepthFunc(DepthFunction.Less);
            GL.DepthMask(true);
            GL.CullFace(CullFaceMode.Front);

            shader.Use();
            shader.SetVector3("lightPos", new Vector3(0, 0, 0));
            shader.SetVector3("lightColor", new Vector3(1, 1, 1));
            shader.SetVector3("viewPos", new Vector3(0, 0, 0));
            //shader.SetMatrix4("view", camera.GetViewMatrix());
        }
    }
}
