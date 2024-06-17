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
    public class Shader
    {

        private int ShaderProgram;

        public Shader(string vertPath, string fragPath)
        {
            var vertShaderPath = vertPath;
            var fragShaderPath = fragPath;

            var vertShaderSource = File.ReadAllText(vertShaderPath);
            var fragShaderSource = File.ReadAllText(fragShaderPath);

            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShader, vertShaderSource);
            GL.ShaderSource(fragmentShader, fragShaderSource);

            GL.CompileShader(vertexShader);
            GL.CompileShader(fragmentShader);

            ShaderProgram = GL.CreateProgram();
            GL.AttachShader(ShaderProgram, vertexShader);
            GL.AttachShader(ShaderProgram, fragmentShader);

            GL.LinkProgram(ShaderProgram);
        }

        public void Use()
        {
            GL.UseProgram(ShaderProgram);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(ShaderProgram, attribName);
        }

        public int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(ShaderProgram, uniformName);
        }

        public void SetMatrix4(string uniformName, Matrix4 matrix)
        {
            var location = GL.GetUniformLocation(ShaderProgram, uniformName);
            GL.UniformMatrix4(location, true, ref matrix);
        }

        public void SetVector4(string uniformName, Vector4 vector)
        {
            var location = GL.GetUniformLocation(ShaderProgram, uniformName);
            GL.Uniform4(location, vector);
        }

        public void SetVector3(string uniformName, Vector3 vector)
        {
            var location = GL.GetUniformLocation(ShaderProgram, uniformName);
            GL.Uniform3(location, vector);
        }
    }
}
