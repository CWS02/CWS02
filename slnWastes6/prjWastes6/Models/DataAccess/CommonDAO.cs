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

        public bool CheckPassword(List<string> codes, string password)
        {
            using (var connection = new SqlConnection(_TWNCPC))
            {
                var query = @"SELECT MV004, MV001, RIGHT(MV009, 5) as pwd
                              FROM TWNCPC..CMSMV 
                              WHERE MV004 IN @codes 
                                AND LEN(MV022) = 0
                                AND RIGHT(MV009, 5)=@password";

                var result = connection.QueryFirstOrDefault(query, new {codes,password});
                return result != null;
            }
        }

    }
}