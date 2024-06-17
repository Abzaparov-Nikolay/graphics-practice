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
using System.Reflection.Metadata.Ecma335;
using Practice7;

namespace Practice7
{
    public class Renderer
    {
        Shader shader;
        private int vao;
        private int ebo;
        GameObject gameObject;


        private int[] indices;
        private float[] vertices;
        private float[] normals;
        private float[] texCoords;
        private Transform transform;

        private float[] vboArray;

        public Renderer(GameObject gameobject)
        {
            this.gameObject = gameobject;
            Vector3[] buf1;
            int[] buf2;
            Vector2[] buf3;


            (buf1, buf2, buf3) = VerticesMultiplier.MultiplyVertices(gameObject.mesh.GetVertices(), gameObject.mesh.GetIndices(), gameobject.mesh.GetTextureCoords());

            this.vertices = buf1.SelectMany(vector => new float[] { vector.X, vector.Y, vector.Z }).ToArray();
            this.indices = buf2;
            this.normals = VerticesMultiplier.GetNormals(buf1, buf2).SelectMany(vector => new float[] { vector.X, vector.Y, vector.Z }).ToArray();
            this.texCoords = buf3.SelectMany(vector => new float[] { vector.X, vector.Y }).ToArray();
            this.transform = gameObject.transform;

            CreateVBOArray();

            shader = Window.shader;

            SetUpVao();
        }

        private void SetUpVao()
        {
            var vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(BufferTarget.ArrayBuffer, vboArray.Length * sizeof(float), vboArray, BufferUsageHint.StaticDraw);
            var posAttLocation = shader.GetAttribLocation("aPosition");
            var normAttLocation = shader.GetAttribLocation("normalCoords");
            var texAttLocation = shader.GetAttribLocation("texCoords");

            GL.VertexAttribPointer(posAttLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(normAttLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(texAttLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);


            shader.Use();
            if (gameObject.texture != null)
                gameObject.texture.Use();
            shader.SetVector4("aColor", (Vector4)transform.myColor);
            shader.SetMatrix4("model", transform.modelMatrix);
            GL.UseProgram(0);
        }

        public void Draw()
        {
            transform.UpdateTransformMatrix();
            shader.Use();
            if (gameObject.texture != null)
            {
                gameObject.texture.Use();
                shader.SetBool("useTexture", 1);
            }
            else
            {
                shader.SetBool("useTexture", 0);
            }
            shader.SetMatrix4("model", transform.modelMatrix);
            shader.SetVector4("aColor", (Vector4)transform.myColor);
            GL.BindVertexArray(vao);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.UseProgram(0);
            GL.BindVertexArray(0);
        }

        private void CreateVBOArray()
        {
            var vbo = new List<float>();
            for (var vertex = 0; vertex < indices.Length; vertex++)
            {
                vbo.Add(vertices[3 * vertex]);
                vbo.Add(vertices[3 * vertex + 1]);
                vbo.Add(vertices[3 * vertex + 2]);

                vbo.Add(normals[3 * vertex]);
                vbo.Add(normals[3 * vertex + 1]);
                vbo.Add(normals[3 * vertex + 2]);

                if (texCoords.Length > 0)
                {
                    vbo.Add(texCoords[2 * vertex]);
                    vbo.Add(texCoords[2 * vertex + 1]);
                }
                else
                {
                    vbo.Add(0);
                    vbo.Add(0);
                }
            }
            vboArray = vbo.ToArray();
        }
    }
}
