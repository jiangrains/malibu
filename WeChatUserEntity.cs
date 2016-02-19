using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace malibu
{
    /// <summary>
    /// 用户实体信息类
    /// </summary>
    public class WeChatUserEntity
    {
        public string Subscribe { get; set; }
        public string Openid { get; set; }
        public string Nickname { get; set; }
        public string Sex { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string HeadImgUrl { get; set; }
        public string Subscribe_time { get; set; }
        public string Language { get; set; }

        public int insertUserInfo(DBOperation dbOperation)
        {
            int retCode = 0;

            try
            {
                string sqlstr = "insert into user_info (openid,nickname,img_url) values ('" + this.Openid + "','" + this.Nickname + "','" + this.HeadImgUrl + "')";
                dbOperation.executeSqlStr(sqlstr);
            }
            catch (Exception)
            {
                retCode = -1;
            }  

            return retCode;
        }
    }

    public class UserInfoEntity
    {
        public string openid;
        public string nickname;
        public string img_url;

        public UserInfoEntity()
        {
        }
    }

    public class PlayerInfoEntity
    {
        public string openid;
        public int score_self;
        public int score;
        public int selected_car_color;
        public int shared;
        public DateTime? start_time;
        //public DateTime last_share_date;
        public int status; //0代表尚未到达终点；1代表到达终点，尚未留下联系方式；2代表已留下联系方式，但尚未领取卡券；3代表已领取卡券

        public PlayerInfoEntity()
        {
        }

        public PlayerInfoEntity(string openid, int car_color)
        {
            this.openid = openid;
            this.score_self = 0;
            this.score = 0;
            this.selected_car_color = car_color;
            this.shared = 0;
            this.start_time = null;
            this.status = 0;
        }

        public int insertPlayerInfoEntity(DBOperation dbOperation)
        {
            int retCode = 0;
            try
            {
                string sqlstr = "insert into player_info values('" + this.openid + "'," + this.score_self + "," + this.score + "," + this.selected_car_color
                    + "," + this.shared + ",'" + this.start_time + "'," + this.status + ")";
                dbOperation.executeSqlStr(sqlstr);
            }
            catch (Exception)
            {
                retCode = -1;
            }
            return retCode;
        }
    }

    public class UserContactInfoEntity
    {
        public string openid;
        public string name;
        public string phonenum;
    }

    public class FriendScoreInfoEntity
    {
        public int id;
        public int score;
        public DateTime play_time;
        public string openid;
        public string friend_openid;
    }

    public class AwardInfoEntity
    {
        public string voucher_id1;
        public string voucher_ext1;
        public string voucher_id2;
        public string voucher_ext2;
        public string taobao_url;
    }

    public class ExecuteResult
    {
        public bool IsSuccess;
        public string ErrorDesc; 
        public int RecordNum;
        public object ReturnObject;
    }
}