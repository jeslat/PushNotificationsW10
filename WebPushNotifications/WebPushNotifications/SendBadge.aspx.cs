using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WebPushNotifications
{
    public partial class SendBadge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSendBadge_Click(object sender, EventArgs e)
        {
            TextBoxResponse.Text = string.Empty;

            var secret = TextBoxClientSecret.Text;
            var sid = TextBoxPackageSID.Text;
            var notificationType = "wns/badge";
            var contentType = "text/xml";
            var uri = TextBoxUri.Text;

            var badge = "<badge value=\"" + TextBoxNumber.Text + "\" version?=integer/>";

            try
            {
                // You should cache this access token.
                var accessToken = GetAccessToken(secret, sid);

                byte[] contentInBytes = Encoding.UTF8.GetBytes(badge);

                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.Method = "POST";
                request.Headers.Add("X-WNS-Type", notificationType);
                request.ContentType = contentType;
                request.Headers.Add("Authorization", string.Format("Bearer {0}", accessToken.AccessToken));

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(contentInBytes, 0, contentInBytes.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    TextBoxResponse.Text = webResponse.StatusCode.ToString();
                }
            }

            catch (WebException webException)
            {
                HttpStatusCode status = ((HttpWebResponse)webException.Response).StatusCode;

                if (status == HttpStatusCode.Unauthorized)
                {
                    // The access token you presented has expired. Get a new one and then try sending your notification again.
                    // Because your cached access token expires after 24 hours, you can expect to get this response from WNS at least once a day.
                    // We recommend that you implement a maximum retry policy.
                }
                else if (status == HttpStatusCode.Gone || status == HttpStatusCode.NotFound)
                {
                    // The channel URI is no longer valid.
                    // Remove this channel from your database to prevent further attempts to send notifications to it.
                    // The next time that this user launches your app, request a new WNS channel.
                    // Your app should detect that its channel has changed, which should trigger the app to send the new channel URI to your app server.
                }
                else if (status == HttpStatusCode.NotAcceptable)
                {
                    // This channel is being throttled by WNS.
                    // Implement a retry strategy that exponentially reduces the amount of notifications being sent in order to prevent being throttled again.
                    // Also, consider the scenarios that are causing your notifications to be throttled. 
                    // You will provide a richer user experience by limiting the notifications you send to those that add true value.
                }
                else
                {
                    // WNS responded with a less common error. Log this error to assist in debugging.
                    // You can see a full list of WNS response codes here:
                    // http://msdn.microsoft.com/en-us/library/windows/apps/hh868245.aspx#wnsresponsecodes
                }

                string[] debugOutput = {
                                       status.ToString(),
                                       webException.Response.Headers["X-WNS-Debug-Trace"],
                                       webException.Response.Headers["X-WNS-Error-Description"],
                                       webException.Response.Headers["X-WNS-Msg-ID"],
                                       webException.Response.Headers["X-WNS-Status"]
                                   };
                TextBoxResponse.Text = string.Join(" | ", debugOutput);
                return;
            }
            catch (Exception ex)
            {
                TextBoxResponse.Text = "EXCEPTION: " + ex.Message;
                return;
            }
        }

        public class OAuthToken
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

        private OAuthToken GetOAuthTokenFromJson(string jsonString)
        {
            var oAuthToken = JsonConvert.DeserializeObject<OAuthToken>(jsonString);
            return oAuthToken;
        }

        protected OAuthToken GetAccessToken(string secret, string sid)
        {
            var urlEncodedSecret = HttpUtility.UrlEncode(secret);
            var urlEncodedSid = HttpUtility.UrlEncode(sid);

            var body = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com",
                                     urlEncodedSid,
                                     urlEncodedSecret);

            string response;
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                response = client.UploadString("https://login.live.com/accesstoken.srf", body);
            }
            return GetOAuthTokenFromJson(response);
        }
    }
}