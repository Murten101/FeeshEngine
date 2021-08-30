using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Feesh
{
    public class Transform
    {
        public Matrix4 TransformMatrix
        {
            get => Matrix4.CreateScale(Scale) * Matrix4.CreateFromQuaternion(Rotation) * Matrix4.CreateTranslation(Position);
        }

        public Vector3 Position, Scale;

        public Quaternion Rotation; 

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        public void Rotate(float x, float y, float z)
        {
            Rotation *= Quaternion.FromEulerAngles(x, y, z);
        }

        public void Rotate(Vector3 rotation)
        {
            Rotation *= Quaternion.FromEulerAngles(rotation);
        }

        public void Translate(Vector3 translation)
        {
            Position += translation;
        }        
        public void Translate(float x, float y, float z)
        {
            Position += new Vector3(x, y, z);
        }
    }
}
