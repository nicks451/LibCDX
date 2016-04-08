using System;
using System.Threading;
using libcdx.Utils;

namespace libcdx
{
    /// <summary>
    /// An <code>Application</code> is the main entry point of your project. It sets up a window and rendering surface and manages the
    /// different aspects of your application, namely Graphics, Audio, Input and Files. This of an 
    /// Application being equivilant to Swing's <code>JFrame</code> or Android's <code>Activity</code>.
    /// 
    /// 
    /// </summary>
    public abstract class Application
    {
        public static readonly int LOG_NONE = 0;
        public static readonly int LOG_DEBUG = 3;
        public static readonly int LOG_INFO = 2;
        public static readonly int LOG_ERROR = 1;

        /// <returns>The <see cref="ApplicationListener"/> instance</returns>
        public abstract ApplicationListener GetApplicationListener();

        /// <returns>The <see cref="IGraphics"/> instance</returns>
        public abstract IGraphics GetGraphics();

        public abstract IAudio GetAudio();

        public abstract IInput GetInput();

        public abstract IFiles GetFiles();

        public abstract INet GetNet();

        public abstract void Log(string tag, string message);

        public abstract void Log(string tag, string message, Exception exception);

        public abstract void Error(string tag, string message);

        public abstract void Error(string tag, string message, Exception exception);

        public abstract void Debug(string tag, string message);

        public abstract void Debug(string tag, string message, Exception exception);

        public abstract void SetLogLevel(int logLevel);

        public abstract int getLogLevel();

        public abstract ApplicationType GetApplicationType();

        public abstract int GetVersion();

        public abstract long GetJavaHeap();

        public abstract long GetNativeHeap();

        public abstract IPreferences GetPreferences(string name);

        public abstract IClipboard GetClipboard();

        public abstract void PostRunnable(ThreadStart runnable);

        public abstract void exit();

        public abstract void AddLifecycleListener(ILifecycleListener listener);

        public abstract void RemoveLifecycleListener(ILifecycleListener listener);
    }
}