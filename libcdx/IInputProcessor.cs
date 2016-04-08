namespace libcdx
{
    public interface IInputProcessor
    {
        /** Called when a key was pressed
        * 
        * @param keycode one of the constants in {@link Input.Keys}
        * @return whether the input was processed */
        bool KeyDown(int keycode);

        /** Called when a key was released
         * 
         * @param keycode one of the constants in {@link Input.Keys}
         * @return whether the input was processed */
        bool KeyUp(int keycode);

        /** Called when a key was typed
         * 
         * @param character The character
         * @return whether the input was processed */
        bool KeyTyped(char character);

        /** Called when the screen was touched or a mouse button was pressed. The button parameter will be {@link Buttons#LEFT} on iOS.
         * @param screenX The x coordinate, origin is in the upper left corner
         * @param screenY The y coordinate, origin is in the upper left corner
         * @param pointer the pointer for the event.
         * @param button the button
         * @return whether the input was processed */
        bool TouchDown(int screenX, int screenY, int pointer, int button);

        /** Called when a finger was lifted or a mouse button was released. The button parameter will be {@link Buttons#LEFT} on iOS.
         * @param pointer the pointer for the event.
         * @param button the button
         * @return whether the input was processed */
        bool TouchUp(int screenX, int screenY, int pointer, int button);

        /** Called when a finger or the mouse was dragged.
         * @param pointer the pointer for the event.
         * @return whether the input was processed */
        bool TouchDragged(int screenX, int screenY, int pointer);

        /** Called when the mouse was moved without any buttons being pressed. Will not be called on iOS.
         * @return whether the input was processed */
        bool MouseMoved(int screenX, int screenY);

        /** Called when the mouse wheel was scrolled. Will not be called on iOS.
         * @param amount the scroll amount, -1 or 1 depending on the direction the wheel was scrolled.
         * @return whether the input was processed. */
        bool Scrolled(int amount);
    }
}