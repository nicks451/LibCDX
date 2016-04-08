using System;
using System.Text.RegularExpressions;
using libcdx.Utils;

namespace libcdx.Graphics.GLUtils
{
    public class GLVersion
    {
        private int majorVersion;
        private int minorVersion;
        private int releaseVersion;

        private readonly string vendorString;
        private readonly string rendererString;

        private readonly Type type;

        private readonly string TAG = "GLVersion";

        public GLVersion(ApplicationType appType, string versionString, string vendorString, string rendererString)
        {
            if (appType == ApplicationType.Android) this.type = Type.GLES;
            else if (appType == ApplicationType.iOS) this.type = Type.GLES;
            else if (appType == ApplicationType.Desktop) this.type = Type.OpenGL;
            else if (appType == ApplicationType.Applet) this.type = Type.OpenGL;
            else if (appType == ApplicationType.WebGL) this.type = Type.WebGL;
            else this.type = Type.NONE;

            if (type == Type.GLES)
            {
                //OpenGL<space>ES<space><version number><space><vendor-specific information>.
                ExtractVersion("OpenGL ES (\\d(\\.\\d){0,2})", versionString);
            }
            else if (type == Type.WebGL)
            {
                //WebGL<space><version number><space><vendor-specific information>
                ExtractVersion("WebGL (\\d(\\.\\d){0,2})", versionString);
            }
            else if (type == Type.OpenGL)
            {
                //<version number><space><vendor-specific information>
                ExtractVersion("(\\d(\\.\\d){0,2})", versionString);
            }
            else
            {
                majorVersion = -1;
                minorVersion = -1;
                releaseVersion = -1;
                vendorString = "";
                rendererString = "";
            }

            this.vendorString = vendorString;
            this.rendererString = rendererString;
        }

        private void ExtractVersion(string patternString, string versionString)
        {
            Regex pattern = new Regex(patternString);
            Match matcher = pattern.Match(versionString);
            bool found = matcher.Success;
            if (found)
            {
                string result = matcher.Groups[1].Value;
                string[] resultSplit = result.Split(Convert.ToChar("\\."));
                majorVersion = parseInt(resultSplit[0], 2);
                minorVersion = resultSplit.Length < 2 ? 0 : parseInt(resultSplit[1], 0);
                releaseVersion = resultSplit.Length < 3 ? 0 : parseInt(resultSplit[2], 0);
            }
            else
            {
                Cdx.App.Log(TAG, "Invalid version string: " + versionString);
                majorVersion = 2;
                minorVersion = 0;
                releaseVersion = 0;
            }
        }

        /** Forgiving parsing of gl major, minor and release versions as some manufacturers don't adhere to spec **/
        private int parseInt(String v, int defaultValue)
        {
            try
            {
                return Int32.Parse(v);
            }
            catch (Exception nfe)
            {
                Cdx.App.Error("LibGDX GL", "Error parsing number: " + v + ", assuming: " + defaultValue);
                return defaultValue;
            }
        }

        /** @return what {@link Type} of GL implementation this application has access to, e.g. {@link Type#OpenGL} or {@link Type#GLES}*/
        public Type getType()
        {
            return type;
        }

        /** @return the major version of current GL connection. -1 if running headless */
        public int getMajorVersion()
        {
            return majorVersion;
        }

        /** @return the minor version of the current GL connection. -1 if running headless */
        public int getMinorVersion()
        {
            return minorVersion;
        }

        /** @return the release version of the current GL connection. -1 if running headless */
        public int getReleaseVersion()
        {
            return releaseVersion;
        }

        /** @return the vendor string associated with the current GL connection */
        public String getVendorString()
        {
            return vendorString;
        }

        /** @return the name of the renderer associated with the current GL connection.
         * This name is typically specific to a particular configuration of a hardware platform. */
        public String getRendererString()
        {
            return rendererString;
        }

        /**
         * Checks to see if the current GL connection version is higher, or equal to the provided test versions.
         *
         * @param testMajorVersion the major version to test against
         * @param testMinorVersion the minor version to test against
         * @return true if the current version is higher or equal to the test version
         */
        public bool isVersionEqualToOrHigher(int testMajorVersion, int testMinorVersion)
        {
            return majorVersion > testMajorVersion || (majorVersion == testMajorVersion && minorVersion >= testMinorVersion);
        }

        /** @return a string with the current GL connection data */
        public string getDebugVersionString()
        {
            return "Type: " + type + "\n" +
                    "Version: " + majorVersion + ":" + minorVersion + ":" + releaseVersion + "\n" +
                    "Vendor: " + vendorString + "\n" +
                    "Renderer: " + rendererString;
        }

        public enum Type
        {
            OpenGL,
            GLES,
            WebGL,
            NONE
        }
    }
}