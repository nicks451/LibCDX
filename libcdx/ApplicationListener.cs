namespace libcdx
{
    public interface ApplicationListener
    {
        void Create();

        void Resize(int width, int height);

        void Render();

        void Pause();

        void Resume();

        void Dispose();
    }
}