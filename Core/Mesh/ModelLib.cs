using System;
using System.Collections.Generic;
using System.Text;

namespace Feesh.Core.Mesh
{
    class ModelLib
    {
        public static Dictionary<string, int> ExistingMeshes = new Dictionary<string, int>();
        public static List<List<Mesh>> Meshes = new List<List<Mesh>>();

        public static void AddNewMesh(List<Mesh> meshes, string name, out int id)
        {
            Meshes.Add(meshes);
            ExistingMeshes.Add(name, Meshes.Count - 1);
            id = Meshes.Count - 1;
        }
    }
}
