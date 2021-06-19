using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 組別作品
{
    public partial class cart : System.Web.UI.Page
    {
        public string s_data = SiteMaster.s_data;
        public string custermer = "123@123";//可變動的
        public int custermerID = 1;//可變動的

        public List<string> cartList = new List<string>();
        public HashSet<string> cartSet = new HashSet<string>();//紀錄有多少種商品

        protected void Page_Load(object sender, EventArgs e)
        {

            pageUpdate(sender,e);

       
         }

        public void pageUpdate(object sender, EventArgs e)
        {
            #region 取得購物車資料
            cartList.Clear();
            cartSet.Clear();
            SqlConnection connection = new SqlConnection(s_data);
            string sql = $"select * from cart_{custermer} order by productID";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    cartList.Add("" + reader["productID"]);
                    cartSet.Add("" + reader["productID"]);
                }
            connection.Close();
            #endregion

            Panel1.Controls.Clear();

            int total = 0;
            //計算每種商品有多少個&計算價格
            foreach (var item in cartSet)//多少種
            {
                int i = 0;
                # region 取得單價
                int singlePrice = 0;
                sql = $"select * from products where id={item}";
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        singlePrice = int.Parse(reader["price"].ToString());
                    }
                }
                connection.Close();

                #endregion
                foreach (var b in cartList)//每種有幾個
                {
                    if (b == item)
                        i++;
                }
                #region html生成

                Panel productBox = new Panel();
                productBox.CssClass = "product_box";
                Panel1.Controls.Add(productBox);

                Label lb_title = new Label();
                #region 從商品ID取得商品名字
                string productName = "";
                sql = $"select * from products where id={item}";
                 sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productName =""+ reader["title"];
                    }
                }
                connection.Close();

                #endregion
                lb_title.Text = $"商品:{item.ToString()}{productName}    數量:";
                productBox.Controls.Add(lb_title);

                Label lb_amount = new Label();
                lb_amount.Text = $"{i}";
                productBox.Controls.Add(lb_amount);

                Button btn_add = new Button();
                btn_add.Text = "+1";
                btn_add.CssClass = $"{item}";//存放productID
                btn_add.Click += new EventHandler(this.add);
                productBox.Controls.Add(btn_add);

                Button btn_minus = new Button();
                btn_minus.Text = "-1";
                btn_minus.CssClass = $"{item}";//存放productID
                btn_minus.Click += new EventHandler(this.minus);
                productBox.Controls.Add(btn_minus);

                Label lb_subtotalTxt = new Label();
                lb_subtotalTxt.Text = "小計:";
                productBox.Controls.Add(lb_subtotalTxt);

                Label lb_subtotal = new Label();//單一種類總價
                lb_subtotal.Text = ""+i*singlePrice;
                productBox.Controls.Add(lb_subtotal);

                total += int.Parse(lb_subtotal.Text);//計算總共的金額

                Button btn_deleteAll = new Button();
                btn_deleteAll.Text = "刪除";
                btn_deleteAll.CssClass = $"{item}";//存放productID
                btn_deleteAll.Click += new EventHandler(this.deleteAll);
                productBox.Controls.Add(btn_deleteAll);
                #endregion
            }
            lb_total.Text =""+ total;
        }

        public void minus(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Panel productBox = (Panel)btn.Parent;//取得包裹它的容器，再轉型成要的html物件
            Label amount = (Label)FindControl(btn.Parent.Controls[1].UniqueID);//取得包裹它的容器，再取得容器的第0個control，再轉型成要的html物件
            Label subtotal = (Label)FindControl(btn.Parent.Controls[5].UniqueID);

            #region 從sql把購物車商品減1
            if (int.Parse(amount.Text) > 1)
            {
                SqlConnection connection = new SqlConnection(s_data);
                string sql = $"delete top(1) from cart_{custermer} where productID={btn.CssClass}";//刪除1個資料
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();

            #endregion

                #region 取得資料&計算商品有多少個
                int count = 0;
                connection = new SqlConnection(s_data);
                sql = $"select * from cart_123@123 where productID={btn.CssClass}";
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
                connection.Close();
                #endregion

                amount.Text = "" + count;


                #region 取得單價
                int price = 0;
                sql = $"select * from products where id={btn.CssClass}";
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        price = int.Parse(reader["price"].ToString());
                    }
                }
                connection.Close();

                #endregion
                subtotal.Text = "" + count * price;
                lb_total.Text = "" + (int.Parse(lb_total.Text) - price);
            }
        }

        public void add(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Panel productBox = (Panel)btn.Parent;//取得包裹它的容器，再轉型成要的html物件
            Label amount = (Label)FindControl(btn.Parent.Controls[1].UniqueID);//取得包裹它的容器，再取得容器的第0個control，再轉型成要的html物件
            Label subtotal = (Label)FindControl(btn.Parent.Controls[5].UniqueID);
            

            #region 從sql把購物車商品+1
            SqlConnection connection = new SqlConnection(s_data);
            string sql = $"insert [cart_{custermer}](productID)values({btn.CssClass}) ";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            #endregion

            #region 取得資料&計算商品有多少個
            int count = 0;
            connection = new SqlConnection(s_data);
            sql = $"select * from cart_{custermer} where productID={btn.CssClass}";
            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count++;
                }
            }
            connection.Close();
            #endregion
            amount.Text = "" + count;

            #region 取得單價
            int price = 0;
            sql = $"select * from products where id={btn.CssClass}";
            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    price = int.Parse(reader["price"].ToString());
                }
            connection.Close();

            #endregion
            subtotal.Text =""+ count * price;
            lb_total.Text = "" + (int.Parse(lb_total.Text) +price);

        }

        public void deleteAll(object sender,EventArgs e)
        {
            Button btn = (Button)sender;
            Panel productBox = (Panel)btn.Parent;//取得包裹它的容器
            Label amount = (Label)FindControl(btn.Parent.Controls[1].UniqueID);//取得包裹它的容器，再取得容器的第0個control，再轉型成要的html物件

            #region 單一商品全部移出購物車
            SqlConnection connection = new SqlConnection(s_data);
            string sql = $"delete from cart_{custermer} where productID={btn.CssClass}";//刪除1個資料
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            #endregion

            productBox.Controls.Clear();//先暫時這樣，因為如果使用productBox.Visible=false會造成ID更動而影響顯示


            #region 取得單價
            int price = 0;
            sql = $"select * from products where id={btn.CssClass}";
            sqlCommand = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    price = int.Parse(reader["price"].ToString());
                }
            connection.Close();

            #endregion

            lb_total.Text = "" +(int.Parse(lb_total.Text)-price*int.Parse(amount.Text));

            

        }

        protected void btn_checkout_Click(object sender, EventArgs e)
        {

            foreach (var item in cartSet)
            {
                int amount = 0;
                foreach (var b in cartList)
                {
                    if (b==item)
                    {
                        amount++;
                    }
                }
                #region 取得price資料
                int price = 0;
                SqlConnection connection = new SqlConnection(s_data);
                string sql = $"select * from products where id={item}";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                           price = int.Parse(reader["price"].ToString());
                    }
                connection.Close();
                #endregion

                #region 從商品ID取得商品名字
                string productName = "";
                sql = $"select * from products where id={item}";
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productName = "" + reader["title"];
                    }
                }
                connection.Close();
                #endregion

                #region 確定下單，資料放入dbo.Oderdtail
                connection = new SqlConnection(s_data);
                sql = $"insert [Orderdetail](productID,amount,total,customersID,customerName)values({int.Parse(item)},{amount},{price * amount},{custermerID},'{custermer}') ";
                sqlCommand = new SqlCommand(sql, connection);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                #endregion

            }


        }
        }
}