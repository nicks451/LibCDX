namespace libcdx
{
    public interface IScreen
    {
        void Show();

        void Render(float delta);

        void Resize(int width, int height);

        void Pause();

        void Resume();

        void Hide();

        void Dispose();
    }
}