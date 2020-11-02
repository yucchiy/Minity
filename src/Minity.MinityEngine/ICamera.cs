using OpenTK.Mathematics;

namespace Minity.MinityEngine
{
    public interface ICamera
    {
        float Width { get; set; }
        float Height { get; set; }
        Vector3 Position { get; set; }
        Vector3 Target { get; set; }
        void GetViewMatrix(out Matrix4 value);
        void GetProjectionMatrix(out Matrix4 value);
    }
}