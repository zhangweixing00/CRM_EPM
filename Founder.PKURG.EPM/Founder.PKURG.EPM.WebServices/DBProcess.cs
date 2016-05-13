using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace Founder.PKURG.EPM.WebServices
{
    public class DBProcess
    {
        internal static DataTable GetDataTable(string requestName, object[] parms)
        {
            Founder.PKURG.DBUntility.DbHelperSQL dbHelper = new DBUntility.DbHelperSQL();
            dbHelper.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            StringBuilder sqlTxt = new StringBuilder();
            sqlTxt.AppendFormat("exec {0}", requestName);
            if (parms != null)
            {
                foreach (var item in parms)
                {
                    sqlTxt.AppendFormat("'{0}',", item);
                }
            }

            DataTable dt = dbHelper.ExecuteDataTable(sqlTxt.ToString().TrimEnd(','));
            return dt;
        }

        internal static DataTable GetDataSet(string requestName, Dictionary<string, string> parms)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            foreach (KeyValuePair<string, string> item in parms)
            {
                paramList.Add(new SqlParameter()
                {
                    ParameterName = "@" + item.Key,
                    Value = item.Value,
                    SqlDbType = SqlDbType.NVarChar
                });
            }
            Founder.PKURG.DBUntility.DbHelperSQL dbHelper = new DBUntility.DbHelperSQL();
            dbHelper.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            DataTable dt = dbHelper.ExecuteDataTable(requestName, CommandType.StoredProcedure, paramList.ToArray());
            return dt;
        }

    }
}