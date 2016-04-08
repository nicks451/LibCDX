using System.IO;

namespace libcdx.Net
{
    /** A client socket that talks to a server socket via some {@link Protocol}. See
     * {@link Net#newClientSocket(Protocol, String, int, SocketHints)} and
     * {@link Net#newServerSocket(Protocol, int, ServerSocketHints)}.</p>
     * 
     * A socket has an {@link InputStream} used to send data to the other end of the connection, and an {@link OutputStream} to
     * receive data from the other end of the connection.</p>
     * 
     * A socket needs to be disposed if it is no longer used. Disposing also closes the connection.
     * 
     * @author mzechner */
    public interface ISocket
    {
        /** @return whether the socket is connected */
        bool IsConnected();

        /** @return the {@link InputStream} used to read data from the other end of the connection. */
        MemoryStream GetInputStream();

        /** @return the {@link OutputStream} used to write data to the other end of the connection. */
        MemoryStream GetOutputStream();

        /** @return the RemoteAddress of the Socket as String */
        string GetRemoteAddress();
    }
}