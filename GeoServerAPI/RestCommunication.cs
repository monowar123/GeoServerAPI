using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeoServerAPI
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RestCommunication
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public RestCommunication()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }

        public RestCommunication(string endPoint)
        {
            EndPoint = endPoint;
            Method = HttpVerb.GET;
            ContentType = "application/json; charset=utf-8";
            PostData = "";
        }

        public RestCommunication(string endPoint, HttpVerb method)
        {
            EndPoint = endPoint;
            Method = method;
            ContentType = "application/json; charset=utf-8";
            PostData = "";
        }

        public RestCommunication(string endPoint, HttpVerb method, string postData)
        {
            EndPoint = endPoint;
            Method = method;
            ContentType = "application/json; charset=utf-8";
            PostData = postData;
        }

        public string MakeRequest()
        {
            try
            {
                HttpWebRequest request = CreateHttpWebRequest();

                if (!string.IsNullOrEmpty(PostData) && (Method == HttpVerb.POST || Method == HttpVerb.PUT))
                {
                    var encoding = new UTF8Encoding();
                    var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (request.Method != HttpVerb.GET.ToString())
                    {
                        responseValue = response.StatusCode.ToString();
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue += reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string MakeRequest(byte[] shapeFile)
        {
            try
            {
                var request = CreateHttpWebRequest();
                request.ContentLength = shapeFile.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(shapeFile, 0, shapeFile.Length);
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (request.Method != HttpVerb.GET.ToString())
                    {
                        responseValue = response.StatusCode.ToString();
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue += reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #region Private Methods

        private HttpWebRequest CreateHttpWebRequest()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPoint);
            httpWebRequest.ContentType = ContentType; //"application/zip"; //"application/json; charset=utf-8";
            httpWebRequest.Method = Method.ToString();

            string user = Utilities.GetAppConfigValue("GeoServerUser");
            string password = Utilities.GetAppConfigValue("GeoServerPass");

            NetworkCredential cred = new NetworkCredential(user, password);
            httpWebRequest.Credentials = cred;

            return httpWebRequest;
        }

        #endregion Private Methods
    }
}
