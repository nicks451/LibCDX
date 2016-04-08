namespace libcdx
{
    public abstract class Game
    {
        protected IScreen screen;

        public void Dispose()
        {
            if (screen != null)
            {
                screen.Hide();
            }
        }

        public void Pause()
        {
            if (screen != null)
            {
                screen.Pause();
            }
        }

        public void Resume()
        {
            if (screen != null)
            {
                screen.Resume();
            }
        }

        public void Render()
        {
            if (screen != null)
            {
                screen.Render(Cdx.Graphics.GetDeltaTime());
            }
        }

        public void Resize(int width, int height)
        {
            if (screen != null)
            {
                screen.Resize(width, height);
            }
        }

        public void SetScreen(IScreen screen)
        {
            if (this.screen != null)
            {
                this.screen.Hide();
            }

            this.screen = screen;

            if (this.screen != null)
            {
                this.screen.Show();
                this.screen.Resize(Cdx.Graphics.GetWidth(), Cdx.Graphics.GetHeight());
            }
        }

        public IScreen GetScreen()
        {
            return this.screen;
        }
    }
}