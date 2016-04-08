namespace libcdx
{
    public class InputAdapter : IInputProcessor
    {
        public bool KeyDown(int keycode)
        {
            return false;
        }

        public bool KeyUp(int keycode)
        {
            return false;
        }

        public bool KeyTyped(char character)
        {
            return false;
        }

        public bool TouchDown(int screenX, int screenY, int pointer, int button)
        {
            return false;
        }

        public bool TouchUp(int screenX, int screenY, int pointer, int button)
        {
            return false;
        }

        public bool TouchDragged(int screenX, int screenY, int pointer)
        {
            return false;
        }

        public bool MouseMoved(int screenX, int screenY)
        {
            return false;
        }

        public bool Scrolled(int amount)
        {
            return false;
        }
    }
}