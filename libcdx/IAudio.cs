using libcdx.Audio;
using libcdx.Files;

namespace libcdx
{
    public interface IAudio
    {
        IAudioDevice NewAudioDevice(int samplingRate, bool isMono);

        IAudioRecorder NewAudioRecorder(int samplingRate, bool isMono);

        ISound NewSound(FileHandle fileHandle);

        IMusic NewMusic(FileHandle fileHandle);
    }
}