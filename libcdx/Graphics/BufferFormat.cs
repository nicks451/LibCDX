namespace libcdx.Graphics
{
    /// <summary>
    /// Class describing the bits per pixel, depth buffer precision, stencil precision and number of MSAA samples.
    /// </summary>
    public class BufferFormat
    {
        /// <summary>
        /// number of bits per color channel
        /// </summary>
        public readonly int r, g, b, a;
        /// <summary>
        /// number of bits for depth and stencil buffer
        /// </summary>
        public readonly int depth, stencil;
        /// <summary>
        /// number of samples for multi-sample anti-aliasing (MSAA)
        /// </summary>
        public readonly int samples;
        /// <summary>
        /// whether coverage sampling anti-aliasing is used. in that case you have to clear the coverage buffer as well!
        /// </summary>
        public readonly bool coverageSampling;

        public BufferFormat(int r, int g, int b, int a, int depth, int stencil, int samples, bool coverageSampling)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
            this.depth = depth;
            this.stencil = stencil;
            this.samples = samples;
            this.coverageSampling = coverageSampling;
        }

        public new string ToString()
        {
            return "r: " + r + ", g: " + g + ", b: " + b + ", a: " + a + ", depth: " + depth + ", stencil: " + stencil
                + ", num samples: " + samples + ", coverage sampling: " + coverageSampling;
        }
    }
}