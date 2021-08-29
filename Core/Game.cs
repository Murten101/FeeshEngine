using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace Feesh
{
    public class Game : GameWindow
    {
        private Renderer renderer;

        public Camera camera;

        private bool firstMove = true;

        private Vector2 lastPos;

        private GameObject gameObject;
        private GameObject gameObject2;

        public Game(string windowTitle) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Title = windowTitle;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnLoad()
        {
            renderer = new Renderer(Color.Black);

            gameObject = new GameObject("scene.gltf", Vector3.Zero, Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(-90),0,0), Vector3.One, renderer);
            gameObject2 = new GameObject("scene.gltf", Vector3.Zero, Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(90), 0,0), Vector3.One, renderer);

            camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            base.OnLoad();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);

            // We need to update the aspect ratio once the window has been resized.
            camera.AspectRatio = Size.X / (float) Size.Y;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderer.Draw(this);

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Title = Math.Round(1 / e.Time).ToString();
            base.OnUpdateFrame(e);  

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            if (input.IsKeyDown(Keys.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                camera.Position += camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)e.Time; // Down
            }
            if (input.IsKeyDown(Keys.R))
            {
                gameObject.Transform.Position += new Vector3(30 * (float)e.Time, 0, 0);
            }
            if (input.IsKeyDown(Keys.T))
            {
                gameObject.Transform.Rotate(5 * (float)e.Time, 0, 0);
                Utils.WriteLine(gameObject.Transform.Rotation.ToString(), ConsoleColor.Cyan);
            }

            if (input.IsKeyDown(Keys.Y))
            {
                gameObject.Transform.Scale += Vector3.One * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.U))
            {
                gameObject.Transform.Scale -= Vector3.One * (float)e.Time;
            }
            // Get the mouse state
            var mouse = MouseState;

            if (firstMove) // This bool variable is initially set to true.
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse Position
                var deltaX = mouse.X - lastPos.X;
                var deltaY = mouse.Y - lastPos.Y;
                lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                camera.Yaw += deltaX * sensitivity;
                camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            foreach (var gameObject in renderer.renderQueue)
            {
                foreach (var mesh in gameObject.Mesh)
                {
                    GL.DeleteBuffer((int)mesh.VertexBufferObject);
                }
            }
            base.OnUnload();
        }
    }
}