using Feesh.Core.Mesh;
using Feesh.Core.Visuals;
using OpenTK.Mathematics;

namespace Feesh
{
    public class GameObject
    {
        public Transform Transform;
        public int MeshId;
        public Shader Shader;
        public GameObject(string modelPath, Vector3 position, Quaternion rotation, Vector3 scale, Renderer renderer)
        {
            Shader = new Shader("Shader.vert", "Shader.frag");
            Shader.Texture = new Texture("Fish.jpg");
            this.Transform = new Transform(position, rotation, scale);
            this.MeshId = ModelLoader.loadMesh(modelPath);
            renderer.renderQueue.Add(this);
        }


    }
}
