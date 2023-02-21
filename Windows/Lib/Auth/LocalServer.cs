using System;
using System.Net;
using System.Threading.Tasks;

namespace CLI
{
    public static class LocalServer
    {
        private static HttpListener _listener;

        public static async Task<string> StartServerAsync(string prefix)
        {
            var auth0Information = String.Empty;
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
            _listener.Start();

            Console.WriteLine("Listening for redirects...");

            while (true)
            {
                var context = await _listener.GetContextAsync();
                var response = context.Response;

                var message = "Authentication complete. You can now close this window and return to the CLI. <a href='/cli-logout'>Logout and try again</a>.  <a href='https://aicapture.io/'>AIC Home</a> (<a href='https://localhost:7033/'>Localhost Home</a>).";
                var responseBytes = System.Text.Encoding.UTF8.GetBytes(message);

                response.ContentLength64 = responseBytes.Length;
                response.ContentType = "text/html";
                response.OutputStream.Write(responseBytes, 0, responseBytes.Length);


                auth0Information = context.Request.QueryString["auth0Information"];

                // Use the auth0Information to make API requests to your backend
                // ...

                response.Close();
                break;
            }

            _listener.Stop();
            return auth0Information;
        }
    }
}
