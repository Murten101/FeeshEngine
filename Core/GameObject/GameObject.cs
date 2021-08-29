using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace Feesh
{
    public class GameObject
    {
        public Transform Transform;
        public List<Mesh> Mesh;
        public Shader shader;
        public GameObject(string modelPath, Vector3 position, Quaternion rotation, Vector3 scale, Renderer renderer)
        {
            shader = new Shader("shader.vert", "shader.frag");
            shader.Texture = new Texture("Fish.jpg");
            this.Transform = new Transform(position, rotation, scale);
            this.Mesh = ModelLoader.loadMesh(modelPath);
            renderer.renderQueue.Add(this);
        }


    }
}
