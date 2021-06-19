using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace 組別作品
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string s_data = SiteMaster.s_data;
        public string custermer = "123@123";//可變動的
        public int custermerID = 1;//可變動的

        protected void Page_Load(object sender, EventArgs e)
        {
            string template = "";
            SqlConnection connection = new SqlConnection(s_data);
            SqlCommand command = new SqlCommand("select * from products",connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    #region asp的動態生成


                    Panel productBox = new Panel();
                    productBox.CssClass = "product_box";
                    Panel1.Controls.Add(productBox);

                    HyperLink linkBox = new HyperLink();
                    linkBox.NavigateUrl = $"https://localhost:44374/productDetail?productID={reader["id"]}";
                    productBox.Controls.Add(linkBox);

                    Image img = new Image();
                    img.ImageUrl = @"~/images/" +reader["id"]+ ".jpg";
                    linkBox.Controls.Add(img);

                    Label title = new Label();
                    title.Text =reader["title"].ToString();
                    linkBox.Controls.Add(title);

                    Button btn = new Button();
                    btn.ID = ""+reader["id"];
                    btn.Text = "加入購物車";
                    btn.Click += new EventHandler(addToCart);
                    productBox.Controls.Add(btn);
                    
                    
                    
                    #endregion
                }
            }

            connection.Close();
           



        }

     

        protected void addToCart(object sender, EventArgs e)
        {

            //把購物車存到sql
            Button product = (Button)sender;
            SqlConnection connection = new SqlConnection(s_data);
            connection.Open();
            string sql = $"insert into[cart_{custermer}](productID)values(@productID)";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.Parameters.Add("@productID", SqlDbType.Int);
            sqlCommand.Parameters["@productID"].Value= product.ID;
            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }


    }
}