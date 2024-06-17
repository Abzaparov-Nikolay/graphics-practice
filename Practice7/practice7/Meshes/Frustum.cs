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
    public class Frustum : IMesh
    {
        private int height;
        private int sides;
        float botRadius;
        float topScale;

        private Vector3[] vertices = { };
        private int[] indices = { };
        private Vector2[] texCoords = { };

        public Frustum(int height, int sides, float botRadius, float topScale, Vector3 topbotOffset)
        {
            this.height = height;
            this.sides = sides;
            this.botRadius = botRadius;
            this.topScale = topScale;

            (var buf1, var buf2, var buf3)=MeshBuilder.BuildWithTexture(sides, 
                botRadius, 
                new float[] { 1, topScale }, 
                false, 
                new Vector3[] { new Vector3(topbotOffset.X,-height/2, 0), new Vector3(-topbotOffset.X, height / 2, 0) }, 
                new Vector3[] { Vector3.Zero, Vector3.Zero });

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
