using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Market;
using ProtoBuf;

namespace AndroidMarketApi
{
    public sealed class MarketSession
    {
        public MarketSession()
        {
            Context = new RequestContext
            {
                Unknown1 = Constant.DefaultContext.Unknown1,
                Version = Constant.DefaultContext.Version,
                AndroidId = Constant.DefaultContext.AndroidId,
                DeviceAndSdkVersion = Constant.DefaultContext.DeviceAndSdk
            };

            Locale = CultureInfo.GetCultureInfo(1033);
            Operator = Operator.TMobile;
        }

        #region Properties

        public RequestContext Context { get; private set; }

        private string authSubToken;
        public string AuthSubToken
        {
            get
            {
                return authSubToken;
            }
            set
            {
                Context.AuthSubToken = authSubToken = value;
            }
        }

        public CultureInfo Locale
        {
            set
            {
                var name = value.Name.Split('-');
                Context.UserLanguage = name[0].ToLower();
                Context.UserCountry = name[1].ToLower();
            }
        }

        ///	 <summary> * http://www.2030.tk/wiki/Android_market_switch </summary>
        public Operator Operator
        {
            set
            {
                Context.OperatorAlpha = value.Alpha;
                Context.SimOperatorAlpha = value.SimAlpha;
                Context.OperatorNumeric = value.Numeric;
                Context.SimOperatorNumeric = value.SimNumeric;
            }
        }

        #endregion

        #region Public Methods
        public void Login(string email, string password)
        {
            Login(email, password, Constant.AccountType.HostedOrGoogle);
        }

        public void Login(string email, string password, string accountType)
        {
            AuthSubToken = HttpAuthenticate(accountType, email, password);
        }

        #region Retrievers
        public AppsResponse Get(AppsRequest req)
        {
            return Exec(req, r => r.AppsResponse);
        }

        public CommentsResponse Get(CommentsRequest req)
        {
            return Exec(req, r => r.CommentsResponse);
        }

        public CategoriesResponse Get(CategoriesRequest req)
        {
            return Exec(req, r => r.CategoriesResponse);
        }

        public SubCategoriesResponse Get(SubCategoriesRequest req)
        {
            return Exec(req, r => r.SubCategoriesResponse);
        }

        public GetImageResponse Get(GetImageRequest req)
        {
            return Exec(req, r => r.ImageResponse);
        }
        #endregion
        #endregion

        #region Private Methods

        private static string HttpAuthenticate(string accountType, string email, string password)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constant.Url.Login));

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            string requestData = string.Concat("Email=", email, "&Passwd=", password, "&service=", Constant.Config.Service, "&accountType=", accountType);

            byte[] requestBuffer = Encoding.UTF8.GetBytes(requestData);

            req.KeepAlive = false;
            req.ContentLength = requestBuffer.Length;

            Stream postDataStream = req.GetRequestStream();
            postDataStream.Write(requestBuffer, 0, requestBuffer.Length);
            postDataStream.Close();

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();

            string res;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                while (sr.Peek() > 0)
                {
                    res = sr.ReadLine();
                    if (res.StartsWith("Auth="))
                        return res.Substring(5);
                }
            }

            return null;
        }

        private TRes Exec<TReq, TRes>(TReq req, Func<Response.ResponseGroup, TRes> retriever)
        {
            var r = new Request { Context = Context };
            r.Requestgroup.Add(new Request.RequestGroup
            {
                AppsRequest = req as AppsRequest,
                CategoriesRequest = req as CategoriesRequest,
                CommentsRequest = req as CommentsRequest,
                ImageRequest = req as GetImageRequest,
                SubCategoriesRequest = req as SubCategoriesRequest
            });

            Response res = SubmitHttpRequest(r);
            return retriever.Invoke(res.Responsegroup[0]);
        }

        private static string GetParams(Request request)
        {
            byte[] bytes;

            using (MemoryStream ms = new MemoryStream(2000))
            {
                Serializer.Serialize(ms, request);
                bytes = ms.ToArray();
            }

            string encodedData = Convert.ToBase64String(bytes);

            return string.Concat("version=", Constant.Config.ProtocolVersion, "&request=", encodedData);
        }

        private Response SubmitHttpRequest(Request request)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(Constant.Url.ApiRequest));
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2";
            req.UserAgent = "Android-Market/2 (sapphire PLAT-RC33); gzip";

            req.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(new Cookie("ANDROID", AuthSubToken) { Domain = "android.clients.google.com" });

            string requestData = GetParams(request);

            byte[] requestBuffer = Encoding.UTF8.GetBytes(requestData);

            req.KeepAlive = false;
            req.ContentLength = requestBuffer.Length;

            Stream postDataStream = req.GetRequestStream();
            postDataStream.Write(requestBuffer, 0, requestBuffer.Length);
            postDataStream.Close();

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();

            Stream stream = response.GetResponseStream();
            var res = new GZipStream(stream, CompressionMode.Decompress);

            return Serializer.Deserialize<Response>(res);
        }

        #endregion

    }
}
