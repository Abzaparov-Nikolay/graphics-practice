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

namespace practice6
{
    public class Spiral :IMesh
    {
        private Vector3[] vertices = { };
        private int[] indices = { };

        private int caps = 500;
        private int sides = 100;

        public Spiral(float spiralRadius, float capRadius, float repetitions)
        {
            var offsetsList = new List<Vector3>();
            var rotationsList = new List<Vector3>();
            var scalesList = new List<float>();

            var angle = MathHelper.Pi *2 / caps;
            for (var cap = 0; cap < caps; cap++)
            {
                var offset = new Vector3((float)(spiralRadius * MathHelper.Cos(repetitions * angle * cap)),
                    -caps/4 + cap*0.5f, 
                    (float)(spiralRadius * MathHelper.Sin( repetitions*angle * cap)));

                var rotation = new Vector3((float)(Math.PI / 2), -angle* repetitions * cap, 0);

                offsetsList.Add(offset);
                rotationsList.Add(rotation);
                scalesList.Add(1f);
                //scalesList.Add((float)(1f + 0.5*MathHelper.Cos(angle*2*cap)));
            }

            (var buf1, var buf2) = MeshBuilder.Build(caps,
                sides,
                capRadius,
                scalesList.ToArray(),
                false,
                offsetsList.ToArray(),
                rotationsList.ToArray());

            vertices = buf1.ToArray();
            indices = buf2.ToArray();
        }

        public int[] GetIndices()
        {
            return indices.ToArray();
        }

        public Vector3[] GetVertices()
        {
            return vertices.ToArray();
        }
    }
}
