using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;


namespace practice6
{
    public static class VerticesMultiplier
    {
        public static (Vector3[], int[], Vector2[]) MultiplyVertices(Vector3[] vertices, int[] indices, Vector2[] texCoords)
        {
            var vertList = new List<Vector3>();
            var indList = new List<int>();
            var texList = new List<Vector2>();
            for (var trioStart = 0; trioStart < indices.Length; trioStart += 3)
            {
                for (var vertNum = 0; vertNum < 3; vertNum++)
                {
                    vertList.Add(vertices[indices[trioStart + vertNum]]);
                    indList.Add(trioStart + vertNum);
                    if (texCoords != null)
                        texList.Add(texCoords[indices[trioStart + vertNum]]);
                }
            }
            return (vertList.ToArray(), indList.ToArray(), texList.ToArray());
        }

        public static Vector3[] GetNormals(Vector3[] vertices, int[] indices)
        {
            var normalList = new List<Vector3>();
            for (var trioStart = 0; trioStart < indices.Length; trioStart += 3)
            {
                var p1 = vertices[trioStart];
                var p2 = vertices[trioStart + 1];
                var p3 = vertices[trioStart + 2];

                var dir = Vector3.Cross((p2 - p1), (p3 - p1));
                var norm = Vector3.Normalize(dir);
                normalList.Add(norm);
                normalList.Add(norm);
                normalList.Add(norm);
            }
            return normalList.ToArray();
        }
    }
}
