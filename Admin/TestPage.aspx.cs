using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class TestPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // You can add initialization logic here if needed
    }

    protected void btnFetchData_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["chennaiexports"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT ShopName FROM tbl_Shop";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dt);
                gvShopNames.DataSource = dt;
                gvShopNames.DataBind();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                Response.Write("Exception in btnFetchData_Click: " + ex.Message);
            }
        }
    }
}
