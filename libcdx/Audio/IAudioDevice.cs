namespace libcdx.Audio
{
    public interface IAudioDevice
    {
        bool IsMono();

        void WriteSamples(int[] samples, int offset, int numSamples);

        void WriteSample(float[] samples, int offset, int numSamples);

        int GetLatency();

        void Dispose();

        void SetVolume(float volume);
    }
}