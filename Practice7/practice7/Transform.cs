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

namespace Practice7
{
    public class Transform
    {
        public Color4 myColor;

        protected Matrix4 translateMatrix;
        protected Matrix4 selfRotationMatrix;
        protected Matrix4 worldRotationMatrix;
        protected Matrix4 scaleMatrix;

        public Matrix4 modelMatrix;
        protected Matrix4 projection;
        protected Matrix4 view;
        public Vector3 pivot;

        public Vector3 position;
        public Vector3 selfRotation;
        public Vector3 worldRotation;
        public Vector3 scale;

        public Transform()
        {
            modelMatrix = Matrix4.Identity;

            translateMatrix = Matrix4.Identity;
            selfRotationMatrix = Matrix4.Identity;
            worldRotationMatrix = Matrix4.Identity;
            scaleMatrix = Matrix4.Identity;

            view = Matrix4.Identity;
            projection = Matrix4.Identity;

            pivot = Vector3.Zero;
            myColor = Color4.DarkOliveGreen;

            position = Vector3.Zero;
            worldRotation = Vector3.Zero;
            selfRotation = Vector3.Zero;
            scale = new Vector3(1, 1, 1);
        }

        public void Translate(float x, float y, float z)
        {
            position += new Vector3(x, y, z);

            translateMatrix = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
        }

        public void AddTranslation(float x, float y, float z)
        {
            position += new Vector3(x, y, z);

            translateMatrix = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
        }

        public void Scale(float x, float y, float z)
        {
            scale += new Vector3(x, y, z);

            scaleMatrix = Matrix4.CreateScale(scale.X, scale.Y, scale.Z);
        }

        public void SetScale(Vector3 vector)
        {
            scale = vector;

            scaleMatrix = Matrix4.CreateScale(scale.X, scale.Y, scale.Z);
        }

        public void WorldRotate(float xRadians, float yRadians, float zRadians, Vector3 rotPoint)
        {
            pivot = rotPoint;
            worldRotation += new Vector3(xRadians, yRadians, zRadians);

            worldRotationMatrix = Matrix4.CreateRotationX(worldRotation.X);
            worldRotationMatrix *= Matrix4.CreateRotationY(worldRotation.Y);
            worldRotationMatrix *= Matrix4.CreateRotationZ(worldRotation.Z);
        }

        public void SelfRotate(float xRadians, float yRadians, float zRadians)
        {
            selfRotation += new Vector3(xRadians, yRadians, zRadians);

            selfRotationMatrix = Matrix4.CreateRotationX(selfRotation.X);
            selfRotationMatrix *= Matrix4.CreateRotationY(selfRotation.Y);
            selfRotationMatrix *= Matrix4.CreateRotationZ(selfRotation.Z);

        }

        public void UpdateTransformMatrix()
        {
            modelMatrix = Matrix4.Identity;

            modelMatrix *= scaleMatrix;
            modelMatrix *= selfRotationMatrix;


            modelMatrix *= Matrix4.CreateTranslation(pivot - position);
            modelMatrix *= worldRotationMatrix;
            modelMatrix *= Matrix4.CreateTranslation(-pivot + position);

            modelMatrix *= translateMatrix;
        }
    }
}
