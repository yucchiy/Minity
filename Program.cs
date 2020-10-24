using System;

namespace Minity
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new MinityWindow())
            {
                window.Run();
            }
        }
    }
}
