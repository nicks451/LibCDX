using System;
using libcdx.Utils;

namespace libcdx
{
    /** The version of libgdx
     * 
     * @author mzechner */
    public class Version
    {
        /** the current version of libgdx as a String in the major.minor.revision format **/
        public readonly string VERSION = "1.9.3";

	    /** the current major version of libgdx **/
	    public static int MAJOR;

        /** the current minor version of libgdx **/
        public static int MINOR;

        /** the current revision version of libgdx **/
        public static int REVISION;

        public Version()
        {
            try
            {
                string[] v = VERSION.Split(Convert.ToChar("\\."));
                MAJOR = v.Length < 1 ? 0 : Int32.Parse(v[0]);
                MINOR = v.Length < 2 ? 0 : Int32.Parse(v[1]);
                REVISION = v.Length < 3 ? 0 : Int32.Parse(v[2]);
            }
            catch (Exception e)
            {
                // Should never happen
                throw new CdxRuntimeException("Invalid version " + VERSION, e);
            }
        }

	    public static bool IsHigher(int major, int minor, int revision)
        {
            return IsHigherEqual(major, minor, revision + 1);
        }

        public static bool IsHigherEqual(int major, int minor, int revision)
        {
            if (MAJOR != major)
            {
                return MAJOR > major;
            }
            if (MINOR != minor)
            {
                return MINOR > minor;
            }
            return REVISION >= revision;
        }

        public static bool IsLower(int major, int minor, int revision)
        {
            return IsLowerEqual(major, minor, revision - 1);
        }

        public static bool IsLowerEqual(int major, int minor, int revision)
        {
            if (MAJOR != major)
            {
                return MAJOR < major;
            }
            if (MINOR != minor)
            {
                return MINOR < minor;
            }
            return REVISION <= revision;
        }
    }
}