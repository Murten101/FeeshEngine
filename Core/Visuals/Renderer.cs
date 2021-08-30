using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Mathematics;
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
                gameObject.shader.Use();

                gameObject.shader.SetMatrix4("model", gameObject.Transform.TransformMatrix);
                gameObject.shader.SetMatrix4("view", game.camera.GetViewMatrix());
                gameObject.shader.SetMatrix4("projection", game.camera.GetProjectionMatrix());

                foreach (var mesh in gameObject.Mesh)
                {
                    GL.BindVertexArray(mesh.VertexArrayObject);

                    GL.DrawElements(PrimitiveType.Triangles, mesh.vertexIndices.Length,
                        DrawElementsType.UnsignedInt, 0);
                }
            }
        }

    }
}
