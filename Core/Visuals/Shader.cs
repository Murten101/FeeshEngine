using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Feesh
{
    public class Shader
    {
         public int Handle;
         public Texture Texture;

        public void Use()
        {
            GL.UseProgram(Handle);
            GL.BindTexture(TextureTarget.Texture2D, Texture.Handle);
        }

        public int GetAttribute(string name)
        {
            int vertexLocation = GL.GetAttribLocation(Handle, name);
            GL.EnableVertexAttribArray(vertexLocation);
            return vertexLocation;
        }

        public int GetUniform(string name)
        {
            int location = GL.GetUniformLocation(Handle, name);
            return location;
        }

        public void SetMatrix4(string name, Matrix4 matrix)
        {
            int matrixLocation = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(matrixLocation, true, ref matrix);
        }

        public Shader(string vertexPath, string fragmentPath)
        {
            Utils.WriteLine("Reading shaders", ConsoleColor.Yellow);
            string vertexShaderSource;

            using (StreamReader reader = new StreamReader("Assets/Shaders/" + vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            string fragmentShaderSource;

            using (StreamReader reader = new StreamReader("Assets/Shaders/" + fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }
            Utils.WriteLine("Creating shaders", ConsoleColor.Yellow);
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            Utils.WriteLine("Compiling shaders", ConsoleColor.Yellow);

            GL.CompileShader(vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(vertexShader);
            if (infoLogVert != string.Empty)
            {
                Utils.WriteLine(infoLogVert, ConsoleColor.DarkRed);
            }

            GL.CompileShader(fragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

            if (infoLogFrag == string.Empty)
            {
                Utils.WriteLine(infoLogFrag, ConsoleColor.DarkRed);
            }

            Handle = GL.CreateProgram();

            Utils.WriteLine("Attaching shaders", ConsoleColor.Yellow);

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            Utils.WriteLine("Linking shaders", ConsoleColor.White);

            GL.LinkProgram(Handle);

            Utils.WriteLine("Cleaning up and detaching shaders", ConsoleColor.Yellow);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
