using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DocumentsViewer
{
    public partial class Document : System.Web.UI.Page
    {
        DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            if (dt == null)
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FleetConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("GetOwnVehicleWeb", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                con.Close();
                dt=ds.Tables[0];
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
                ASPxComboBox1.ValueField = "VehicleID";
                ASPxComboBox1.ValueType = typeof(Int32);
                ASPxComboBox1.TextField = "VehicleNo";
                ASPxComboBox1.DataBind();
                LoadingPanel.ContainerElementID = "Panel";

            //}
        }

        protected void DdlVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32( ASPxComboBox1.Value) > 0)
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FleetConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("GetVehicleDocumentsForWeb", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PropertyID", Convert.ToInt32(ASPxComboBox1.Value));
               var a= ASPxComboBox1.Value;
                con.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                GvDocuments.DataSource = dataTable;
                GvDocuments.DataBind();
                con.Close();
            }
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

                frame.Src = "Files\\" + System.IO.Path.GetFileName(pathLiteral.Text);
                //Session["Path"] = "Files\\" + System.IO.Path.GetFileName(pathLiteral.Text);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Pdf.aspx');", true);

                //System.IO.File.Copy(pathLiteral.Text, Server.MapPath("Files\\" + System.IO.Path.GetFileName(pathLiteral.Text)), true);
                //Response.ContentType = "application/octet-stream";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + pathLiteral.Text);
                //Response.TransmitFile("Files\\" + System.IO.Path.GetFileName(pathLiteral.Text));
                //Response.End();
            }
        }
    }
}