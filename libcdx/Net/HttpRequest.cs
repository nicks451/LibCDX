using System;
using System.Collections.Generic;
using System.IO;
using libcdx.Utils;

namespace libcdx.Net
{
    public class HttpRequest 
    {
        private string httpMethod;
        private string url;
        private Dictionary<string, string> headers;
        private int timeOut = 0;

        private string content;
        private MemoryStream contentStream;
        private long contentLength;

        private bool followRedirects = true;

        private bool includeCredentials = false;

        public HttpRequest()
        {
            this.headers = new Dictionary<string, string>();
        }

        /** Creates a new HTTP request with the specified HTTP method, see {@link HttpMethods}.
		 * @param httpMethod This is the HTTP method for the request, see {@link HttpMethods} */
        public HttpRequest(string httpMethod) : this()
        {
            this.httpMethod = httpMethod;
        }

        /** Sets the URL of the HTTP request.
		 * @param url The URL to set. */
        public void SetUrl(string url)
        {
            this.url = url;
        }

        /** Sets a header to this HTTP request, see {@link HttpRequestHeader}.
		 * @param name the name of the header.
		 * @param value the value of the header. */
        public void SetHeader(string name, string value)
        {
            headers.Add(name, value);
        }

        /** Sets the content to be used in the HTTP request.
		 * @param content A string encoded in the corresponding Content-Encoding set in the headers, with the data to send with the
		 *           HTTP request. For example, in case of HTTP GET, the content is used as the query string of the GET while on a
		 *           HTTP POST it is used to send the POST data. */
        public void SetContent(string content)
        {
            this.content = content;
        }

        /** Sets the content as a stream to be used for a POST for example, to transmit custom data.
		 * @param contentStream The stream with the content data. */
        public void SetContent(MemoryStream contentStream, long contentLength)
        {
            this.contentStream = contentStream;
            this.contentLength = contentLength;
        }

        /** Sets the time to wait for the HTTP request to be processed, use 0 block until it is done. The timeout is used for both
		 * the timeout when establishing TCP connection, and the timeout until the first byte of data is received.
		 * @param timeOut the number of milliseconds to wait before giving up, 0 or negative to block until the operation is done */
        public void SetTimeOut(int timeOut)
        {
            this.timeOut = timeOut;
        }

        /** Sets whether 301 and 302 redirects are followed. By default true. Can't be changed in the GWT backend because this uses
		 * XmlHttpRequests which always redirect.
		 * @param followRedirects whether to follow redirects.
		 * @exception IllegalArgumentException if redirection is disabled on the GWT backend. */
        public void SetFollowRedirects(bool followRedirects)
        {
			if (followRedirects || Cdx.App.GetApplicationType() != ApplicationType.WebGL) 
            {
                this.followRedirects = followRedirects;
            } 
            else 
            {
                throw new ArgumentException("Following redirects can't be disabled using the GWT/WebGL backend!");
            }
        }

        /** Sets whether a cross-origin request will include credentials. Only used on GWT backend to allow cross-origin requests
		 * to include credentials such as cookies, authorization headers, etc... */
        public void SetIncludeCredentials(bool includeCredentials)
        {
            this.includeCredentials = includeCredentials;
        }

        /** Sets the HTTP method of the HttpRequest. */
        public void SetMethod(string httpMethod)
        {
            this.httpMethod = httpMethod;
        }

        /** Returns the timeOut of the HTTP request.
		 * @return the timeOut. */
        public int GetTimeOut()
        {
            return timeOut;
        }

        /** Returns the HTTP method of the HttpRequest. */
        public string GetMethod()
        {
            return httpMethod;
        }

        /** Returns the URL of the HTTP request. */
        public string GetUrl()
        {
            return url;
        }

        /** Returns the content string to be used for the HTTP request. */
        public string GetContent()
        {
            return content;
        }

        /** Returns the content stream. */
        public MemoryStream GetContentStream()
        {
            return contentStream;
        }

        /** Returns the content length in case content is a stream. */
        public long GetContentLength()
        {
            return contentLength;
        }

        /** Returns a Map<String, String> with the headers of the HTTP request. */
        public Dictionary<string, String> GetHeaders()
        {
            return headers;
        }

        /** Returns whether 301 and 302 redirects are followed. By default true. Whether to follow redirects. */
        public bool GetFollowRedirects()
        {
            return followRedirects;
        }

        /** Returns whether a cross-origin request will include credentials. By default false. */
        public bool GetIncludeCredentials()
        {
            return includeCredentials;
        }

        public void Reset()
        {
            httpMethod = null;
            url = null;
            headers.Clear();
            timeOut = 0;

            content = null;
            contentStream = null;
            contentLength = 0;

            followRedirects = true;
        }
    }
}