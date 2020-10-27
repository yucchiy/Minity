
namespace Minity
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            // using (var window = new MinityEngine.MinityWindow(new App.Exercise.FirstTriangle.FirstTriangleScene()))
            using (var window = new MinityEngine.MinityWindow(new App.Exercise.ColoredTriangle.MainScene()))
            {
                window.Run();
            }
        }
    }
}
