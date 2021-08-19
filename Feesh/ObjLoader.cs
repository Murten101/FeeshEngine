using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OpenTK;
using OpenTK.Mathematics;

namespace ObjRenderer
{
	public static class ObjLoader
	{
		public static Mesh Load(string path)
        {
            path = "Assets/Meshes/" + path;

			List<Vector3> vertices = new List<Vector3>();
			List<Vector3> textureVertices = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			List<uint> vertexIndices = new List<uint>();

            var clone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            clone.NumberFormat.NumberDecimalSeparator = ".";
            clone.NumberFormat.NumberGroupSeparator = ",";

			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
			}

			using(StreamReader streamReader = new StreamReader(path))
			{
                List<Vector3> TempTextureVertices = new List<Vector3>();
                List<Vector3> TempNormals = new List<Vector3>();
				while (!streamReader.EndOfStream)
				{
					List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
					words.RemoveAll(s => s == string.Empty);

					if(words.Count == 0)
						continue;
					
					string type = words[0];
					words.RemoveAt(0);

                    switch(type)
					{
						// vertex
						case "v":
							vertices.Add(new Vector3(float.Parse(words[0], clone), float.Parse(words[1], clone), float.Parse(words[2], clone)));
							break;

						case "vt":
							TempTextureVertices.Add(new Vector3(float.Parse(words[0], clone), float.Parse(words[1], clone), words.Count < 3 ? 0 : float.Parse(words[2], clone)));
							textureVertices.Add(new Vector3(float.Parse(words[0], clone), float.Parse(words[1], clone), words.Count < 3 ? 0 : float.Parse(words[2], clone)));
                            break;

						case "vn":
							TempNormals.Add(new Vector3(float.Parse(words[0], clone), float.Parse(words[1], clone), float.Parse(words[2], clone)));
							break;
						
						// face
						case "f":
							foreach(string w in words)
							{
								if(w.Length == 0)
									continue;
								
								string[] comps = w.Split('/');

								// subtract 1: indices start from 1, not 0
								vertexIndices.Add(uint.Parse(comps[0], clone) - 1);

                                if (comps.Length > 1 && comps[1].Length != 0)
                                {
                                    var i = int.Parse(comps[1], clone) - 1;

                                    textureVertices[int.Parse(comps[0], clone) - 1] = (TempTextureVertices[i]);
                                }

                                if(comps.Length > 2)
									normals.Add(TempNormals[int.Parse(comps[2], clone) - 1]);
							}
							break;
                    }
				}
			}

			return new Mesh(vertices, textureVertices, normals, vertexIndices);
		}
	}
}
