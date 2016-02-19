using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace malibu
{
    /// <summary>
    /// 微信Oauth2 accessToken实体类
    /// </summary>
    public class WeChatOauth2TokenEntity
    {
        public string Access_token { get; set; }
        public string Expires_in { get; set; }
        public string Refresh_token { get; set; }
        public string Openid { get; set; }
        public string Scope { get; set; }
    }

    public class WeChatAccessTokenEntity
    {
        public string Access_token { get; set; }
        public string Expires_in { get; set; }
    }

    public class WeChatJsapiTicketEntity
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }
    }

    public class WechatJsapiConfig
    {
        public string timestamp;
        public string nonceStr;
        public string signature;
        public string appId;

        public WechatJsapiConfig()
        {
        }
    }

    public class WeChatApiTicketEntity
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }
    }

    public class WechatCardExt
    {
        public string timestamp;
        public string signature;
    }
}