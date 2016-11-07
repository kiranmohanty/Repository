using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CompanyWeb.Models.Box8;

namespace CompanyWeb.Controllers
{
    public class DataBaseFactory
    {
        private static DataBaseType DataBase;

        static  DataBaseFactory()
        {
            string configuredType = ConfigurationManager.AppSettings["DataBaseType"].ToTrimedString();
           if(! Enum.TryParse<DataBaseType>(configuredType, out DataBase))            
            {
                DataBase = DataBaseType.MSSQL;
            }
        }
        public static DataSet ExecuteCommand(string command)
        {
            if (DataBase == DataBaseType.MSSQL)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Box8Context"].ConnectionString))
                {
                    DataSet dsLocations = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(command,connection);
                    adapter.Fill(dsLocations);

                    return dsLocations;
                }
            }
            return null;
        }
    }

    public enum DataBaseType
    {
        MSSQL,
        Oracle,
        Mongo
    }
}