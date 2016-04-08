namespace libcdx.Audio
{
    public interface ISound
    {
        long Play();

        long Play(float volume);

        long Play(float volume, float pitch, float pan);

        long Loop();

        long Loop(float volume);

        long Loop(float volume, float pitch, float pan);

        void Stop();

        void Pause();

        void Resume();

        void Dispose();

        void Stop(long soundId);

        void Pause(long soundId);

        void Resume(long soundId);

        void SetLooping(long soundId, bool looping);

        void SetPitch(long soundId, float pitch);

        void SetVolume(long soundId, float volume);

        void SetPan(long soundId, float pan, float volume);
    }
}