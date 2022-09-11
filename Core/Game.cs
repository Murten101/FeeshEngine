using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using Feesh.Core.Mesh;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace Feesh
{
    public class Game : GameWindow
    {
        private Renderer renderer;

        public Camera camera;

        private bool firstMove = true;

        private Vector2 lastPos;

        private List<GameObject> gameObjects = new List<GameObject>();

        public float theNum = 1;

        public Game(string windowTitle) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Title = windowTitle;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            renderer = new Renderer(Color.Coral);

                gameObjects.Add(new GameObject("Posa_13.fbx", new Vector3(0, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(-90), 0, 0), Vector3.One * 0.01f, renderer));
                gameObjects.Add(new GameObject("IronMan.obj", new Vector3(4, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(0), 0, 0), Vector3.One * 0.02f, renderer));
                gameObjects.Add(new GameObject("scene.gltf", new Vector3(8, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(-90), 0, 0), Vector3.One, renderer));
                gameObjects.Add(new GameObject("IronMan.obj", new Vector3(12, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(0), 0, 0), Vector3.One * 0.02f, renderer));
                gameObjects.Add(new GameObject("Posa_13.fbx", new Vector3(16, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(-90), 0, 0), Vector3.One * 0.01f, renderer));
                gameObjects.Add(new GameObject("IronMan.obj", new Vector3(20, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(0), 0, 0), Vector3.One * 0.02f, renderer));
                gameObjects.Add(new GameObject("scene.gltf", new Vector3(24, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(-90), 0, 0), Vector3.One, renderer));
                gameObjects.Add(new GameObject("IronMan.obj", new Vector3(28, 0, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(0), 0, 0), Vector3.One * 0.02f, renderer));
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        Stopwatch stopwatch = Stopwatch.StartNew();
            //        new GameObject("TESTING.FBX", new Vector3(i, j, 0), Quaternion.FromEulerAngles(MathHelper.DegreesToRadians(-90), 0, 0), Vector3.One, renderer); 
            //        stopwatch.Stop();

            //        TimeSpan ts = stopwatch.Elapsed;

            //        Console.WriteLine("game object created in {2:00}.{3} seconds",
            //            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            //    }
            //}

            camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorGrabbed = true;

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //Utils.WriteLine("resized to :" + e.Height +" , " + e.Width, ConsoleColor.White);
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

            if (input.IsKeyDown(Keys.LeftAlt) && input.IsKeyDown(Keys.Enter))
            {
                WindowState = WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
                Thread.Sleep(100);
            }
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
            if (input.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)e.Time; // Down
            }
            if (input.IsKeyDown(Keys.R))
            {
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Transform.Position += new Vector3(30 * (float)e.Time, 0, 0);
                }
            }
            if (input.IsKeyDown(Keys.T))
            {
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Transform.Rotate(5 * (float) e.Time, 0, 0);
                }

            }
            if (input.IsKeyDown(Keys.G))
            {
                theNum -= (float)(1 * e.Time);
            }
            if (input.IsKeyDown(Keys.Y))
            {
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Transform.Scale += Vector3.One * (float) e.Time;
                }
            }
            if (input.IsKeyDown(Keys.U))
            {
                foreach (var gameObject in gameObjects)
                {
                    gameObject.Transform.Scale -= Vector3.One * (float) e.Time;
                }
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
                foreach (var mesh in ModelLib.Meshes[gameObject.MeshId])
                {
                    GL.DeleteBuffer((int)mesh.VertexBufferObject);
                }
            }
            base.OnUnload();
        }
    }
}