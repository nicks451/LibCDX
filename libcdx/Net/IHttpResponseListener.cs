using System;

namespace libcdx.Net
{
    public interface IHttpResponseListener
    {
        /** Called when the {@link HttpRequest} has been processed and there is a {@link HttpResponse} ready. Passing data to the
		 * rendering thread should be done using {@link Application#postRunnable(java.lang.Runnable runnable)} {@link HttpResponse}
		 * contains the {@link HttpStatus} and should be used to determine if the request was successful or not (see more info at
		 * {@link HttpStatus#getStatusCode()}). For example:
		 * 
		 * <pre>
		 *  HttpResponseListener listener = new HttpResponseListener() {
		 *  	public void handleHttpResponse (HttpResponse httpResponse) {
		 *  		HttpStatus status = httpResponse.getStatus();
		 *  		if (status.getStatusCode() >= 200 && status.getStatusCode() < 300) {
		 *  			// it was successful
		 *  		} else {
		 *  			// do something else
		 *  		}
		 *  	}
		 *  }
		 * </pre>
		 * 
		 * @param httpResponse The {@link HttpResponse} with the HTTP response values. */
        void HandleHttpResponse(IHttpResponse httpResponse);

        /** Called if the {@link HttpRequest} failed because an exception when processing the HTTP request, could be a timeout any
		 * other reason (not an HTTP error).
		 * @param t If the HTTP request failed because an Exception, t encapsulates it to give more information. */
        void Failed(Exception e);

        void Cancelled();
    }
}