using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assimp;
using Assimp.Configs;
using OpenTK.Mathematics;

namespace Feesh
{
    static class ModelLoader
    {
        public static List<Mesh> loadMesh(string modelPath)
        {
            //Check if file exist
            if (!File.Exists("Assets/Meshes/" + modelPath))
            {
                Utils.WriteLine("file not found at Assets/Meshes/" + modelPath, ConsoleColor.DarkRed);
                throw new FileNotFoundException("file not found at Assets/Meshes/" + modelPath);
            }

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));

            var scene = importer.ImportFile("Assets/Meshes/" + modelPath);

            List<Mesh> meshes = new List<Mesh>(scene.Meshes.Count);

            foreach (var mesh in scene.Meshes)
            {
                meshes.Add(new Mesh(mesh.GetIndices(), Utils.ConvertV3DToV3(mesh.Vertices),Utils.ConvertV3DToV3(mesh.TextureCoordinateChannels[0]), Utils.ConvertV3DToV3(mesh.Normals)));
            }

            return meshes;
        }
    }
}
