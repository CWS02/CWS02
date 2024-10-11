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
    public class keyenecDAO
    {
        private readonly string _connectionString;
        public keyenecDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ESG"].ConnectionString;
        }

        //空壓機數值
        public IEnumerable<dynamic> keyenecData(DateTime? startDate)
        {
            var sql = @"
        SELECT 
            KEY01, KEY02, KEY03, 
            MAX(KEY11) AS MAX空壓1, MIN(KEY11) AS MIN空壓1, SUM(KEY11) AS SUM空壓1,
            MAX(KEY12) AS MAX乾燥1, MIN(KEY12) AS MIN乾燥1, SUM(KEY12) AS SUM乾燥1,
            MAX(KEY13) AS MAX空壓2, MIN(KEY13) AS MIN空壓2, SUM(KEY13) AS SUM空壓2,
            MAX(KEY14) AS MAX乾燥2, MIN(KEY14) AS MIN乾燥2, SUM(KEY14) AS SUM乾燥2,
            MAX(KEY15) AS MAX空壓3, MIN(KEY15) AS MIN空壓3, SUM(KEY15) AS SUM空壓3,
            MAX(KEY16) AS MAX乾燥3, MIN(KEY16) AS MIN乾燥3, SUM(KEY16) AS SUM乾燥3,
            MAX(KEY17) AS MAX水塔, MIN(KEY17) AS MIN水塔, SUM(KEY17) AS SUM水塔,
            MAX(KEY18) - MIN(KEY18) AS 空壓1用電,
            MAX(KEY19) - MIN(KEY19) AS 乾燥1用電,
            MAX(KEY20) - MIN(KEY20) AS 空壓2用電,
            MAX(KEY21) - MIN(KEY21) AS 乾燥2用電,
            MAX(KEY22) - MIN(KEY22) AS 空壓3用電,
            MAX(KEY23) - MIN(KEY23) AS 乾燥3用電,
            MAX(KEY24) - MIN(KEY24) AS 水塔用電,
            (MAX(KEY18) - MIN(KEY18) + MAX(KEY19) - MIN(KEY19) + MAX(KEY20) - MIN(KEY20) +
             MAX(KEY21) - MIN(KEY21) + MAX(KEY22) - MIN(KEY22) + MAX(KEY23) - MIN(KEY23) + 
             MAX(KEY24) - MIN(KEY24)) AS 總用電,
            ((MAX(KEY18) - MIN(KEY18) + MAX(KEY19) - MIN(KEY19) + MAX(KEY20) - MIN(KEY20) + 
              MAX(KEY21) - MIN(KEY21) + MAX(KEY22) - MIN(KEY22) + MAX(KEY23) - MIN(KEY23) + 
              MAX(KEY24) - MIN(KEY24)) / mSUM) * 60 AS mCMM 
            ,mMAX,mMIN,mAVG,mSUM
        FROM keyenec2 
        LEFT JOIN (
            SELECT year, month, day, 
                   MAX(total_amount) AS mMAX,
                   MIN(total_amount) AS mMIN,
                   AVG(total_amount) AS mAVG,
                   SUM(total_amount) AS mSUM 
            FROM keyenec 
            GROUP BY year, month, day
        ) AS kk1 ON kk1.year = KEY01 AND kk1.month = KEY02 AND kk1.day = KEY03";

            var parameters = new DynamicParameters();

            if (startDate.HasValue)
            {
                sql += " WHERE KEY01 = @Year AND KEY02 = @Month";
                parameters.Add("Year", startDate.Value.Year.ToString());
                parameters.Add("Month", startDate.Value.Month.ToString("D2"));
            }

            sql += " GROUP BY KEY01, KEY02, KEY03, mMAX, mMIN, mAVG, mSUM ORDER BY KEY01, KEY02, KEY03";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (startDate.HasValue)
                {
                    return db.Query(sql, parameters).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}