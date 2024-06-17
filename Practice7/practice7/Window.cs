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
using System.Runtime.InteropServices;
using System.IO;


namespace Practice7
{
    public class Window : GameWindow
    {
        private string vertPath = "shader.vert";
        private string fragPath = "shader.frag";
        private bool isLineMode = false;

        private string tex1 = "1.png";
        private string tex2 = "2.png";
        private string tex3 = "3.png";
        private string tex4 = "4.png";
        private string tex5 = "5.png";
        private string tex6 = "6.png";
        private string tex7 = "7.png";
        private string tex8 = "8.png";
        private string tex9 = "9.PNG";
        private string metal = "metal2.png";
        private string metal1 = "metal5.png";
        private string paint = "paint.png";
        private string pipe = "pipe.png";
        private string camo1 = "camo1.png";
        private string camo2 = "camo2.png";
        private string camo3 = "camo3.png";


        public static Shader shader;
        private GameObject[] figures;
        private int index;

        private Light light;
        private Camera camera;


        //private Vector3 FiguresRotationPoint = new Vector3(0, 0, -50);

        public Window(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings)
            : base(gameSettings, nativeSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();

            shader = new Shader(vertPath, fragPath);
            GL.ClearColor(Color4.Aquamarine);

            camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0));
            light = new Light(new Vector3(0, 0, 1), new Vector3(1, 1, 1));

            camera.CameraMove += OnCameraMove;

            AddFigures();
            SetUpGLSettings();
        }


        private void AddFigures()
        {
            var list = new List<GameObject>();


            //railgun 

            var pogtube2 = new GameObject(new Toroid(30, 5),camo1);
            pogtube2.transform.FromString("(27; -9,5; -80,5)\r\n(1,6399994; 0,42000043; 0,28000036)\r\n(22,04995; -28,699848; 0,5)\r\n");
            list.Add(pogtube2);

            var pogtube = new GameObject(new Toroid(30, 5),camo1);
            pogtube.transform.FromString("(27; -9,5; -80,5)\r\n(1,6399994; 0,42000043; 0,28000036)\r\n(-21,899952; -28,699848; 0)");
            list.Add(pogtube);

            var firetrube = new GameObject(new Frustum(80, 80, 30, 1.3f, Vector3.Zero), metal) ;
            firetrube.transform.FromString("(21; -11; -81,5)\r\n(0,3400004; 1,4799995; 0,30000037)\r\n(-1,7999994; -5,1000013; 0)\r\n");
            list.Add(firetrube);

            var stoon = new GameObject(new Toroid(20, 7), metal1);
            stoon.transform.FromString("(-22,5; -23; -100)\r\n(0,6600003; 1,7799993; 0,4200004)\r\n(1,3499998; -5,1000013; 0)\r\n");
            stoon.transform.myColor = Color4.Maroon;
            list.Add(stoon);


            var scope1 = new GameObject(new Sphere(5),tex4);
            scope1.transform.FromString("(-85; -34,5; -125,5)\r\n(0,4200009; 5,219996; 1,6599994)\r\n(1,3; 1,1999999; 0)\r\n");
            list.Add(scope1);

            var mainb = new GameObject(new Frustum(80, 4, 20, 1f, Vector3.Zero), tex2);
            mainb.transform.FromString("(-57; -29,5; -112,5)\r\n(0,6600003; 0,7800002; 0,74000025)\r\n(-1,1499999; 4,6499996; 0)\r\n");
            mainb.transform.WorldRotate(MathHelper.Pi / 4, 0, 0, mainb.transform.position);
            list.Add(mainb);

            var hadngrip = new GameObject(new Toroid(10, 3), tex2);
            hadngrip.transform.FromString("(-74; -47; -125)\r\n(0,94000006; 1; 0,6800003)\r\n(1,6499995; -0,3; 0)\r\n");
            list.Add(hadngrip);


            //horns

            var hornleft = new GameObject(new Toroid(10, 6));
            hornleft.transform.FromString("(23,5; 64,5; -191,5)\r\n(0,98; 1,5199995; 1,4199996)\r\n(-1,15; 1,4499997; 0)");
            hornleft.transform.myColor = Color4.YellowGreen;
            list.Add(hornleft);


            var hornright = new GameObject(new Toroid(10, 6));
            hornright.transform.FromString("(-23,5; 64,5; -191,5)\r\n(0,98; 1,5199995; 1,4199996)\r\n(-1,15; -1,4499997; 0)");
            hornright.transform.myColor = Color4.YellowGreen;
            list.Add(hornright);



            //hair batteries

            var battery3 = new GameObject(new Frustum(20, 50, 10, 0.4f, Vector3.Zero), tex8);
            battery3.transform.FromString("(32; 78,5; -221)\r\n(0,9200004; 1,6599998; 0,9200003)\r\n(3,5999978; 2,4499989; 0)\r\n");
            battery3.transform.myColor = Color4.LightYellow;
            list.Add(battery3);

            var hair3 = new GameObject(new Frustum(20, 20, 5, 1, Vector3.Zero));
            hair3.transform.FromString("(27; 64,5; -216)\r\n(0,16000037; 1; 0,26000035)\r\n(-0,7; 5,9000044; 0)\r\n");
            hair3.transform.myColor = Color4.CornflowerBlue;
            list.Add(hair3);

            var hair4 = new GameObject(new Frustum(20, 20, 5, 1, Vector3.Zero));
            hair4.transform.FromString("(25,5; 52; -224,5)\r\n(0,16000037; 1; 0,26000035)\r\n(2,399999; 5,9000044; 0)\r\n");
            hair4.transform.myColor = Color4.CornflowerBlue;
            list.Add(hair4);

            var battery4 = new GameObject(new Frustum(20, 50, 10, 0.4f, Vector3.Zero), tex8);
            battery4.transform.FromString("(34; 68; -238)\r\n(0,62000036; 1,2599998; 0,6200003)\r\n(3,9499974; 2,4499989; 0)\r\n");
            battery4.transform.myColor = Color4.LightYellow;
            list.Add(battery4);



            var hair2 = new GameObject(new Frustum(20, 20, 5, 1, Vector3.Zero));
            hair2.transform.FromString("(-25,5; 52; -224,5)\r\n(0,16000037; 1; 0,26000035)\r\n(-2,349999; -2,6999986; 0)\r\n");
            hair2.transform.myColor = Color4.CornflowerBlue;
            list.Add(hair2);

            var battery2 = new GameObject(new Frustum(20, 50, 10, 0.4f, Vector3.Zero), tex8);
            battery2.transform.FromString("(-34; 68; -238)\r\n(0,62000036; 1,2599998; 0,6200003)\r\n(-3,9499974; 0,6; 0)\r\n");
            battery2.transform.myColor = Color4.LightYellow;
            list.Add(battery2);

            var hair1 = new GameObject(new Frustum(20, 20, 5, 1, Vector3.Zero));
            hair1.transform.FromString("(-27; 64,5; -216)\r\n(0,16000037; 1; 0,26000035)\r\n(-2,7999985; -2,6999986; 0)");
            hair1.transform.myColor = Color4.CornflowerBlue;
            list.Add(hair1);

            var battery1 = new GameObject(new Frustum(20, 50, 10, 0.4f, Vector3.Zero), tex8);
            battery1.transform.FromString("(-32; 78,5; -221)\r\n(0,92000036; 1,6599998; 0,9200003)\r\n(-3,5999978; 0,9000001; 0)\r\n");
            battery1.transform.myColor = Color4.LightYellow;
            list.Add(battery1);


            //right leg

            var footballright = new GameObject(new Sphere(9), paint);
            footballright.transform.FromString("(52; -199,5; -156,5)\r\n(1; 1,0999999; 1,6999993)\r\n(0; 0; 0)\r\n");
            footballright.transform.myColor = Color4.Salmon;
            list.Add(footballright);

            var rightfoot = new GameObject(new Frustum(5, 14, 10, 0.3f, Vector3.Zero),tex4);
            rightfoot.transform.FromString("(51; -204; -155,5)\r\n(1,6999993; 3,4799976; 4,099997)\r\n(0; 0; 0)\r\n");
            rightfoot.transform.myColor = Color4.White;
            list.Add(rightfoot);

            var legboneright3 = new GameObject(new Frustum(30, 10, 20, 1.3f, Vector3.Zero), tex7);
            legboneright3.transform.FromString("(52; -177; -160,5)\r\n(0,24000035; 1,1999998; 0,24000035)\r\n(-0,2; 0; 0)\r\n");
            legboneright3.transform.myColor = Color4.White;
            list.Add(legboneright3);

            var legballright3 = new GameObject(new Sphere(6), tex8);
            legballright3.transform.FromString("(52; -157; -164,5)\r\n(1,2799997; 1,3199997; 1,1999998)\r\n(0; 0; 0)\r\n");
            legballright3.transform.myColor = Color4.White;
            list.Add(legballright3);

            var legboneright2 = new GameObject(new Toroid(10, 5), tex7);
            legboneright2.transform.FromString("(50,5; -132; -154,5)\r\n(0,42000043; 1,04; 1,7799993)\r\n(1,1999999; -3,299998; 0)\r\n");
            legboneright2.transform.myColor = Color4.White;
            list.Add(legboneright2);

            var lgeballright2 = new GameObject(new Sphere(9), tex8);
            lgeballright2.transform.FromString("(49,5; -108; -148)\r\n(1; 1; 1)\r\n(0; 0; 0)\r\n");
            lgeballright2.transform.myColor = Color4.White;
            list.Add(lgeballright2);

            var legboneright1 = new GameObject(new Toroid(20, 6),tex7);
            legboneright1.transform.FromString("(39; -91; -177)\r\n(0,48000047; 0,9200001; 1,6399994)\r\n(-0,49999982; -9,100017; 0)");
            legboneright1.transform.myColor = Color4.White;
            list.Add(legboneright1);


            //left leg

            var footballleft = new GameObject(new Sphere(9),paint);
            footballleft.transform.FromString("(-58; -199; -204)\r\n(1; 1,0999999; 1,6999993)\r\n(0; 0; 0)");
            footballleft.transform.myColor = Color4.Salmon;
            list.Add(footballleft);

            var leftfoot = new GameObject(new Frustum(5, 14, 10, 0.3f, Vector3.Zero),tex4);
            leftfoot.transform.FromString("(-58; -204; -203,5)\r\n(1,6999993; 3,4799976; 4,099997)\r\n(0; 0; 0)\r\n");
            leftfoot.transform.myColor = Color4.White;
            list.Add(leftfoot);

            var legboneleft3 = new GameObject(new Frustum(30, 10, 20, 1.3f, Vector3.Zero),tex7);
            legboneleft3.transform.FromString("(-57,5; -177; -203,5)\r\n(0,24000035; 1,1999998; 0,24000035)\r\n(0; 0; 0)\r\n");
            legboneleft3.transform.myColor = Color4.White;
            list.Add(legboneleft3);

            var legballleft3 = new GameObject(new Sphere(6),tex8);
            legballleft3.transform.FromString("(-57,5; -158,5; -203,5)\r\n(1,2799997; 1,3199997; 1,1999998)\r\n(0; 0; 0)\r\n");
            legballleft3.transform.myColor = Color4.White;
            list.Add(legballleft3);

            var legboneleft2 = new GameObject(new Toroid(10, 5),tex7);
            legboneleft2.transform.FromString("(-58,5; -143,5; -186)\r\n(0,42000043; 1,04; 1,7799993)\r\n(0,6500001; -3,299998; 0)");
            legboneleft2.transform.myColor = Color4.White;
            list.Add(legboneleft2);

            var lgeballleft2 = new GameObject(new Sphere(9),tex8);
            lgeballleft2.transform.FromString("(-58,5; -132,5; -171,5)\r\n(1; 1; 1)\r\n(0; 0; 0)\r\n");
            lgeballleft2.transform.myColor = Color4.White;
            list.Add(lgeballleft2);

            var legboneleft1 = new GameObject(new Toroid(20, 6),tex7);
            legboneleft1.transform.FromString("(-44,5; -101,5; -189,5)\r\n(0,48000047; 0,9200001; 1,6399994)\r\n(-0,90000004; -3,8499975; 0)\r\n");
            legboneleft1.transform.myColor = Color4.White;
            list.Add(legboneleft1);


            //balls

            var legballleft1 = new GameObject(new Sphere(9),tex8);
            legballleft1.transform.FromString("(-28,5; -75; -206)\r\n(1; 1; 1)\r\n(0; 0; 0)\r\n");
            list.Add(legballleft1);

            var legballright1 = new GameObject(new Sphere(9),tex8);
            legballright1.transform.FromString("(28,5; -75; -206)\r\n(1; 1; 1)\r\n(0; 0; 0)\r\n");
            list.Add(legballright1);


            //pilot door

            var pivoldoor = new GameObject(new Toroid(10, 30),tex8);
            pivoldoor.transform.FromString("(-0,5; 25,5; -177)\r\n(1; 1; 1)\r\n(-1,5575; -1,5075; 0)");
            list.Add(pivoldoor);


            //spine

            var spineBalls = new GameObject(new Sphere(20),tex4);
            spineBalls.transform.FromString("(0; -76; -212)\r\n(1,3399997; 0,84000015; 1)\r\n(0; 0; 0)\r\n");
            list.Add(spineBalls);

            var spine = new GameObject(new Toroid(10, 3), tex3);
            spine.transform.FromString("(0; -25; -214)\r\n(1,5599995; 3,079998; 3,2999978)\r\n(1,5999995; -6,7055225E-08; 0)\r\n");
            list.Add(spine);
            for (var i = 1; i < 5; i++)
            {
                var angle = 3.14 * 2 / 5;
                var newSpine = new GameObject(new Toroid(10, 3));
                newSpine.transform.myColor = Color4.Crimson;
                newSpine.transform.FromString("(0; -35; -211)\r\n(1,2599998; 3,079998; 3,2999978)\r\n(1,5999995; -6,7055225E-08; 0)");
                newSpine.transform.SelfRotate(0, (float)angle * i, 0);
                list.Add(newSpine);
            }


            //hands

            var shoulderleft = new GameObject(new Toroid(30, 10));
            shoulderleft.transform.FromString("(61; -3; -160)\r\n(0,18000036; 0,42000043; 1,0799999)\r\n(-2,5499988; -5,850004; 0)\r\n");
            shoulderleft.transform.myColor = Color4.YellowGreen;
            list.Add(shoulderleft);

            var elbowleft = new GameObject(new Sphere(7));
            elbowleft.transform.FromString("(75,5; -27,5; -128,5)\r\n(1,1; 1,1; 1,1)\r\n(0; 0; 0)\r\n");
            elbowleft.transform.myColor = Color4.YellowGreen;
            list.Add(elbowleft);

            var handleft = new GameObject(new Toroid(10, 5));
            handleft.transform.FromString("(63; -19,5; -105)\r\n(0,32000038; 0,8000002; 1,999999)\r\n(3,399998; -3,5999978; 0)\r\n");
            handleft.transform.myColor = Color4.YellowGreen;
            list.Add(handleft);

            var wristleft = new GameObject(new Sphere(7));
            wristleft.transform.FromString("(51; -15; -77)\r\n(0,94000006; 0,52000046; 1,6999993)\r\n(3,4999979; -0,24999996; 0)\r\n");
            wristleft.transform.myColor = Color4.YellowGreen;
            list.Add(wristleft);



            var wristright = new GameObject(new Sphere(7));
            wristright.transform.FromString("(-74; -46,5; -125)\r\n(0,94000006; 0,52000046; 1,6999993)\r\n(3,1499982; 0; 0)\r\n");
            wristright.transform.myColor = Color4.YellowGreen;

            list.Add(wristright);

            var handright = new GameObject(new Toroid(10, 5));
            handright.transform.FromString("(-74; -46,5; -154,5)\r\n(0,32000038; 0,8000002; 1,999999)\r\n(0; 0; 0)\r\n");
            handright.transform.myColor = Color4.YellowGreen;
            list.Add(handright);

            var elbowright = new GameObject(new Sphere(7));
            elbowright.transform.FromString("(-73; -43,5; -181)\r\n(1,1; 1,1; 1,1)\r\n(0; 0; 0)\r\n");
            elbowright.transform.myColor = Color4.YellowGreen;
            list.Add(elbowright);

            var shoulderright = new GameObject(new Toroid(30, 10));
            shoulderright.transform.FromString("(-59,5; -9; -185)\r\n(0,18000036; 0,42000043; 1,0799999)\r\n(-1,9999992; -1,1999999; 0)\r\n");
            shoulderright.transform.myColor = Color4.YellowGreen;
            list.Add(shoulderright);


            //body?

            var bodypilotTor2 = new GameObject(new Toroid(50, 2));
            bodypilotTor2.transform.FromString("(26,5; 14,5; -192,5)\r\n(0,96000004; 1,3599997; 0,7000003)\r\n(1,2; 1,6; 0)");
            bodypilotTor2.transform.myColor = Color4.LightSteelBlue;
            list.Add(bodypilotTor2);

            var bodypilotTor1 = new GameObject(new Toroid(50, 2));
            bodypilotTor1.transform.FromString("(-26,5; 14,5; -192,5)\r\n(0,96000004; 1,3599997; 0,7000003)\r\n(1,2; -1,6; 0)");
            bodypilotTor1.transform.myColor = Color4.LightSteelBlue;
            list.Add(bodypilotTor1);

            var backpack = new GameObject(new Frustum(60, 4, 30, 1, Vector3.Zero), tex2);
            backpack.transform.FromString("(0; 33; -221,5)\r\n(1; 1; 1)\r\n(0; 0,7500001; 0)");
            list.Add(backpack);

            var eye = new GameObject(new Sphere(5));
            eye.transform.FromString("(0; 34,5; -134,5)\r\n(1,2599998; 1,2399998; 1)\r\n(0; 0; 0)\r\n");
            eye.transform.myColor = Color4.Yellow;
            list.Add(eye);

            var eyeTor = new GameObject(new Toroid(5, 2));
            eyeTor.transform.FromString("(0; 34; -143)\r\n(1,3399997; 5,339996; 1,3399997)\r\n(-1,57075; 0; 0)\r\n");
            eyeTor.transform.myColor= Color4.SteelBlue;
            list.Add(eyeTor);


            var bodyhandtor1 = new GameObject(new Toroid(12, 3));
            bodyhandtor1.transform.FromString("(-40,5; 18,5; -192)\r\n(1,5599995; 1,3999996; 1,4199996)\r\n(1,57; -1,57; 0)");
            bodyhandtor1.transform.myColor = Color4.Crimson;
            list.Add(bodyhandtor1);

            var bodyhandtor2 = new GameObject(new Toroid(12, 3));
            bodyhandtor2.transform.FromString("(40,5; 18,5; -192)\r\n(1,5599995; 1,3999996; 1,4199996)\r\n(1,57; -1,57; 0)");
            bodyhandtor2.transform.myColor = Color4.Crimson;
            list.Add(bodyhandtor2);


            var bodyhandSphere1 = new GameObject(new Sphere(10));
            bodyhandSphere1.transform.FromString("(-42; 19; -192)\r\n(1,4; 1,4; 1,4)\r\n(0; 0; 0)\r\n");
            bodyhandSphere1.transform.myColor = Color4.YellowGreen;
            list.Add(bodyhandSphere1);

            var bodyhandSphere2 = new GameObject(new Sphere(10));
            bodyhandSphere2.transform.FromString("(42; 19; -192)\r\n(1,4; 1,4; 1,4)\r\n(0; 0; 0)\r\n");
            bodyhandSphere2.transform.myColor = Color4.YellowGreen;
            list.Add(bodyhandSphere2);



            var body = new GameObject(new Toroid(30, 40),pipe);
            body.transform.FromString("(0; 18,5; -192)\r\n(0,72000027; 1; 0,72000027)\r\n(-1,5707; -1,5707; 0)\r\n");
            list.Add(body);

            
            figures = list.ToArray();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (var figure in figures)
            {
                figure.Draw();
            }
            //this.Title = $"pos{figures[index].transform.position}  scale{figures[index].transform.scale}  selfrot{figures[index].transform.selfRotation}";

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;
            var mouse = MouseState;
            if (input.IsKeyReleased(Keys.Escape))
            {
                File.WriteAllLines("pogtasty.txt", new string[] { figures[index].transform.position.ToString(),
                        figures[index].transform.scale.ToString(),
                        figures[index].transform.selfRotation.ToString()});
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

            #region figure debug
            //if (input.IsKeyDown(Keys.Up))
            //{
            //    figures[index].transform.SelfRotate(-0.05f, 0, 0);
            //}
            //if (input.IsKeyDown(Keys.Down))
            //{
            //    figures[index].transform.SelfRotate(0.05f, 0, 0);
            //}
            //if (input.IsKeyDown(Keys.Left))
            //{
            //    figures[index].transform.SelfRotate(0, -0.05f, 0);
            //}
            //if (input.IsKeyDown(Keys.Right))
            //{
            //    figures[index].transform.SelfRotate(0, 0.05f, 0);
            //}


            //if (input.IsKeyDown(Keys.W))
            //{
            //    figures[index].transform.AddTranslation(0, 0.5f, 0);
            //}
            //if (input.IsKeyDown(Keys.S))
            //{
            //    figures[index].transform.AddTranslation(0, -0.5f, 0);
            //}
            //if (input.IsKeyDown(Keys.A))
            //{
            //    figures[index].transform.AddTranslation(-0.5f, 0, 0);
            //}
            //if (input.IsKeyDown(Keys.D))
            //{
            //    figures[index].transform.AddTranslation(0.5f, 0, 0);
            //}
            //if (input.IsKeyDown(Keys.Q))
            //{
            //    figures[index].transform.AddTranslation(0, 0, -0.5f);
            //}
            //if (input.IsKeyDown(Keys.E))
            //{
            //    figures[index].transform.AddTranslation(0, 0, 0.5f);
            //}


            //if (input.IsKeyDown(Keys.KeyPad4))
            //{
            //    figures[index].transform.Scale(-0.02f, 0, 0);
            //}
            //if (input.IsKeyDown(Keys.KeyPad6))
            //{
            //    figures[index].transform.Scale(0.02f, 0, 0);
            //}
            //if (input.IsKeyDown(Keys.KeyPad8))
            //{
            //    figures[index].transform.Scale(0, 0.02f, 0);
            //}
            //if (input.IsKeyDown(Keys.KeyPad2))
            //{
            //    figures[index].transform.Scale(0, -0.02f, 0);
            //}
            //if (input.IsKeyDown(Keys.KeyPad5))
            //{
            //    figures[index].transform.Scale(0, 0, 0.02f);
            //}
            //if (input.IsKeyDown(Keys.KeyPad7))
            //{
            //    figures[index].transform.Scale(0, 0, -0.02f);
            //}
            #endregion

            if (mouse.IsButtonDown(MouseButton.Right))
            {
                camera.MoveCamera(new Vector3(MouseState.Delta));

            }
            if (mouse.IsButtonDown(MouseButton.Left))
            {
                camera.RotateCamera(mouse.Delta / 200);
                
            }
            camera.MoveCamera(new Vector3(0, 0, mouse.ScrollDelta.Y * 6));
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

            shader.SetVector3("lightPos", light.Position);
            shader.SetVector3("lightColor", light.Color);
            shader.SetVector3("viewPos", camera.position);
            shader.SetMatrix4("view", camera.GetViewMatrix());
        }

        public void OnCameraMove(object sender, EventArgs e)
        {
            shader.Use();
            shader.SetVector3("lightPos", camera.position);
            shader.SetVector3("lightColor", light.Color);
            shader.SetVector3("viewPos", camera.position);
            shader.SetMatrix4("view", camera.GetViewMatrix());
        }
    }
}
