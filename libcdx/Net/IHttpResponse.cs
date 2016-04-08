using System;
using System.Collections.Generic;
using System.IO;

namespace libcdx.Net
{
    public interface IHttpResponse
    {
        /** Returns the data of the HTTP response as a byte[].
        * <p>
        * <b>Note</b>: This method may only be called once per response.
        * </p>
        * @return the result as a byte[] or null in case of a timeout or if the operation was canceled/terminated abnormally. The
        *         timeout is specified when creating the HTTP request, with {@link HttpRequest#setTimeOut(int)} */
        byte[] GetResult();

        /** Returns the data of the HTTP response as a {@link String}.
		 * <p>
		 * <b>Note</b>: This method may only be called once per response.
		 * </p>
		 * @return the result as a string or null in case of a timeout or if the operation was canceled/terminated abnormally. The
		 *         timeout is specified when creating the HTTP request, with {@link HttpRequest#setTimeOut(int)} */
        String GetResultAsString();

        /** Returns the data of the HTTP response as an {@link InputStream}. <b><br>
		 * Warning:</b> Do not store a reference to this InputStream outside of
		 * {@link HttpResponseListener#handleHttpResponse(HttpResponse)}. The underlying HTTP connection will be closed after that
		 * callback finishes executing. Reading from the InputStream after it's connection has been closed will lead to exception.
		 * @return An {@link InputStream} with the {@link HttpResponse} data. */
        MemoryStream GetResultAsStream();

        /** Returns the {@link HttpStatus} containing the statusCode of the HTTP response. */
        HttpStatus GetStatus();

        /** Returns the value of the header with the given name as a {@link String}, or null if the header is not set. See
		 * {@link HttpResponseHeader}. */
        String getHeader(string name);

        /** Returns a Map of the headers. The keys are Strings that represent the header name. Each values is a List of Strings that
		 * represent the corresponding header values. See {@link HttpResponseHeader}. */
        Dictionary<String, List<string>> getHeaders();
    }
}