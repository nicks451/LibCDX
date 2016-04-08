using System;
using System.Diagnostics;

namespace libcdx.Utils
{
    public class TimeUtils
    {
        private const long nanosPerMilli = 1000000;

        /** @return The current value of the system timer, in nanoseconds. */
        public static long NanoTime()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (DateTime.UtcNow - epochStart).Ticks * 100;
        }

        /** @return the difference, measured in milliseconds, between the current time and midnight, January 1, 1970 UTC. */
        public static long Millis()
        {
            return DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;
        }

        

        /** Convert nanoseconds time to milliseconds
         * @param nanos must be nanoseconds
         * @return time value in milliseconds */
        public static long NanosToMillis(long nanos)
        {
            return nanos / nanosPerMilli;
        }

        /** Convert milliseconds time to nanoseconds
         * @param millis must be milliseconds
         * @return time value in nanoseconds */
        public static long MillisToNanos(long millis)
        {
            return millis * nanosPerMilli;
        }

        /** Get the time in nanos passed since a previous time
         * @param prevTime - must be nanoseconds
         * @return - time passed since prevTime in nanoseconds */
        public static long TimeSinceNanos(long prevTime)
        {
            return NanoTime() - prevTime;
        }

        /** Get the time in millis passed since a previous time
         * @param prevTime - must be milliseconds
         * @return - time passed since prevTime in milliseconds */
        public static long TimeSinceMillis(long prevTime)
        {
            return Millis() - prevTime;
        }
    }
}