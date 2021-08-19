using System.Collections.Generic;
using OpenTK;
using OpenTK.Mathematics;

namespace ObjRenderer
{
    public class Mesh
    {
        public readonly Vector3[] vertices;
        public readonly Vector3[] textureVertices;
        public readonly Vector3[] normals;
        public readonly uint[] vertexIndices;

        public Mesh(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals,
                    List<uint> vertexIndices)
        {
            this.vertices = vertices.ToArray();

            this.textureVertices = textureVertices.ToArray();

            this.normals = normals.ToArray();

            this.vertexIndices = vertexIndices.ToArray();
        }
    }
}
