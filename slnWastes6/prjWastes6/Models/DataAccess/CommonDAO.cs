using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjWastes6.Models.DataAccess
{
    public class CommonDAO
    {
        private readonly string _ESG;
        private readonly string _TWNCPC;

        public CommonDAO()
        {
            _ESG = ConfigurationManager.ConnectionStrings["ESG"].ConnectionString;
            _TWNCPC = ConfigurationManager.ConnectionStrings["TWNCPCContext"].ConnectionString;
        }



    }
}