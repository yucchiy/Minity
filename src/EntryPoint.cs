
namespace Minity
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.FirstTriangle.FirstTriangleScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.ColoredTriangle.MainScene()))
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.IBO.MainScene()))
            using (var window = new MinityEngine.MinityWindow(new App.Exercise.Uniform.MainScene()))
            {
                window.Run();
            }
        }
    }
}
