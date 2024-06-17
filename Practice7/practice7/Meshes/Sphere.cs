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
    public class Sphere : IMesh
    {
        private Vector3[] vertices = { };
        private int[] indices = { };
        private Vector2[] texCoords = { };

        private int capSides = 100;
        private int sides = 50;

        public Sphere(float radius)
        {
            (var buf1, var buf2, var buf3) = MeshBuilder.BuildSphere(sides, radius, capSides);

            vertices = buf1.ToArray();
            indices = buf2.ToArray();
            texCoords = buf3.ToArray();
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
