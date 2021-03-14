using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DocumentsViewer
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDriver_Click(object sender, EventArgs e)
        {
            Response.Redirect("DriverDocuments.aspx");
        }

        protected void btnVehicle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Document.aspx");
        }

        protected void DriverImage_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("DriverDocuments.aspx");
        }

        protected void VehicleImage_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Document.aspx");
        }

    }
}