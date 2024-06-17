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
    public class Dodecahedron : IMesh
    {
        private Vector3[] vertices = { };
        private int[] indices = { };

        public Dodecahedron(float sideLen)
        {
            (var buf1, var buf2) = MeshBuilder.BuildDedothahedron(sideLen);

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
        public Vector2[] GetTextureCoords()
        {
            return null;
        }
    }
}
