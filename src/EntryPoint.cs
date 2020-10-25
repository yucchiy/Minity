
namespace Minity
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            using (var window = new MinityEngine.MinityWindow(new App.FirstTriangleScene()))
            {
                window.Run();
            }
        }
    }
}
