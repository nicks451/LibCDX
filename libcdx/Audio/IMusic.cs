namespace libcdx.Audio
{
    public interface IMusic
    {
        void Play();

        void Pause();

        void Stop();

        bool IsPlaying();

        void SetLooping(bool isLooping);

        void IsLooping();

        void SetVolume(float volume);

        float GetVolume();

        void SetPan(float pan, float volume);

        void SetPosition(float position);

        void GetPosition();

        void Dispose();

        void SetOnCompletionListener(IOnCompletionListener listener);
    }

    public interface IOnCompletionListener
    {
        void OnCompletion(IMusic music);
    }
}