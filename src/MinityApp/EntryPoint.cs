
namespace MinityApp
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            using (var window = new MinityEngine.MinityWindow(new SampleColoredTriangleScene()))
            {
                window.Run();
            }
        }
    }
}
