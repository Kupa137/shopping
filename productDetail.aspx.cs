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
    public partial class WebForm2 : System.Web.UI.Page
    {
        public string s_data = SiteMaster.s_data;
        public string custermer = "123@123";//可變動的
        public int custermerID = 1;//可變動的


        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(s_data);
            SqlCommand command = new SqlCommand($"select * from products where id={Request.QueryString["productID"]}", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lb_title.Text = ""+reader["title"];
                    lb_price.Text = "" + reader["price"];
                    lb_describe.Text = "" + reader["describe"];
                }
            }
            connection.Close();





        }

        protected void btn_addToCart_Click(object sender, EventArgs e)
        {
         //把購物車存到sql
                Button product = (Button)sender;
                SqlConnection connection = new SqlConnection(s_data);
                connection.Open();
                string sql = $"insert into[cart_{custermer}](productID)values(@productID)";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.Add("@productID", SqlDbType.Int);
                sqlCommand.Parameters["@productID"].Value = Request.QueryString["productID"];
                sqlCommand.ExecuteNonQuery();
                connection.Close();
        }




    }
}