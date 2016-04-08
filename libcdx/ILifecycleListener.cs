namespace libcdx
{
    public interface ILifecycleListener
    {
        /** Called when the {@link Application} is about to pause */
        void Pause();

        /** Called when the Application is about to be resumed */
        void Resume();

        /** Called when the {@link Application} is about to be disposed */
        void Dispose();
    }
}