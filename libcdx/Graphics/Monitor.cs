namespace libcdx.Graphics
{
    /// <summary>
    /// Describes a monitor
    /// </summary>
    public class Monitor
    {
        public readonly int virtualX;
        public readonly int virtualY;
        public readonly string name;

        protected Monitor(int virtualX, int virtualY, string name)
        {
            this.virtualX = virtualX;
            this.virtualY = virtualY;
            this.name = name;
        }
    }
}