using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Feesh.Core.Mesh
{
    public class Mesh
    {
        public int[] vertexIndices;
        public List<Vector3> vertices;
        public List<Vector3> textureVertices;
        public List<Vector3> normals;

        public int VertexArrayObject;

        public int VertexBufferObject;

        public int IndexBufferObject;

        public int TextureCoordsBufferObject;

        public int NormalBufferObject;
        public Mesh(int[] vertexIndices, List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals)
        {
            this.vertices = vertices;
            this.textureVertices = textureVertices;
            this.normals = normals;
            this.vertexIndices = vertexIndices;

            //Create new buffers
            VertexArrayObject = GL.GenVertexArray();

            VertexBufferObject = GL.GenBuffer();

            IndexBufferObject = GL.GenBuffer();

            TextureCoordsBufferObject = GL.GenBuffer();

            NormalBufferObject = GL.GenBuffer();

            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, this.textureVertices.Count * Vector3.SizeInBytes, this.textureVertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, this.normals.Count * Vector3.SizeInBytes, this.normals.ToArray(), BufferUsageHint.StaticDraw);

            //3.then set our vertex attributes pointers
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(2);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices.Length * sizeof(uint), vertexIndices, BufferUsageHint.StaticDraw);
        }
    }
}
