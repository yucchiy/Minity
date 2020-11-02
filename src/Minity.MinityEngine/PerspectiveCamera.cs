using OpenTK.Mathematics;

namespace Minity.MinityEngine
{
    public class PerspectiveCamera : ICamera
    {
        public float Fov { get; set; }
        public float Aspect => Width / Height;
        public float Width { get; set; }
        public float Height { get; set; }
        public float Near { get; set; }
        public float Far { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }

        public PerspectiveCamera(Vector3 position, Vector3 target, float width, float height, float fov, float near, float far)
        {
            Position = position;
            Target = target;
            Width = width;
            Height = height;
            Fov = fov;
            Near = near;
            Far = far;
        }
        public void GetViewMatrix(out Matrix4 result)
        {
            result = Matrix4.LookAt(Position, Target, Vector3.UnitY);
        }

        public void GetProjectionMatrix(out Matrix4 result)
        {
            Matrix4.CreatePerspectiveFieldOfView(Fov, Aspect, Near, Far, out result);
        }
    }
}