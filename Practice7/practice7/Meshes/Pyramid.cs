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
    public class Pyramid : IMesh
    {
        private Vector3[] vertices = { };
        private int[] indices = { };

        private int sides;
        private float radius;
        private float height;

        public Pyramid(int sides, float radius, float height)
        {

            this.sides = sides;
            this.radius = radius;
            this.height = height;

            MakeVertices(Vector3.Zero);
            MakeIndices();
        }

        private void MakeVertices(Vector3 offset)
        {
            var verticesList = new List<Vector3>();

            verticesList.Add(new Vector3(0, height / 2, 0));
            var angle = 2 * MathHelper.Pi / sides;
            for (var i = 0; i < sides; i++)
            {
                verticesList.Add(new Vector3((float)(offset.X + radius * Math.Cos(angle * i)),
                                              offset.Y - height / 2,
                                              (float)(offset.Z + radius * Math.Sin(angle * i))));
            }
            vertices = verticesList.ToArray();

        }

        private void MakeIndices()
        {
            var indicesList = new List<int>();

            for (var i = 1; i < sides; i++)
            {
                indicesList.Add(0);
                indicesList.Add(i);
                indicesList.Add(i + 1);
            }
            indicesList.Add(0);
            indicesList.Add(sides);
            indicesList.Add(1);

            indicesList.AddRange(GetSideIndices(Enumerable.Range(1, sides).ToArray(), true));

            indices = indicesList.ToArray();
        }

        private List<int> GetSideIndices(int[] indices, bool reverse)
        {
            var indicesList = new List<int>();
            for (int i = 0; i < indices.Length - 2; i++)
            {
                indicesList.Add(indices[0]);
                if (reverse)
                {
                    indicesList.Add(indices[i + 2]);
                    indicesList.Add(indices[i + 1]);
                }
                else
                {
                    indicesList.Add(indices[i + 1]);
                    indicesList.Add(indices[i + 2]);
                }
            }
            return indicesList;
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
