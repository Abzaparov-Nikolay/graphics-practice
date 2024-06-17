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
    public class MeshBuilder
    {
        public static (List<Vector3>, List<int>) Build(
            int caps,
            int sides,
            float radius,
            float[] capScales,
            bool isFirstLastConnected,
            Vector3[] capOffsets,
            Vector3[] capRotations)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            for (var cap = 0; cap < caps; cap++)
            {
                //creating
                var vertBuf = CreateCapVertices(sides, radius * capScales[cap]);
                vertices.AddRange(MoveCap(vertBuf, capOffsets[cap], capRotations[cap]));

                if ((cap == 0 || cap == caps - 1) && !isFirstLastConnected) // drawing
                {
                    indices.AddRange(CloseCap(Enumerable.Range(sides * cap, sides).ToArray(), cap == 0));
                }
                if (cap != 0) //connection
                {
                    indices.AddRange(ConnectCaps(Enumerable.Range(sides * (cap - 1), sides).ToArray(),
                        Enumerable.Range(sides * cap, sides).ToArray()));
                }
                if (cap == caps - 1 && isFirstLastConnected) //connect last and first
                {
                    indices.AddRange(ConnectCaps(Enumerable.Range(sides * (cap), sides).ToArray(),
                        Enumerable.Range(0, sides).ToArray()));
                }
            }
            return (vertices, indices);
        }


        public static (Vector3[], int[]) BuildIcosahedron(float sideLen)
        {
            var vertList = new List<Vector3>();
            var indList = new List<int>();
            var X = (float)0.525731112119133606 * sideLen/2;
            var Z = (float)0.850650808352039932 * sideLen /2;
            vertList.Add(new Vector3(-X, 0, Z));
            vertList.Add(new Vector3(X, 0, Z));
            vertList.Add(new Vector3(-X, 0, -Z));
            vertList.Add(new Vector3(X, 0, -Z));
            vertList.Add(new Vector3(0, Z, X));
            vertList.Add(new Vector3(0, Z, -X));
            vertList.Add(new Vector3(0, -Z, X));
            vertList.Add(new Vector3(0, -Z, -X));
            vertList.Add(new Vector3(Z, X, 0));
            vertList.Add(new Vector3(-Z, X, 0));
            vertList.Add(new Vector3(Z, -X, 0));
            vertList.Add(new Vector3(-Z, -X, 0));

            indList.AddRange(new int[] { 0, 4, 1,
                0, 9, 4,
                9, 5, 4,
                4, 5, 8,
                4, 8, 1,
                8, 10, 1,
                8, 3, 10,
                5, 3, 8,
                5, 2, 3,
                2, 7, 3,
                7, 10, 3,
                7, 6, 10,
                7, 11, 6,
                11, 0, 6,
                0, 1, 6,
                6, 1, 10,
                9, 0, 11,
                9, 11, 2,
                9, 2, 5,
                7, 2, 11 });

            return (vertList.ToArray(), indList.ToArray());
        }

        public static (Vector3[], int[]) BuildDedothahedron(float sideLen)
        {
            var vertList = new List<Vector3>();
            var indicesList = new List<int>();
            var capRadius = (float)(sideLen / 2 / MathHelper.Sin(MathHelper.Pi / 2.5));
            var goldenRation = (float)(1 + MathHelper.Sqrt(5)) / 2;
            var rGolRat = 1 / goldenRation;

            vertList.Add(new Vector3(0, rGolRat * sideLen / 2, goldenRation * sideLen / 2));
            vertList.Add(new Vector3(0, -rGolRat * sideLen / 2, goldenRation * sideLen / 2));
            vertList.Add(new Vector3(0, -rGolRat * sideLen / 2, -goldenRation * sideLen / 2));
            vertList.Add(new Vector3(0, rGolRat * sideLen / 2, -goldenRation * sideLen / 2));
            vertList.Add(new Vector3(goldenRation * sideLen / 2, 0, rGolRat * sideLen / 2));
            vertList.Add(new Vector3(-goldenRation * sideLen / 2, 0, rGolRat * sideLen / 2));
            vertList.Add(new Vector3(-goldenRation * sideLen / 2, 0, -rGolRat * sideLen / 2));
            vertList.Add(new Vector3(goldenRation * sideLen / 2, 0, -rGolRat * sideLen / 2));
            vertList.Add(new Vector3(rGolRat * sideLen / 2, goldenRation * sideLen / 2, 0));
            vertList.Add(new Vector3(-rGolRat * sideLen / 2, goldenRation * sideLen / 2, 0));
            vertList.Add(new Vector3(-rGolRat * sideLen / 2, -goldenRation * sideLen / 2, 0));
            vertList.Add(new Vector3(rGolRat * sideLen / 2, -goldenRation * sideLen / 2, 0));
            vertList.Add(new Vector3(1 * sideLen / 2, 1 * sideLen / 2, 1 * sideLen / 2));
            vertList.Add(new Vector3(-1 * sideLen / 2, 1 * sideLen / 2, 1 * sideLen / 2));
            vertList.Add(new Vector3(-1 * sideLen / 2, -1 * sideLen / 2, 1 * sideLen / 2));
            vertList.Add(new Vector3(1 * sideLen / 2, -1 * sideLen / 2, 1 * sideLen / 2));
            vertList.Add(new Vector3(1 * sideLen / 2, -1 * sideLen / 2, -1 * sideLen / 2));
            vertList.Add(new Vector3(1 * sideLen / 2, 1 * sideLen / 2, -1 * sideLen / 2));
            vertList.Add(new Vector3(-1 * sideLen / 2, 1 * sideLen / 2, -1 * sideLen / 2));
            vertList.Add(new Vector3(-1 * sideLen / 2, -1 * sideLen / 2, -1 * sideLen / 2));

            indicesList.AddRange(GetSideIndices(new int[] { 1 - 1, 2 - 1, 16 - 1, 5 - 1, 13 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 1 - 1, 13 - 1, 9 - 1, 10 - 1, 14 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 1 - 1, 14 - 1, 6 - 1, 15 - 1, 2 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 2 - 1, 15 - 1, 11 - 1, 12 - 1, 16 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 3 - 1, 4 - 1, 18 - 1, 8 - 1, 17 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 3 - 1, 17 - 1, 12 - 1, 11 - 1, 20 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 3 - 1, 20 - 1, 7 - 1, 19 - 1, 4 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 19 - 1, 10 - 1, 9 - 1, 18 - 1, 4 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 16 - 1, 12 - 1, 17 - 1, 8 - 1, 5 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 5 - 1, 8 - 1, 18 - 1, 9 - 1, 13 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 14 - 1, 10 - 1, 19 - 1, 7 - 1, 6 - 1 }, true).ToArray());
            indicesList.AddRange(GetSideIndices(new int[] { 6 - 1, 7 - 1, 20 - 1, 11 - 1, 15 - 1 }, true).ToArray());
            return (vertList.ToArray(), indicesList.ToArray());
        }

        public static (Vector3[], int[]) BuildSphere(int sides, float radius, int capSides)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            vertices = MakeSphereVertices(sides, radius, capSides);

            for (var side = 0; side < sides; side++)
            {
                if (side != 0) //connection
                {
                    indices.AddRange(MakeTouches(Enumerable.Range(2 + side * (capSides - 2), capSides - 2).ToArray(),
                        Enumerable.Range(2 + (side - 1) * (capSides - 2), capSides - 2).ToArray()));
                    indices.AddRange(new int[] { 0, 2 + side * (capSides - 2), 2 + (side - 1) * (capSides - 2) });
                    indices.AddRange(new int[] { 1, 1 + (side) * (capSides - 2), 1 + (side + 1) * (capSides - 2) });
                }
                if (side == sides - 1) //connect last and first
                {
                    indices.AddRange(MakeTouches(Enumerable.Range(2, capSides - 2).ToArray(),
                        Enumerable.Range(2 + side * (capSides - 2), capSides - 2).ToArray()));
                    indices.AddRange(new int[] { 0, 2 + 0 * (capSides - 2), 2 + side * (capSides - 2) });
                    indices.AddRange(new int[] { 1, 1 + (side + 1) * (capSides - 2), 1 + 1 * (capSides - 2) });
                }
            }
            return (vertices.ToArray(), indices.ToArray());


            List<int> MakeTouches(int[] first, int[] second)
            {
                var indicesList = new List<int>();
                for (var i = 0; i < first.Length - 1; i++)
                {
                    indicesList.AddRange(GetSideIndices(new int[] { first[i], second[i], second[i + 1], first[i + 1] }, true));
                }
                return indicesList;
            }
        }
        private static List<Vector3> MakeSphereVertices(int sides, float radius, int capSides)
        {
            var angle = MathHelper.Pi * 2 / sides;
            var capAngle = MathHelper.Pi / capSides;
            var vertices = new List<Vector3>();
            vertices.Add(new Vector3(radius, 0, 0));
            vertices.Add(new Vector3(-radius, 0, 0));
            var capVerts = new List<Vector3>();

            for (var capPoint = 1; capPoint < capSides - 1; capPoint++)
            {
                var vert = new Vector3(radius * (float)MathHelper.Cos(capAngle * capPoint),
                    0,
                    radius * (float)MathHelper.Sin(capAngle * capPoint));
                capVerts.Add(vert);
            }
            vertices.AddRange(capVerts);
            for (var side = 1; side < sides; side++)
            {
                vertices.AddRange(MoveCap(capVerts, Vector3.Zero, new Vector3(angle * side, 0, 0)));
            }
            return vertices;
        }

        private static List<Vector3> CreateCapVertices(int sides, float radius)
        {
            var angle = MathHelper.Pi * 2 / sides;
            var vertList = new List<Vector3>();
            for (var i = 0; i < sides; i++)
            {
                var vert = new Vector4((float)(radius * Math.Cos(angle * i)),
                                          0,
                                          (float)(radius * Math.Sin(angle * i)),
                                          1f);
                vertList.Add(vert.Xyz);
            }
            return vertList;
        }

        private static List<Vector3> MoveCap(List<Vector3> capVerts, Vector3 translation, Vector3 rotation)
        {
            var movedVerts = new List<Vector3>();
            foreach (var vert in capVerts)
            {
                var v = new Vector4(vert, 1);
                v *= Matrix4.CreateRotationX(rotation.X);
                v *= Matrix4.CreateRotationY(rotation.Y);
                v *= Matrix4.CreateRotationZ(rotation.Z);
                v += new Vector4(translation, 1);
                movedVerts.Add(v.Xyz);
            }
            return movedVerts;
        }

        private static List<int> ConnectCaps(int[] firstCap, int[] secondCap)
        {
            var indicesList = new List<int>();
            for (var i = 0; i < firstCap.Length - 1; i++)
            {
                indicesList.AddRange(GetSideIndices(new int[] { firstCap[i], secondCap[i], secondCap[i + 1], firstCap[i + 1] }, true));
            }
            indicesList.AddRange(GetSideIndices(new int[] {
                firstCap[firstCap.Length-1],
                secondCap[secondCap.Length-1],
                secondCap[0],
                firstCap[0] }, true));
            return indicesList;
        }

        private static List<int> CloseCap(int[] capIndices, bool reverse)
        {
            return GetSideIndices(capIndices, reverse);
        }

        private static List<int> GetSideIndices(int[] indices, bool reverse)
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
    }
}
