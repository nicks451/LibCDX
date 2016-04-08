namespace libcdx.Graphics
{
    /// <summary>
    /// Describe a fullscreen display mode
    /// </summary>
    public class DisplayMode
    {
        /// <summary>
        /// the width in physical pixels
        /// </summary>
        public readonly int width;
        /// <summary>
        /// the height in physical pixles
        /// </summary>
        public readonly int height;
        /// <summary>
        /// the refresh rate in Hertz
        /// </summary>
        public readonly int refreshRate;
        /// <summary>
        /// the number of bits per pixel, may exclude alpha
        /// </summary>
        public readonly int bitsPerPixel;

        protected DisplayMode(int width, int height, int refreshRate, int bitsPerPixel)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
            this.bitsPerPixel = bitsPerPixel;
        }

        public new string ToString()
        {
            return width + "x" + height + ", bpp: " + bitsPerPixel + ", hz: " + refreshRate;
        }
    }
}