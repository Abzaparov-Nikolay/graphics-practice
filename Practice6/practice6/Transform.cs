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

namespace practice6
{
    public class Transform
    {
        public Color4 myColor;

        protected Matrix4 translateMatrix;
        protected Matrix4 rotationMatrix;
        protected Matrix4 scaleMatrix;

        public Matrix4 modelMatrix;
        protected Matrix4 projection;
        protected Matrix4 view;
        Vector3 pivot;

        public Transform()
        {
            modelMatrix = Matrix4.Identity;

            translateMatrix = Matrix4.Identity;
            rotationMatrix = Matrix4.Identity;
            scaleMatrix = Matrix4.Identity;

            view = Matrix4.Identity;
            projection = Matrix4.Identity;

            pivot = Vector3.Zero;

            myColor = Color4.Crimson;

        }

        public void Translate(float x, float y, float z)
        {
            translateMatrix = Matrix4.CreateTranslation(x, y, z);
        }

        public void AddTranslation(float x, float y, float z)
        {
            translateMatrix *= Matrix4.CreateTranslation(x, y, z); 
        }

        public void Scale(float x, float y, float z)
        {
            scaleMatrix = Matrix4.CreateScale(x, y, z);
        }

        public void Rotate(float xRadians, float yRadians, float zRadians, Vector3 rotPoint)
        {
            pivot = rotPoint;
            rotationMatrix = Matrix4.CreateRotationX(xRadians);
            rotationMatrix *= Matrix4.CreateRotationY(yRadians);
            rotationMatrix *= Matrix4.CreateRotationZ(zRadians);
        }

        public void AddRotate(float xRadians, float yRadians, float zRadians)
        {
            rotationMatrix *= Matrix4.CreateRotationX(xRadians);
            rotationMatrix *= Matrix4.CreateRotationY(yRadians);
            rotationMatrix *= Matrix4.CreateRotationZ(zRadians);
        }

        public void UpdateTransformMatrix()
        {
            modelMatrix = Matrix4.Identity;
            modelMatrix *= scaleMatrix;

            modelMatrix *= Matrix4.CreateTranslation(pivot);
            modelMatrix *= rotationMatrix;
            modelMatrix *= Matrix4.CreateTranslation(-pivot);

            modelMatrix *= translateMatrix;
        }
    }
}
