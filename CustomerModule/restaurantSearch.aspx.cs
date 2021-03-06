﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace CustomerModule
{
    public partial class restaurantSearch : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectStuffConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = " SELECT rp.* from restaurantProfile AS rp JOIN (SELECT restaurantID from menuItems GROUP BY restaurantID) AS mi" +
                " ON rp.restaurantID = mi.restaurantID  WHERE restaurantName= '" + Request.QueryString["rName"] +"'";   // OR '" + Request.QueryString["cName"] + "'
            cmd.CommandType = CommandType.Text;
            DataSet objDS = new DataSet();
            SqlDataAdapter objDA = new SqlDataAdapter();
            objDA.SelectCommand = cmd;
            con.Open();
            objDA.Fill(objDS);
            con.Close();
            RepeaterImages.DataSource = objDS;
            RepeaterImages.DataBind();
        }
    }
}