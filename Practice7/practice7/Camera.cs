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
using System.Windows.Input;
using System.Transactions;

namespace Practice7
{
    public class Camera
    {
        public Vector3 position;
        public Vector3 direction;
        private Vector3 upVector;

        private float pitch;
        private float yaw;

        public event EventHandler CameraMove;
        //public delegate void CameraMovedEventHandler();
        private float movementMultiplier = 0.5f;

        public Camera(Vector3 position, Vector3 direction, Vector3 upVector)
        {
            this.position = position;
            this.direction = direction;
            this.upVector = upVector;
            this.pitch = 0;
            this.yaw = -MathHelper.Pi /2 ;
        }

        public Matrix4 GetViewMatrix()
        {
            var view = Matrix4.LookAt(position, position + direction, upVector);
            return view;
        }

        public void MoveCamera(Vector3 translation)
        {
            var dir = Vector3.Normalize(direction);
            var up = Vector3.Normalize(upVector);

            var right = Vector3.Normalize(Vector3.Cross(up, dir));

            position += up * translation.Y * movementMultiplier;
            position += right * translation.X * movementMultiplier;
            position += dir * translation.Z * movementMultiplier;


            OnCameraMove();
        }

        public void RotateCamera(Vector2 rotation)
        {
            var dir4 = Vector3.One;
            pitch += rotation.Y;
            yaw += rotation.X;

            if (pitch > MathHelper.Pi/2.1)
                pitch = MathHelper.Pi / 2.1f;
            if (pitch < -MathHelper.Pi / 2.1)
                pitch = -MathHelper.Pi / 2.1f;

            dir4.X = MathF.Cos(yaw) * MathF.Cos(pitch);
            dir4.Y = -MathF.Sin(pitch);
            dir4.Z = MathF.Sin(yaw) * MathF.Cos(pitch);

            direction = Vector3.Normalize(dir4);
            var right = Vector3.Cross(direction, Vector3.UnitY).Normalized();
            upVector = Vector3.Cross(right, direction).Normalized();

            OnCameraMove();
        }

        private void OnCameraMove()
        {
            CameraMove?.Invoke(this, new EventArgs());
        }
    }
}
