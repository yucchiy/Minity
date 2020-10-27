
namespace Minity
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            // using (var window = new MinityEngine.MinityWindow(new App.FirstTriangle.FirstTriangleScene()))
            using (var window = new MinityEngine.MinityWindow(new App.ColoredTriangle.MainScene()))
            {
                window.Run();
            }
        }
    }
}
