
namespace Minity
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.FirstTriangle.FirstTriangleScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.ColoredTriangle.MainScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.IBO.MainScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.Uniform.MainScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.Texture.MainScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.Camera.MainScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.FirstDepth.MainScene()))
            using (var window = new MinityEngine.MinityWindow(new App.Exercise.BasicLight.MainScene()))
            {
                window.Run();
            }
        }
    }
}
