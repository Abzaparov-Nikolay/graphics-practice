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
using System.IO;
using System.Drawing;

namespace Practice7
{
    public class Toroid : IMesh
    {
        private Vector3[] vertices = { };
        private int[] indices = { };
        private Vector2[] texCoords = { };

        private int caps = 100;
        private int sides = 50;

        public Toroid(float toroidRadius, float capRadius)
        {
            var offsetsList = new List<Vector3>();
            var rotationsList = new List<Vector3>();
            var scalesList = new List<float>();

            var angle = MathHelper.Pi * 2 / caps;
            for (var cap = 0; cap < caps; cap++)
            {


                var offset = new Vector3((float)(toroidRadius  * MathHelper.Cos(angle * cap)),0, (float)(toroidRadius  * MathHelper.Sin(angle * cap)));

                var rotation = new Vector3((float)(Math.PI / 2), -angle * cap, 0);


                offsetsList.Add(offset);
                rotationsList.Add(rotation);
                scalesList.Add(1f);
                //scalesList.Add((float)(1f + 0.5*MathHelper.Cos(angle*2*cap)));
            }

            (var buf1, var buf2, var buf3) = MeshBuilder.Build(caps,
                sides,
                capRadius,
                scalesList.ToArray(),
                true,
                offsetsList.ToArray(),
                rotationsList.ToArray());

            vertices = buf1.ToArray();
            indices = buf2.ToArray();
            texCoords= buf3.ToArray();

        }

        public int[] GetIndices()
        {
            return indices.ToArray();
        }

        public Vector3[] GetVertices()
        {
            return vertices.ToArray();
        }

        public Vector2[] GetTextureCoords()
        {
            return texCoords.ToArray();
        }
    }
}
