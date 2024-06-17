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
    public class Cuboid : IMesh
    {
        private Vector3[] vertices = { };
        private int[] indices = { };

        private int length;
        private int width;
        private int height;

        public Cuboid(int length, int width, int height)
        {

            this.length = length;
            this.width = width;
            this.height = height;

            MakeVertices();
            MakeIndices();

        }

        private void MakeVertices()
        {
            var verticesList = new List<Vector3>();

            for (var yMultiplier = -1; yMultiplier < 2; yMultiplier += 2)
            {
                verticesList.Add(new Vector3(width / 2, yMultiplier * height / 2, length / 2));
                verticesList.Add(new Vector3(-width / 2, yMultiplier * height / 2, length / 2));
                verticesList.Add(new Vector3(-width / 2, yMultiplier * height / 2, -length / 2));
                verticesList.Add(new Vector3(width / 2, yMultiplier * height / 2, -length / 2));
            }
            vertices = verticesList.ToArray();
        }

        private void MakeIndices()
        {
            var indicesList = new List<int>();
            foreach (var i in new int[2] { 0, 4 })
            {
                indicesList.AddRange(GetSideIndices(new int[4] { i, i + 1, i + 2, i + 3 }, i == 0));
            }

            for (var i = 0; i < 3; i++)
            {
                indicesList.AddRange(GetSideIndices(new int[4] { i, i + 1, i + 5, i + 4 }, false));
            }
            indicesList.AddRange(GetSideIndices(new int[4] { 3, 0, 4, 7 }, false));

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

    }
}
