namespace libcdx.Utils
{
    /** A very simple clipboard interface for text content.
    * @author mzechner */
    public interface IClipboard
    {
        /** gets the current content of the clipboard if it contains text
	    * @return the clipboard content or null */
        string GetContents();

        /** Sets the content of the system clipboard.
         * @param content the content */
        void SetContents(string content);
    }
}