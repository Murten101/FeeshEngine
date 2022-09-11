using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Drawing;
using Feesh.Core.Mesh;

namespace Feesh
{
    public class Renderer
    {
        public List<GameObject> renderQueue = new List<GameObject>();

        public Renderer(Color clearColor)
        {
            GL.ClearColor(clearColor);
        }

        public void Draw(Game game)
        {
            foreach (var gameObject in renderQueue)
            {
                gameObject.Shader.Use();

                gameObject.Shader.SetMatrix4("model", gameObject.Transform.TransformMatrix);
                gameObject.Shader.SetMatrix4("view", game.camera.GetViewMatrix());
                gameObject.Shader.SetMatrix4("projection", game.camera.GetProjectionMatrix());

                gameObject.Shader.SetVector3("objectColor", new Vector3(0.0f, 1f, 0f));
                gameObject.Shader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
                gameObject.Shader.SetVector3("lightPos", game.camera.Position);
                gameObject.Shader.SetVector3("viewPos", game.camera.Position);

                foreach (var mesh in ModelLib.Meshes[gameObject.MeshId])
                {
                    GL.BindVertexArray(mesh.VertexArrayObject);

                    GL.DrawElements(PrimitiveType.Triangles, mesh.vertexIndices.Length,
                        DrawElementsType.UnsignedInt, 0);
                }
            }
        }

    }
}
