namespace Minity.MinityEngine
{
    public interface IScene
    {
        void Setup();
        void Update(double deltaTime);
        void Render(double deltaTime);
    }
}