using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DocumentsViewer
{

    public partial class DriverDocuments : System.Web.UI.Page
    {
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            if (dt == null)
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FleetConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("GetDriverDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                con.Close();
                dt = ds.Tables[0];
            }

            //DataRow row = (ds.Tables[0]).NewRow();
            //row["VehicleNo"] = "-Select-";
            //ds.Tables[0].Rows.InsertAt(row, 0);
            //DdlVehicles.DataSource = new DataView(ds.Tables[0]);
            //DdlVehicles.DataTextField = "VehicleNo";
            //DdlVehicles.DataValueField = "VehicleID";
            //DdlVehicles.DataBind();
            //DdlVehicles.SelectedIndexChanged+=DdlVehicles_SelectedIndexChanged;


            //ASPxComboBox1.DataSource = new DataView(ds.Tables[0]);
            //ASPxComboBox1.TextField = "VehicleNo";
            //ASPxComboBox1.ValueField = "VehicleID";
            //ASPxComboBox1.DataBind();

            ASPxComboBox1.DataSource = dt;
            ASPxComboBox1.ValueField = "DocumentId";
            ASPxComboBox1.ValueType = typeof(Int32);
            ASPxComboBox1.TextField = "NAme";
            ASPxComboBox1.DataBind();
            LoadingPanel.ContainerElementID = "Panel";

            //}
        }

        protected void DdlVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ASPxComboBox1.Value) < 1 && String.IsNullOrEmpty(txtDLNo.Text) && String.IsNullOrEmpty(txtSLNo.Text))
            {
                Response.Write("<script>alert('Please select driver name or enter DL number or enter SL number');</script>");
                return;
            }

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FleetConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("GetDriverDocuments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Convert.ToInt32(ASPxComboBox1.Value) > 0)
                {
                    cmd.Parameters.AddWithValue("@DriverId", Convert.ToInt32(ASPxComboBox1.Value));
                }

                if (!String.IsNullOrEmpty(txtDLNo.Text))
                {
                    cmd.Parameters.AddWithValue("@DLNo", txtDLNo.Text);
                }

                if (!String.IsNullOrEmpty(txtSLNo.Text))
                {
                    cmd.Parameters.AddWithValue("@SLNo", txtSLNo.Text);
                }

                con.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                GvDocuments.DataSource = dataTable;
                GvDocuments.DataBind();
                con.Close();
        }

        [DllImport("advapi32.DLL", SetLastError = true)]
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);


        protected void GvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "View")
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("Files\\"));
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                Literal pathLiteral = (Literal)GvDocuments.Rows[index].FindControl("Path");

                IntPtr admin_token = default(IntPtr);
                WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
                WindowsIdentity wid_admin = null;
                WindowsImpersonationContext wic = null;
                try
                {
                    if (LogonUser("mk", "admin-bv", "mk", 9, 0, ref admin_token) != 0)
                    {
                        wid_admin = new WindowsIdentity(admin_token);
                        wic = wid_admin.Impersonate();

                        System.IO.File.Copy(pathLiteral.Text, Server.MapPath("Files\\" + System.IO.Path.GetFileName(pathLiteral.Text)), true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(),
                     "ServerControlScript", "Login Failed", true);
                    }
                }
                catch (System.Exception se)
                {
                    int ret = Marshal.GetLastWin32Error();
                    ScriptManager.RegisterStartupScript(this, GetType(),
                      "ServerControlScript", "Error code: " + ret.ToString(), true);
                    ScriptManager.RegisterStartupScript(this, GetType(),
                      "ServerControlScript", se.Message, true);
                }
                finally
                {
                    if (wic != null)
                    {
                        wic.Undo();
                    }
                }

                Session["Path"] = "Files\\" + System.IO.Path.GetFileName(pathLiteral.Text);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Pdf.aspx');", true);
            }
        }
    }
}