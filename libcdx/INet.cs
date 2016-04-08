using libcdx.Net;
using libcdx.Net.Enum;

namespace libcdx
{
    public interface INet
    {
        /** Process the specified {@link HttpRequest} and reports the {@link HttpResponse} to the specified {@link HttpResponseListener}
        * .
        * @param httpRequest The {@link HttpRequest} to be performed.
        * @param httpResponseListener The {@link HttpResponseListener} to call once the HTTP response is ready to be processed. Could
        *           be null, in that case no listener is called. */
        void SendHttpRequest(HttpRequest httpRequest, IHttpResponseListener httpResponseListener);

        void CancelHttpRequest(HttpRequest httpRequest);

        /** Creates a new server socket on the given address and port, using the given {@link Protocol}, waiting for incoming connections.
	     * 
	     * @param hostname the hostname or ip address to bind the socket to
	     * @param port the port to listen on
	     * @param hints additional {@link ServerSocketHints} used to create the socket. Input null to use the default setting provided
	     *           by the system.
	     * @return the {@link ServerSocket}
	     * @throws GdxRuntimeException in case the socket couldn't be opened */
        IServerSocket NewServerSocket(Protocol protocol, string hostname, int port, ServerSocketHints hints);

        /** Creates a new server socket on the given port, using the given {@link Protocol}, waiting for incoming connections.
         * 
         * @param port the port to listen on
         * @param hints additional {@link ServerSocketHints} used to create the socket. Input null to use the default setting provided
         *           by the system.
         * @return the {@link ServerSocket}
         * @throws GdxRuntimeException in case the socket couldn't be opened */
        IServerSocket NewServerSocket(Protocol protocol, int port, ServerSocketHints hints);

        /** Creates a new TCP client socket that connects to the given host and port.
         * 
         * @param host the host address
         * @param port the port
         * @param hints additional {@link SocketHints} used to create the socket. Input null to use the default setting provided by the
         *           system.
         * @return GdxRuntimeException in case the socket couldn't be opened */
        ISocket NewClientSocket(Protocol protocol, string host, int port, SocketHints hints);

        /** Launches the default browser to display a URI. If the default browser is not able to handle the specified URI, the
         * application registered for handling URIs of the specified type is invoked. The application is determined from the protocol
         * and path of the URI. A best effort is made to open the given URI; however, since external applications are involved, no guarantee
         * can be made as to whether the URI was actually opened. If it is known that the URI was not opened, false will be returned; 
         * otherwise, true will be returned.
         * 
         * @param URI the URI to be opened.
         * @return false if it is known the uri was not opened, true otherwise. */
        bool OpenURI(string URI);
    }
}