using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 組別作品
{
    public partial class SiteMaster : MasterPage
    {
        public static string s_data = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=測試用資料庫01;Integrated Security=True";//可變動的
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}