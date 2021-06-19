using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 組別作品
{
    public partial class addToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Session["cart"]+=Request.QueryString["productID"]+"&";//存入的是字串，所以用&分隔開，要用時要另外解析
            //if (Session[Request.QueryString["productID"]]!=)
            //{

            //}
            //Session[Request.QueryString["productID"]]=Session[$"{Request.QueryString["productID"]}"]+1;
            //int j = Session.Contents.Count;
            //for (int i = 0; i <j; i++)
            //{
            //    Response.Write(Session.Contents[i]+"</br>");
            //}
                
       
//==============================================
            ////加到訂單sql
            //string s_data = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=測試用資料庫01;Integrated Security=True";
            //SqlConnection connection = new SqlConnection(s_data);
            //connection.Open();
            //string sql = $"insert into [Orderdetail](productID) values (@productID)";
            //SqlCommand command = new SqlCommand(sql, connection);

            //command.Parameters.Add("@productID", SqlDbType.Int);
            //command.Parameters["@productID"].Value = Request.QueryString["productID"];
            //command.ExecuteNonQuery();

            //connection.Close();
//===============================================

            //Server.Transfer("productList.aspx");//轉回頁面


        }
    }
}