namespace libcdx.Audio
{
    public interface IAudioRecorder
    {
        void Read(short[] samples, int offset, int numSamples);

        void dispose();
    }
}