using System;
using System.Collections.Generic;
using System.Text;
using ObjRenderer;
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
    class Game : GameWindow
    {
        private Shader shader;
        private Texture texture;

        private Renderer renderer;

        private Vector2 position = new Vector2(0f,0f);

        private double time;

        public Matrix4 projection;

        public Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

        private Mesh mesh;

        public Game(string windowTitle) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            Title = windowTitle;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e)
            {
                case {Key: Keys.D}:
                    position.X += 0.1f;
                    break;
                case {Key: Keys.A}:
                    position.X -= 0.1f;
                    break;
            } 
            switch (e)
            {
                case {Key: Keys.W}:
                    position.Y += 0.1f;
                    break;
                case {Key: Keys.S}:
                    position.Y -= 0.1f;
                    break;
            }

            base.OnKeyDown(e);
        }

        protected override void OnLoad()
        {
            renderer = new Renderer();
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / Size.Y, 0.1f, 100.0f);

            //set up the shader
            shader = new Shader("shader.vert", "shader.frag");
            texture = new Texture("Fish.jpg");
            shader.Texture = texture;

            base.OnLoad();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0,0,e.Width, e.Height);

            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            time += args.Time * 5;
            Title = "FPS: " + Math.Round(1 / args.Time);


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            Matrix4 rotation = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-90));
            rotation *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)time + 45f));
            Matrix4 scale = Matrix4.CreateScale(.3f, .3f, .3f);
            Matrix4 position = Matrix4.CreateTranslation(new Vector3(this.position));
            Matrix4 trans = rotation * scale * position;

            renderer.Draw(shader, trans, this);


            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(renderer.VertexBufferObject);
            base.OnUnload();
        }
    }
}