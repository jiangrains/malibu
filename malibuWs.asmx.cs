//#define USE_IMAXGINE_WECHAT
//#define USE_PE
#define USE_QA

//#define IMAXGINE_DEBUG

#define OPEN_SESSION

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.Net;

using System.Web.Script.Serialization;

using System.Data.SqlClient;
using System.Data;

using System.Web.Security; 



namespace malibu
{
    /// <summary>
    /// malibuWs 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://mychevywechatqa.chinacloudapp.cn/Campaign20160219/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class malibuWs : System.Web.Services.WebService
    {
#if USE_IMAXGINE_WECHAT
        string malibu_entry_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://www.imaxgine.cn/Campaign20160219/malibuWs.asmx/wechat_oauth2_cb&response_type=code&scope=snsapi_userinfo#wechat_redirect";
        string malibu_share_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://www.imaxgine.cn/Campaign20160219/malibuWs.asmx/wechat_oauth2_cb&response_type=code&scope=snsapi_userinfo&state={1}#wechat_redirect";        
        string malibu_loading_page_url = "http://www.imaxgine.cn/Campaign20160219/app.html?openid={0}";
        string malibu_loading_page2_url = "http://www.imaxgine.cn/Campaign20160219/app.html?openid={0}&friend_openid={1}";
        string smsId_code_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/GetSMSIdentifyingCode";
        string validate_code_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/ValidateSMSIdentifyingCode";
        string appId = "wx127e9b641dc9ff55";
        string appSecret = "4a2e07920d2a8380b87904a6a2512ef3";
        static string access_token = null;
        static string jsapi_ticket = null;
        static string api_ticket = null;

        string get_accessToken_url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        string get_jsapi_ticket_url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        string get_api_ticket_url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=wx_card";
#endif

        string taobao_url = "http://t.cn/RGJWOQV";
        string oauth2_get_accessToken_url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        string oauth2_get_userinfo_url = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        string jsapi_signature = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
        string randomStr = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        int[] randomInt = {1,2,3,4,5,6,4,5,6,4,5,6};

        public const int CAR_COLOR_GRAY = 1;
        public const int CAR_COLOR_RED = 2;
        public const int CAR_COLOR_WHITE = 3;
        public const int CAR_COLOR_MAX = 4;

        public const int SCORE_INIT = 0;
        public const int SCORE_MAX = 22;

        public const int STATUS_PLAYING = 0;
        public const int STATUS_REACH_DST = 1;
        public const int STATUS_SAVE_CONTACT = 2;
        public const int STATUS_VOUCHER_RECEIVED = 3;

        public const int TRY_CNT_MAX = 5;

#if USE_PE
        string malibu_entry_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://mychevywechat.chinacloudapp.cn/Campaign20160219/malibuWs.asmx/wechat_oauth2_cb&response_type=code&scope=snsapi_userinfo#wechat_redirect";
        string malibu_share_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://mychevywechat.chinacloudapp.cn/Campaign20160219/malibuWs.asmx/wechat_oauth2_cb&response_type=code&scope=snsapi_userinfo&state={1}#wechat_redirect";
        string malibu_loading_page_url = "http://mychevywechat.chinacloudapp.cn/Campaign20160219/app.html?openid={0}";
        string malibu_loading_page2_url = "http://mychevywechat.chinacloudapp.cn/Campaign20160219/app.html?openid={0}&friend_openid={1}";

        string access_token_interface_url = "http://mychevywechat.chinacloudapp.cn:8081/Wechat/GetAccessToken";
        string jsapi_ticket_interface_url = "http://mychevywechat.chinacloudapp.cn:8081/Wechat/GetJSApiTickect?access_token={0}";
        string api_ticket_interface_url = "http://mychevywechat.chinacloudapp.cn:8081/Wechat/GetApiTicket?access_token={0}";
        string smsId_code_interface_url = "http://mychevywechat.chinacloudapp.cn:8081/Wechat/GetSMSIdentifyingCode";
        string validate_code_interface_url = "http://mychevywechat.chinacloudapp.cn:8081/Wechat/ValidateSMSIdentifyingCode";
        string appId = "wx8c799927cff328c2";
        string appSecret = "d225312c760a39025dd5bf210a04aea1";
#endif

#if USE_QA
        string malibu_entry_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://mychevywechatqa.chinacloudapp.cn/Campaign20160219/malibuWs.asmx/wechat_oauth2_cb&response_type=code&scope=snsapi_userinfo#wechat_redirect";
        string malibu_share_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://mychevywechatqa.chinacloudapp.cn/Campaign20160219/malibuWs.asmx/wechat_oauth2_cb&response_type=code&scope=snsapi_userinfo&state={1}#wechat_redirect";
        string malibu_loading_page_url = "http://mychevywechatqa.chinacloudapp.cn/Campaign20160219/app.html?openid={0}";
        string malibu_loading_page2_url = "http://mychevywechatqa.chinacloudapp.cn/Campaign20160219/app.html?openid={0}&friend_openid={1}";

        string access_token_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/GetAccessToken";
        string jsapi_ticket_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/GetJSApiTickect?access_token={0}";
        string api_ticket_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/GetApiTicket?access_token={0}";
        string smsId_code_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/GetSMSIdentifyingCode";
        string validate_code_interface_url = "http://mychevywechatqa.chinacloudapp.cn:8081/Wechat/ValidateSMSIdentifyingCode";
        string appId = "wx31fce4820ab2a10d";
        string appSecret = "352093e0127481b9c5350715cf81c38b";
#endif

        DBOperation dbOperation = new DBOperation();
        //DBOperation dbOperation = null;

        private string load_data_from_url(string url, bool post, string param)
        {
            string jsonStr = null;
            WebClient client = new WebClient();

            if (post)
            {
                byte[] postData = Encoding.UTF8.GetBytes(param);
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("ContentLength", param.Length.ToString());

                try
                {
                    byte[] responseData = client.UploadData(url, "POST", postData);
                    jsonStr = Encoding.UTF8.GetString(responseData);
                }
                catch (WebException)
                {
                    goto leave;
                }                
            }
            else
            {
                client.Encoding = System.Text.Encoding.GetEncoding("GB2312");

                try
                {
                    byte[] pageData = client.DownloadData(url);
                    jsonStr = Encoding.UTF8.GetString(pageData);
                }
                catch (Exception)
                {
                    goto leave;
                }
            }
leave:            
            return jsonStr;
        }

#if IMAXGINE_DEBUG
        [WebMethod]
        public int getRandomDiceNumber()
#else
        private int getRandomDiceNumber()
#endif
        {
            int length = randomInt.Length;
            Random ran = new Random();
            int delta = ran.Next(0, (length - 1));

            return randomInt[delta];
        }

        private string get_random_string()
        {
            char[] random = new char[16];
            char[] chars = randomStr.ToCharArray();

            int length = randomStr.Length;

            Random ran = new Random();

            for (int i = 0; i < 16; i++)
            {
                random[i] = chars[ran.Next(0, length)];
            }

            string retStr = new String(random);

            return retStr;
        }


#if IMAXGINE_DEBUG
        [WebMethod]
        public string get_access_token()
#else
        private string get_access_token()
#endif
        {
#if USE_IMAXGINE_WECHAT
            return access_token;
#else
            string access_token = null;
            string ExecuteResult_str = null;
            int tryCnt = 0;

            JavaScriptSerializer jss = new JavaScriptSerializer();

            while (tryCnt != TRY_CNT_MAX && ExecuteResult_str == null)
            {
                ExecuteResult_str = load_data_from_url(access_token_interface_url, true, "");
                tryCnt++;
            }
            if (ExecuteResult_str == null)
                goto leave;

            ExecuteResult result;
            result = jss.Deserialize<ExecuteResult>(ExecuteResult_str);
            if (result.IsSuccess == true)
                access_token = result.ReturnObject.ToString();

leave:
            return access_token;
#endif
        }
        

        private string get_jsapi_ticket()
        {
#if USE_IMAXGINE_WECHAT
            return jsapi_ticket;
#else
            string jsapi_ticket = null;
            string access_token = null;
            string ExecuteResult_str = null;
            int tryCnt = 0;
 
            if ((access_token = get_access_token()) == null)
                goto leave;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string url = string.Format(jsapi_ticket_interface_url, access_token);

            while (tryCnt != TRY_CNT_MAX && ExecuteResult_str == null)
            {
                ExecuteResult_str = load_data_from_url(url, false, null);
                tryCnt ++;
            }
            if (ExecuteResult_str == null)
                goto leave;

            ExecuteResult result;
            result = jss.Deserialize<ExecuteResult>(ExecuteResult_str);
            if (result.IsSuccess == true)
                jsapi_ticket = result.ReturnObject.ToString();

leave:
            return jsapi_ticket;
#endif
        }

        private string get_api_ticket()
        {
#if USE_IMAXGINE_WECHAT
            return api_ticket;
#else
            string api_ticket = null;
            string access_token = null;   
            string ExecuteResult_str = null;
            int tryCnt = 0;
         
            if ((access_token = get_access_token()) == null)
                goto leave;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string url = string.Format(api_ticket_interface_url, access_token);

            while (tryCnt != TRY_CNT_MAX && ExecuteResult_str == null)
            {
                ExecuteResult_str = load_data_from_url(url, false, null);
                tryCnt ++;
            }
            if (ExecuteResult_str == null)
                goto leave;

            ExecuteResult result;
            result = jss.Deserialize<ExecuteResult>(ExecuteResult_str);
            if (result.IsSuccess == true)
                api_ticket = result.ReturnObject.ToString();

leave:
            return api_ticket;
#endif
        }

#if IMAXGINE_DEBUG
        [WebMethod]
        public bool validate_smsId_code(string phone, string code)
#else
        private bool validate_smsId_code(string phone, string code)
#endif
        {
            bool validate = true;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string param = "phone=" + phone + "&code=" + code;
            string ExecuteResult_str = load_data_from_url(validate_code_interface_url, true, param);
            ExecuteResult result;
            result = jss.Deserialize<ExecuteResult>(ExecuteResult_str);

            validate = result.IsSuccess;

            return validate;
        }

        private string get_timestamp()
        {
            string timestamp = null;

            DateTime oldTime = new DateTime(1970, 1, 1);
            TimeSpan span = DateTime.Now.Subtract(oldTime);
            int seconds = (int)span.TotalSeconds;
            timestamp = Convert.ToString(seconds);
            return timestamp;
        }

        private WechatJsapiConfig get_jssdk_config(string url)
        {
            string jsapi_ticket = null;
            WechatJsapiConfig config = null;

            if ((jsapi_ticket = get_jsapi_ticket()) == null)
                goto leave;

            config = new WechatJsapiConfig();
            config.nonceStr = get_random_string();
            config.timestamp = get_timestamp();
            config.appId = appId;

            string signature = string.Format(jsapi_signature, jsapi_ticket, config.nonceStr, config.timestamp, url);

            config.signature = FormsAuthentication.HashPasswordForStoringInConfigFile(signature, "SHA1");

leave:
            return config;
        }


        private string get_card_ext(string card_id)
        {
            string card_ext = null;
            string timestamp = null;
            string api_ticket = null;
            string signature = null;


            WechatCardExt card_ext_entity = new WechatCardExt();

            timestamp = get_timestamp();

            if ((api_ticket = get_api_ticket()) == null)
                goto leave;

            string[] str = new string[] {api_ticket,timestamp,card_id};
            Array.Sort(str);

            signature = str[0] + str[1] + str[2];
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(signature, "SHA1");

            card_ext_entity.timestamp = timestamp;
            card_ext_entity.signature = signature;

            card_ext = new JavaScriptSerializer().Serialize(card_ext_entity);

leave:
            return card_ext;
        }

        private AwardInfoEntity get_award_info()
        {
            AwardInfoEntity award = null;
            SqlConnection conn = null;
            string voucher_id1 = null;
            string card_ext = null;

            award = new AwardInfoEntity();            
            conn = DBOperation.getSqlConn();

            string sqlStr = "select * from malibu_voucher_info";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "malibu_voucher_info");
            if (ds.Tables["malibu_voucher_info"].Rows.Count != 1)
            {
                DBOperation.destroySqlConn(conn);
                return null;
            }

            voucher_id1 = ds.Tables["malibu_voucher_info"].Rows[0]["voucher_id"].ToString();

            if ((card_ext = get_card_ext(voucher_id1)) == null)
                goto leave;

            award.voucher_id1 = voucher_id1;
            award.voucher_ext1 = card_ext;
            award.taobao_url = taobao_url;

leave:
            DBOperation.destroySqlConn(conn);
            return award;
        }

#if OPEN_SESSION
        [WebMethod(EnableSession=true)]
#else
        [WebMethod]
#endif
        public void malibu()
        {
            string url;
            string openid = Context.Request.QueryString["openid"];

            if (openid == null)
            {
                url = string.Format(malibu_entry_url, appId);
            }
            else
            {
                url = string.Format(malibu_share_url, appId, openid);
            }

            Context.Response.Redirect(url);
            Context.Response.End();
        }

#if USE_IMAXGINE_WECHAT
        [WebMethod]
        public void wechat_get_accessToken()
        {
            int errCode = 0;

            WeChatAccessTokenEntity myTokenEntity;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string accessTokenUrl = string.Format(get_accessToken_url, appId, appSecret);
            string accessTokenStr = load_data_from_url(accessTokenUrl, false, null);
            myTokenEntity = jss.Deserialize<WeChatAccessTokenEntity>(accessTokenStr);
            access_token = myTokenEntity.Access_token;

leave:
            var tmpobj = new
            {
                errCode,
                access_token
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }

        [WebMethod]
        public void wechat_get_jsapi_ticket()
        {
            int errCode = 0;

            WeChatJsapiTicketEntity myJsapiTokenEntity;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string jsapiTicketUrl = string.Format(get_jsapi_ticket_url, access_token);
            string jsapiTicketStr = load_data_from_url(jsapiTicketUrl, false, null);
            myJsapiTokenEntity = jss.Deserialize<WeChatJsapiTicketEntity>(jsapiTicketStr);

            if (myJsapiTokenEntity.errcode != "0")
            {
                errCode = 1;
                jsapi_ticket = jsapiTicketStr;
            }

            jsapi_ticket = myJsapiTokenEntity.ticket;

        leave:
            var tmpobj = new
            {
                errCode,
                jsapi_ticket
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }

        [WebMethod]
        public void wechat_get_api_ticket()
        {
            int errCode = 0;

            WeChatApiTicketEntity myApiTokenEntity;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string apiTicketUrl = string.Format(get_api_ticket_url, access_token);
            string apiTicketStr = load_data_from_url(apiTicketUrl, false, null);

            myApiTokenEntity = jss.Deserialize<WeChatApiTicketEntity>(apiTicketStr);

            if (myApiTokenEntity.errcode != "0")
            {
                errCode = 1;
                jsapi_ticket = apiTicketStr;
            }

            api_ticket = myApiTokenEntity.ticket;

        leave:
            var tmpobj = new
            {
                errCode,
                api_ticket
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }
#endif



#if OPEN_SESSION
        [WebMethod(EnableSession=true)]
#else
        [WebMethod]
#endif
        public void wechat_oauth2_cb()
        {
            int retCode = 0;

            WeChatOauth2TokenEntity myTokenEntity;
            WeChatUserEntity user = new WeChatUserEntity();
            string oauth2AccessToken;
            string openId;
            string redirect_url;

            string code = Context.Request.QueryString["code"];
            string state = Context.Request.QueryString["state"];  //保存分享者的openid

            if (code == null)
                return;

            JavaScriptSerializer jss = new JavaScriptSerializer();

            string accessTokenUrl = string.Format(oauth2_get_accessToken_url, appId, appSecret, code);
            string accessTokenStr = load_data_from_url(accessTokenUrl, false, null);
            myTokenEntity = jss.Deserialize<WeChatOauth2TokenEntity>(accessTokenStr);
            oauth2AccessToken = myTokenEntity.Access_token;
            openId = myTokenEntity.Openid;

            string userInfoUrl = string.Format(oauth2_get_userinfo_url, oauth2AccessToken, openId);
            string userStr = load_data_from_url(userInfoUrl, false, null);

            //Context.Response.Write(userStr);
            //Context.Response.End();
            //return;

            user = jss.Deserialize<WeChatUserEntity>(userStr);

            SqlConnection conn = null;
            conn = DBOperation.getSqlConn();
            string sqlStr = "select * from user_info where openid='" + user.Openid + "'";
            SqlCommand user_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter user_adapter = new SqlDataAdapter(user_selectCMD);
            DataSet ds = new DataSet();
            user_adapter.Fill(ds, "user_info");
            if (ds.Tables["user_info"].Rows.Count == 0)
            {
                DataRow newRow = ds.Tables["user_info"].NewRow();
                newRow["openid"] = user.Openid;
                newRow["nickname"] = user.Nickname;
                newRow["img_url"] = user.HeadImgUrl;
                ds.Tables["user_info"].Rows.Add(newRow);

                SqlCommandBuilder user_scb = new SqlCommandBuilder(user_adapter);
                user_adapter.Update(ds.Tables["user_info"].GetChanges());
            }
            DBOperation.destroySqlConn(conn);

            //retCode = user.insertUserInfo(dbOperation);

            //Context.Response.Write(retCode);

            if (state == null || state == "")
                redirect_url = string.Format(malibu_loading_page_url, openId);  //自己玩
            else
            {
                if (state == openId)
                    redirect_url = string.Format(malibu_loading_page_url, openId);  //自己玩
                else
                    redirect_url = string.Format(malibu_loading_page2_url, state, openId);  //朋友帮玩                
            }

#if OPEN_SESSION
            Context.Session["openid"] = openId;
#endif
            Context.Response.Redirect(redirect_url);

            Context.Response.End();
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void GetUserInfo(string openid)
        {
            int errCode = 0;
            PlayerInfoEntity player = new PlayerInfoEntity();
            List<UserInfoEntity> friend_list = new List<UserInfoEntity>();
            int friend_cnt = 0;
            WechatJsapiConfig jssdk_config = null;
            UserContactInfoEntity contact = null;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 3;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            string sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter player_adapter = new SqlDataAdapter(player_selectCMD);
            DataSet ds = new DataSet();
            player_adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                player.openid = openid;

                DataRow newRow = ds.Tables["player_info"].NewRow();
                newRow["openid"] = openid;
                newRow["score_self"] = player.score;
                newRow["score"] = player.score;
                newRow["select_car_color"] = player.selected_car_color;
                newRow["shared"] = player.shared;
                newRow["start_time"] = new DateTime(1900, 1, 1);
                newRow["status"] = player.status;
                ds.Tables["player_info"].Rows.Add(newRow);

                SqlCommandBuilder player_scb = new SqlCommandBuilder(player_adapter);
                player_adapter.Update(ds.Tables["player_info"].GetChanges());
            }
            else
            {
                int score = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]);
                int score_self = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score_self"]);

                player.openid = openid;
                player.score_self = score_self;
                player.score = score;
                player.selected_car_color = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["select_car_color"]);
                player.shared = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["shared"]);
                player.status = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["status"]);
            }

            if (player.status >= STATUS_SAVE_CONTACT)
            {
                sqlStr = "select * from user_contact_info where openid='" + openid + "'";
                SqlDataAdapter user_contact_adapter = new SqlDataAdapter(sqlStr,conn);
                user_contact_adapter.Fill(ds, "user_contact_info");
                if (ds.Tables["user_contact_info"].Rows.Count != 1)
                {
                    errCode = 1;
                    goto leave;
                }

                contact = new UserContactInfoEntity();
                contact.openid = openid;
                contact.name = ds.Tables["user_contact_info"].Rows[0]["name"].ToString();
                contact.phonenum = ds.Tables["user_contact_info"].Rows[0]["phonenum"].ToString();
            }

            string url = string.Format(malibu_loading_page_url, openid);
            if ((jssdk_config = get_jssdk_config(url)) == null)
            {
                errCode = 2;
                goto leave;
            }

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                friend_cnt,
                player,
                jssdk_config,
                contact,
                //friend_list
            };

            Context.Session["openid"] = openid;
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void GetUserInfoByFriend(string openid, string friend_openid)
        {
            int errCode = 0;
            int score = 0, score_self = 0;
            string img_url = null;
            bool hasHelp = false;
            WechatJsapiConfig jssdk_config = null;
            PlayerInfoEntity player = new PlayerInfoEntity();

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != friend_openid)
            {
                errCode = 2;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();
            string sqlStr =
            "select user_info.nickname,user_info.img_url,player_info.* "
            + "from user_info,player_info "
            + "where user_info.openid=player_info.openid and player_info.openid='" + openid + "'";
            SqlDataAdapter player_adapter = new SqlDataAdapter(sqlStr, conn);
            DataSet ds = new DataSet();
            player_adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 1;
                goto leave;
            }

            score = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]);
            score_self = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score_self"]);
            img_url = ds.Tables["player_info"].Rows[0]["img_url"].ToString();

            player.openid = openid;
            player.score_self = score_self;
            player.score = score;
            player.selected_car_color = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["select_car_color"]);
            player.status = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["status"]);

            sqlStr = "select * from friend_score_info where openid='" + openid + "' and friend_openid='" + friend_openid +"'";
            SqlDataAdapter friend_adapter = new SqlDataAdapter(sqlStr, conn);
            friend_adapter.Fill(ds, "friend_score_info");
            if (ds.Tables["friend_score_info"].Rows.Count > 0)
                hasHelp = true;

            string url = string.Format(malibu_loading_page2_url, openid, friend_openid);
            if ((jssdk_config = get_jssdk_config(url)) == null)
            {
                errCode = 3;
                goto leave;
            }

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                player,
                img_url,
                hasHelp,
                jssdk_config
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void SelectCarColor(string openid, int color)
        {
            int errCode = 0;
            int select_car_color = 0;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 5;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();
            string sqlStr = "select * from user_info where openid='" + openid + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "user_info");
            if (ds.Tables[0].Rows.Count == 0)
            {
                errCode = 1;
                goto leave;
            }

            if (color <= 0 || color >= CAR_COLOR_MAX)
            {
                errCode = 2;
                goto leave;
            }

            sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter player_adapter = new SqlDataAdapter(player_selectCMD);
            player_adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 3;
                goto leave;
            }

            select_car_color = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["select_car_color"]);
            if (select_car_color != 0)
            {
                errCode = 4;
                goto leave;
            }

            ds.Tables["player_info"].Rows[0]["select_car_color"] = color;
            ds.Tables["player_info"].Rows[0]["start_time"] = DateTime.Now;

            SqlCommandBuilder player_scb = new SqlCommandBuilder(player_adapter);
            player_adapter.Update(ds.Tables["player_info"].GetChanges());

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                openid,
                color,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return ;
        }


#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void GetDiceNumber(string openid, string friend_openid)
        {
            int errCode = 0;
            string errInfo = null;
            int diceNumber = 0;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (friend_openid == null || friend_openid == "")
            {

                if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
                {
                    errCode = 7;
                    errInfo = Context.Session["openid"].ToString();
                    goto leave;
                }
            }
            else
            {
                if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != friend_openid)
                {
                    errCode = 7;
                    errInfo = Context.Session["openid"].ToString();
                    goto leave;
                }
            }
#endif
            conn = DBOperation.getSqlConn();

            if (friend_openid == null || friend_openid == "")
            {
                string sqlStr = "select * from user_info where openid='" + openid + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr,conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "user_info");
                if (ds.Tables["user_info"].Rows.Count != 1)
                {
                    errCode = 1;
                    goto leave;
                }

                sqlStr = "select * from player_info where openid='" + openid + "'";
                SqlCommand selectCMD = new SqlCommand(sqlStr, conn);
                SqlDataAdapter player_adapter = new SqlDataAdapter(selectCMD);
                player_adapter.Fill(ds, "player_info");
                if (ds.Tables["player_info"].Rows.Count != 1)
                {
                    errCode = 2;
                    goto leave;
                }

                //判断是否选择过车的颜色
                if (Convert.ToInt32(ds.Tables["player_info"].Rows[0]["select_car_color"]) == 0)
                {
                    errCode = 3;
                    goto leave;
                }

                //判断是否已经玩过游戏
                if (Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]) != SCORE_INIT)
                {
                    errCode = 4;
                    goto leave;
                }

                /*
                string sqlStr = 
                "select player_info.* "
                +"from user_info,player_info "
                +"where user_info.openid=player_info.openid and player_info.openid='" + openid + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, DBOperation.sqlCon);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "player_info");//ds.Tables["player_info"].Rows]
                if (ds.Tables[0].Rows.Count != 1)
                {
                    errCode = 1;
                    goto leave;
                }
                */

                diceNumber = getRandomDiceNumber();

                ds.Tables["player_info"].Rows[0]["score_self"] = diceNumber;
                ds.Tables["player_info"].Rows[0]["score"] = diceNumber;

                SqlCommandBuilder scb = new SqlCommandBuilder(player_adapter);          
                player_adapter.Update(ds.Tables["player_info"].GetChanges()); //adapter.Update(ds);
                //ds.AcceptChanges();  
                              
            }
            else
            {
                if (openid == friend_openid)
                {
                    errCode = 6;
                    goto leave;
                }

                string sqlStr = "select * from user_info where openid='" + openid + "' or openid='" + friend_openid + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "user_info");
                if (ds.Tables["user_info"].Rows.Count < 2)
                {
                    errCode = 1;
                    goto leave;
                }

                sqlStr = "select * from player_info where openid='" + openid + "'";
                SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
                SqlDataAdapter player_adapter = new SqlDataAdapter(player_selectCMD);
                player_adapter.Fill(ds, "player_info");
                if (ds.Tables["player_info"].Rows.Count != 1)
                {
                    errCode = 2;
                    goto leave;
                }
                //查看朋友当前的得分情况
                int score = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]);

                /*
                if (score == SCORE_INIT)
                {
                    errCode = 3;
                    goto leave;
                }
                else if (score == SCORE_MAX)
                {
                    errCode = 4;
                    goto leave;
                }
                */

                sqlStr = "select * from friend_score_info where openid='" + openid + "' and friend_openid='" + friend_openid + "'";
                SqlCommand friend_score_selectCMD = new SqlCommand(sqlStr, conn);
                SqlDataAdapter friend_score_adapter = new SqlDataAdapter(friend_score_selectCMD);
                friend_score_adapter.Fill(ds, "friend_score_info");
                if (ds.Tables["friend_score_info"].Rows.Count != 0)
                {
                    errCode = 5;
                    goto leave;
                }


                diceNumber = getRandomDiceNumber();

                //ds.Tables["friend_score_info"].Rows.Add(new object[] { ,,,});
                DataRow newRow = ds.Tables["friend_score_info"].NewRow();
                newRow["score"] = diceNumber;
                newRow["play_time"] =  DateTime.Now;
                newRow["openid"] = openid;
                newRow["friend_openid"] = friend_openid;
                ds.Tables["friend_score_info"].Rows.Add(newRow);

                score += diceNumber;
                if (score >= SCORE_MAX)
                {
                    score = SCORE_MAX;
                    ds.Tables["player_info"].Rows[0]["status"] = STATUS_REACH_DST;
                }
                ds.Tables["player_info"].Rows[0]["score"] = score;

                SqlCommandBuilder player_scb = new SqlCommandBuilder(player_adapter);
                SqlCommandBuilder friend_score_scb = new SqlCommandBuilder(friend_score_adapter);
                player_adapter.Update(ds.Tables["player_info"].GetChanges());
                friend_score_adapter.Update(ds.Tables["friend_score_info"].GetChanges());

                //ds.AcceptChanges(); 
            }

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                errInfo,
                diceNumber,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void GetFriendHelpInfo(string openid)
        {
            int errCode = 0;
            int rowCount = 0;
            List<UserInfoEntity> user_list = null;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 1;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            string sqlStr =
            "select user_info.* "
            + "from user_info,friend_score_info "
            + "where user_info.openid=friend_score_info.friend_openid and friend_score_info.openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(player_selectCMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "player_info");

            rowCount = ds.Tables["player_info"].Rows.Count;
            user_list = new List<UserInfoEntity>();

            for (int i = 0; i < rowCount; i ++)
            {
                UserInfoEntity user = new UserInfoEntity();
                user.openid = ds.Tables["player_info"].Rows[i]["openid"].ToString();
                user.nickname = ds.Tables["player_info"].Rows[i]["nickname"].ToString();
                user.img_url = ds.Tables["player_info"].Rows[i]["img_url"].ToString();

                user_list.Add(user);
            }

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                rowCount,
                user_list
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void RecordPv(string openid, string page)
        {
            int errCode = 0;
            string errInfo = null;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 3;
                errInfo = Context.Session["openid"].ToString() + ":" + openid;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            if (page != "index" && page != "chooseCar" && page != "game")
            {
                errCode = 1;
                goto leave;
            }

            string sqlStr = "select * from user_info where openid='" + openid + "'";
            SqlCommand user_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter user_adapter = new SqlDataAdapter(user_selectCMD);
            DataSet ds = new DataSet();
            user_adapter.Fill(ds, "user_info");
            if (ds.Tables["user_info"].Rows.Count != 1)
            {
                errCode = 2;
                goto leave;
            }

            sqlStr = "select * from visit_info where id=1";
            SqlCommand visit_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter visit_adapter = new SqlDataAdapter(visit_selectCMD);
            visit_adapter.Fill(ds, "visit_info");

            DataRow newRow = ds.Tables["visit_info"].NewRow();
            newRow["openid"] = openid;
            newRow["page"] = page;
            newRow["visit_time"] = DateTime.Now;
            ds.Tables["visit_info"].Rows.Add(newRow);

            SqlCommandBuilder share_scb = new SqlCommandBuilder(visit_adapter);
            visit_adapter.Update(ds.Tables["visit_info"].GetChanges());

leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                errInfo,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void RecordShare(string openid, string friend_openid)
        {
            int errCode = 0;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 1;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            string sqlStr = "select * from share_info where id=1";
            SqlCommand share_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter share_adapter = new SqlDataAdapter(share_selectCMD);
            DataSet ds = new DataSet();
            share_adapter.Fill(ds, "share_info");

            DataRow newRow = ds.Tables["share_info"].NewRow();
            newRow["openid"] = openid;
            newRow["friend_openid"] = friend_openid;
            newRow["share_time"] = DateTime.Now;
            ds.Tables["share_info"].Rows.Add(newRow);

            SqlCommandBuilder share_scb = new SqlCommandBuilder(share_adapter);
            share_adapter.Update(ds.Tables["share_info"].GetChanges());

leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }


#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void GetSmsIdCode(string openid, string phone)
        {
            int errCode = 0;
            string errInfo = null;
            string sqlStr = null;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 3;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlDataAdapter player_adapter = new SqlDataAdapter(sqlStr, conn);
            DataSet ds = new DataSet();
            player_adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 1;
                goto leave;
            }

            JavaScriptSerializer jss = new JavaScriptSerializer(); 
            
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.GetEncoding("GB2312");
            string param = "phone=" + phone;
            string ExecuteResult_str = load_data_from_url(smsId_code_interface_url, true, param);

            ExecuteResult result;
            result = jss.Deserialize<ExecuteResult>(ExecuteResult_str);
            if (result.IsSuccess == true)
                goto leave;
            else
            {
                errCode = 2;
                errInfo = ExecuteResult_str;
            }
        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                errInfo,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }



#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void SetUserContactInfo(string openid, string name, string phonenum, string code, string modify)
        {
            int errCode = 0;
            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 7;
                goto leave;
            }
#endif
            //AwardInfoEntity award = null;

            //string modify = Context.Request.QueryString["modify"];

            if (code == "" || code == null)
            {
                errCode = 1;
                goto leave;
            }

            
            if (validate_smsId_code(phonenum, code) == false)
            {
                errCode = 6;
                goto leave;
            }
            

            conn = DBOperation.getSqlConn();
            string sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(player_selectCMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 2;
                goto leave;
            }

            int score = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]);
            int status = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["status"]);

            if (modify == null || modify == "")
            {
                if (score != SCORE_MAX || status != STATUS_REACH_DST)
                {
                    errCode = 3;
                    goto leave;
                }
            }
            else
            {
                if (score != SCORE_MAX || status != STATUS_SAVE_CONTACT)
                {
                    errCode = 3;
                    goto leave;
                }
            }

            sqlStr = "select * from user_contact_info where openid='" + openid + "'";
            SqlCommand user_contact_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter user_contact_adapter = new SqlDataAdapter(user_contact_selectCMD);
            user_contact_adapter.Fill(ds, "user_contact_info");

            if (modify == null || modify == "")
            {
                if (ds.Tables["user_contact_info"].Rows.Count != 0)
                {
                    errCode = 4;
                    goto leave;
                }

                DataRow newRow = ds.Tables["user_contact_info"].NewRow();
                newRow["openid"] = openid;
                newRow["name"] = name;
                newRow["phonenum"] = phonenum;
                newRow["record_time"] = DateTime.Now;
                ds.Tables["user_contact_info"].Rows.Add(newRow);
            }
            else
            {
                if (ds.Tables["user_contact_info"].Rows.Count != 1)
                {
                    errCode = 4;
                    goto leave;
                }

                ds.Tables["user_contact_info"].Rows[0]["name"] = name;
                ds.Tables["user_contact_info"].Rows[0]["phonenum"] = phonenum;
                ds.Tables["user_contact_info"].Rows[0]["record_time"] = DateTime.Now;
            }


            SqlCommandBuilder user_contact_scb = new SqlCommandBuilder(user_contact_adapter);
            user_contact_adapter.Update(ds.Tables["user_contact_info"].GetChanges());

            if (modify == null || modify == "")
            {
                ds.Tables["player_info"].Rows[0]["status"] = STATUS_SAVE_CONTACT;
                SqlCommandBuilder player_scb = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["player_info"].GetChanges());
            }

            /*
            award = get_award_info();
            if (award == null)
            {
                errCode = 5;
                goto leave;
            }
            */

leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                //award
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }


#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void GetAwardInfo(string openid)
        {
            int errCode = 0;
            string errInfo = null;
            AwardInfoEntity award = null;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 3;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            string sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter player_adapter = new SqlDataAdapter(player_selectCMD);
            DataSet ds = new DataSet();
            player_adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 1;
                goto leave;
            }
            int score = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]);
            int status = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["status"]);
            if (score != SCORE_MAX || status < STATUS_SAVE_CONTACT)
            {
                errCode = 2;
                goto leave;
            }

            if ((award = get_award_info()) == null)
            {
                errCode = 4;
                goto leave;
            }

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                award,
                errInfo,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }


#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void SetSharedTag(string openid)
        {
            int errCode = 0;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 2;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            string sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter player_adapter = new SqlDataAdapter(player_selectCMD);
            DataSet ds = new DataSet();
            player_adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 1;
                goto leave;
            }

            int shared = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["shared"]);
            if (shared == 0)
            {
                ds.Tables["player_info"].Rows[0]["shared"] = 1;
                SqlCommandBuilder player_scb = new SqlCommandBuilder(player_adapter);
                player_adapter.Update(ds.Tables["player_info"].GetChanges());
            }

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }


#if OPEN_SESSION
        [WebMethod(EnableSession = true)]
#else
        [WebMethod]
#endif
        public void SetVoucherReceived(string openid, string voucher_id1)
        {
            int errCode = 0;

            SqlConnection conn = null;
#if OPEN_SESSION
            if (Context.Session["openid"] == null || Context.Session["openid"].ToString() != openid)
            {
                errCode = 4;
                goto leave;
            }
#endif
            conn = DBOperation.getSqlConn();

            string sqlStr = "select * from player_info where openid='" + openid + "'";
            SqlCommand player_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(player_selectCMD);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "player_info");
            if (ds.Tables["player_info"].Rows.Count != 1)
            {
                errCode = 1;
                goto leave;
            }

            int score = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["score"]);
            int status = Convert.ToInt32(ds.Tables["player_info"].Rows[0]["status"]);

            if (score != SCORE_MAX || status != STATUS_SAVE_CONTACT)
            {
                errCode = 2;
                goto leave;
            }

            sqlStr = "select * from malibu_voucher_info where voucher_id='" + voucher_id1 + "'";
            SqlDataAdapter malibu_voucher_adapter = new SqlDataAdapter(sqlStr, conn);
            malibu_voucher_adapter.Fill(ds, "malibu_voucher_info");
            if (ds.Tables["malibu_voucher_info"].Rows.Count != 1)
            {
                errCode = 3;
                goto leave;
            }

            ds.Tables["player_info"].Rows[0]["status"] = STATUS_VOUCHER_RECEIVED;
            
            SqlCommandBuilder player_scb = new SqlCommandBuilder(adapter);
            adapter.Update(ds.Tables["player_info"].GetChanges());
        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode,
                voucher_id1,
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }

        [WebMethod]
        public void SetVoucherInfo(string name, string password, string voucher_id1, int voucher_cnt1)
        {
            int errCode = 0;
            SqlConnection conn = null;

            conn = DBOperation.getSqlConn();
            string sqlStr = "select * from malibu_admin_info where admin_name='" + name + "' and admin_password='" + password + "'";
            //SqlCommand player_selectCMD = new SqlCommand(sqlStr, DBOperation.sqlCon);
            SqlDataAdapter malibu_admin_adapter = new SqlDataAdapter(sqlStr, conn);
            DataSet ds = new DataSet();
            malibu_admin_adapter.Fill(ds, "malibu_admin_info");
            if (ds.Tables["malibu_admin_info"].Rows.Count != 1)
            {
                errCode = 2;
                goto leave;
            }

            sqlStr = "select * from malibu_voucher_info";
            SqlCommand malibu_voucher_selectCMD = new SqlCommand(sqlStr, conn);
            SqlDataAdapter malibu_voucher_adapter = new SqlDataAdapter(malibu_voucher_selectCMD);
            malibu_voucher_adapter.Fill(ds, "malibu_voucher_info");
            if (ds.Tables["malibu_voucher_info"].Rows.Count == 0)
            {
                DataRow newRow1 = ds.Tables["malibu_voucher_info"].NewRow();
                newRow1["id"] = 1;
                newRow1["voucher_id"] = voucher_id1;
                newRow1["voucher_cnt"] = voucher_cnt1;
                ds.Tables["malibu_voucher_info"].Rows.Add(newRow1);
            }
            else if (ds.Tables["malibu_voucher_info"].Rows.Count == 1)
            {
                ds.Tables["malibu_voucher_info"].Rows[0]["voucher_id"] = voucher_id1;
                ds.Tables["malibu_voucher_info"].Rows[0]["voucher_cnt"] = voucher_cnt1;
            }
            else
            {
                errCode = 3;
            }

            SqlCommandBuilder malibu_voucher_scb = new SqlCommandBuilder(malibu_voucher_adapter);
            malibu_voucher_adapter.Update(ds.Tables["malibu_voucher_info"].GetChanges());

        leave:
            DBOperation.destroySqlConn(conn);
            var tmpobj = new
            {
                errCode
            };
            Context.Response.Write(new JavaScriptSerializer().Serialize(tmpobj));
            return;
        }

#if IMAXGINE_DEBUG
        [WebMethod]
        public void DeleteAllData()
#else
        private void DeleteAllData()
#endif
        {
            SqlConnection conn = null;
            conn = DBOperation.getSqlConn();

            string sqlStr = "delete from friend_score_info";
            SqlDataAdapter friend_score_adapter = new SqlDataAdapter(sqlStr, conn);
            DataSet ds = new DataSet();
            friend_score_adapter.Fill(ds, "friend_score_info");

            sqlStr = "delete from player_info";
            SqlDataAdapter player_adapter = new SqlDataAdapter(sqlStr, conn);
            player_adapter.Fill(ds, "player_info");

            sqlStr = "delete from user_contact_info";
            SqlDataAdapter user_contact_adapter = new SqlDataAdapter(sqlStr, conn);
            user_contact_adapter.Fill(ds, "user_contact_info");

            sqlStr = "delete from user_info";
            SqlDataAdapter user_adapter = new SqlDataAdapter(sqlStr, conn);
            user_adapter.Fill(ds, "user_info");

            return;
        }

        /*
        [WebMethod]
        public void setCardWhiteList()
        {
            int errCode = 0;
            string access_token = null;

            string set_white_list_url = "https://api.weixin.qq.com/card/testwhitelist/set?access_token={0}";
            //string param = "{\"openid\": [\"oBzWvuA5YOj1syshfhFwK0wESdho\"], \"username\":[\"开发测试号，勿联系！\"]}";
            string param = "{\"openid\": [\"oBzWvuOioSdfGGf0T7b8adymjSFQ\"]}";

            access_token = get_access_token();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.GetEncoding("GB2312");

            set_white_list_url = string.Format(set_white_list_url, access_token);
            
            string ExecuteResult_str = load_data_from_url(set_white_list_url, true, param);

            Context.Response.Write(ExecuteResult_str);

            return;
        }
        */

        /*
        [WebMethod]
        public int CreateDatabaseTable()
        {
            int errCode = 0;

            if (dbOperation == null)
                return 2;

            if (!dbOperation.CreateTable())
                errCode = 1;

            return errCode;
        }
        */
#if USE_IMAXGINE_WECHAT
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void wechatCheckSignature(string signature, string timestamp, string nonce, string echostr)
        {
            Context.Response.Write(echostr);
            Context.Response.End();
        }
#endif
        
    }
}
