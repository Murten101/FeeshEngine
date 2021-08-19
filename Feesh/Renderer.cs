using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using ObjRenderer;
using OpenTK.Mathematics;

namespace Feesh
{
    class Renderer
    {
        public int VertexArrayObject;

        public int VertexBufferObject;

        public int IndexBufferObject;

        public int TextureCoordsBufferObject;

        private Mesh mesh;
        public Renderer()
        {
            mesh = ObjLoader.Load("IronMan.obj");

            VertexArrayObject = GL.GenVertexArray();

            VertexBufferObject = GL.GenBuffer();

            IndexBufferObject = GL.GenBuffer();

            TextureCoordsBufferObject = GL.GenBuffer();

            // ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.vertices.Length * Vector3.SizeInBytes, mesh.vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.textureVertices.Length * Vector3.SizeInBytes, mesh.textureVertices, BufferUsageHint.StaticDraw);

            // 3. then set our vertex attributes pointers
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.vertexIndices.Length * sizeof(uint), mesh.vertexIndices, BufferUsageHint.StaticDraw);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        }

        public void Draw(Shader shader, Matrix4 transform, Game game)
        {
            shader.Use();

            shader.SetMatrix4("model", transform);
            shader.SetMatrix4("view", game.view);
            shader.SetMatrix4("projection", game.projection);

            GL.BindVertexArray(VertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles , mesh.vertexIndices.Length, DrawElementsType.UnsignedInt, 0);
        }

    }
}
