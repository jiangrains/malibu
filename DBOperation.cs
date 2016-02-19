//#define USE_PE
//#define USE_QA
#define USE_IMAXGINE_WECHAT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace malibu
{
    public class DBOperation:IDisposable
    {
        //public static SqlConnection sqlCon;  //用于连接数据库  
  
        //将下面的引号之间的内容换成上面记录下的属性中的连接字符串  
#if USE_IMAXGINE_WECHAT
        private static String ConServerStr = @"Data Source=iZ941e15yquZ;Initial Catalog=imaxgineMalibu;Integrated Security=False;User ID=sa;Password=23Imaxgine";
#endif

#if USE_QA
        private static String ConServerStr = @"Data Source=ewu5ap64qa.database.chinacloudapi.cn,1433;Initial Catalog=imaxgineMalibu;Integrated Security=False;User ID=azureuser;Password=Yung01!@";
#endif

#if USE_PE
        private static String ConServerStr = @"Data Source=s4gzg07gw7.database.chinacloudapi.cn,1433;Initial Catalog=imaxgineMalibu;Integrated Security=False;User ID=azureuser;Password=Yung01!@";
#endif

        //默认构造函数  
        public DBOperation()  
        {   
            /*
            if (sqlCon == null)  
            {  
                sqlCon = new SqlConnection();  
                sqlCon.ConnectionString = ConServerStr;  
                sqlCon.Open();  
            }
            */
        } 

        //关闭/销毁函数，相当于Close()  
        public void Dispose()  
        {  
            /*
            if (sqlCon != null)  
            {  
                sqlCon.Close();  
                sqlCon = null;  
            }  
            */
        }

        public static SqlConnection getSqlConn()
        {
            SqlConnection conn = null;

            conn = new SqlConnection();
            conn.ConnectionString = ConServerStr;
            conn.Open();

            return conn;
        }

        public static void destroySqlConn(SqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }  
        }

        public bool executeSqlStr(string str)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConServerStr;
                conn.Open();

                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                DBOperation.destroySqlConn(conn);

                return true;
            }
            catch (Exception)
            {
                return false;
            }  
        }

        public bool CreateTable()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConServerStr;
                conn.Open();

                string sqlstring = "CREATE TABLE malibutest (id INTEGER NOT Null primary key, value varchar(MAX))";
                SqlCommand cmd = new SqlCommand(sqlstring, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                DBOperation.destroySqlConn(conn);

                return true;
            }
            catch (Exception)
            {
                return false;
            }              
        }
    }
}
