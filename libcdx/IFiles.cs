using libcdx.Files;

namespace libcdx
{
    public interface IFiles
    {
        FileHandle GetFileHandle(string path, FileType type);

        FileHandle Classpath(string path);

        FileHandle Internal(string path);

        FileHandle External(string path);

        FileHandle Absolute(string path);

        FileHandle Local(string path);

        string GetExternalStoragePath();

        bool IsExternalStorageAvailable();

        string GetLocalStoragePath();

        bool IsLocalStorageAvailable();
    }
}