using FileSearchSystem.Models;
using Microsoft.Data.SqlClient;
using Radzen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchSystem.Services
{
    public class FileTransferHistoryService
    {
        private readonly string _connectionString;

        public FileTransferHistoryService(DbConnectionService dbConnectionService)
        {
            _connectionString = dbConnectionService.GetConnectionString();
        }


        
        public async Task<List<FileTransferHistory>> GetFilesByConditionAsync(SearchCondition searchCondition)
        {
            var fileTransferHistorys = new List<FileTransferHistory>();            
            var process = 0;
            var sql = @"SELECT dbo.T_TF_D_FileTransferHistory.* FROM dbo.T_TF_D_FileTransferHistory ";
            
            if (searchCondition.ZyutyuuID != null)
            {
                process = 1;
                sql += "WHERE (ZyutyuuID = @ZyutyuuID)";
            }
            else
            {
                if (searchCondition.ConsignorName == "" && searchCondition.Department == "" && searchCondition.StartDate == "" && searchCondition.EndDate == "")
                {
                    sql += "WHERE (Id = 0)";
                }
                else
                {
                    process = 2;
                    var wherestr = "";
                    if (searchCondition.ConsignorName != "") wherestr += " AND (ConsignorName = @ConsignorName)";
                    if (searchCondition.Department != "") wherestr += " AND (Department = @Department)";
                    if (searchCondition.Remarks != "") wherestr += " AND (Remarks = @Remarks)";
                    if (searchCondition.StartDate != "") wherestr += " AND (EndDate >= @sdate)";
                    if (searchCondition.EndDate != "") wherestr += " AND (StartDate <= @edate)";
                    if (searchCondition.OrderAmountUpper != null) wherestr += " AND (OrderAmount <= @OrderAmountUpper)";
                    if (searchCondition.OrderAmountLower != null) wherestr += " AND (OrderAmount >= @OrderAmountLower)";
                    wherestr = wherestr.Substring(5);
                    sql += "WHERE " + wherestr;
                }  
            }

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(sql, connection);
            try
            {
                if (process == 1)
                {
                    command.Parameters.Add(new SqlParameter("@ZyutyuuID", searchCondition.ZyutyuuID));
                }
                else if (process == 2)
                {
                    if (searchCondition.ConsignorName != "") command.Parameters.Add(new SqlParameter("@ConsignorName", searchCondition.ConsignorName));
                    if (searchCondition.Department != "") command.Parameters.Add(new SqlParameter("@Department", searchCondition.Department));
                    if (searchCondition.Remarks != "") command.Parameters.Add(new SqlParameter("@Remarks", searchCondition.Remarks));
                    if (searchCondition.StartDate != "") command.Parameters.Add(new SqlParameter("@sdate", DateTime.Parse(searchCondition.StartDate)));
                    if (searchCondition.EndDate != "") command.Parameters.Add(new SqlParameter("@edate", DateTime.Parse(searchCondition.EndDate)));
                    if (searchCondition.OrderAmountUpper != null) command.Parameters.Add(new SqlParameter("@OrderAmountUpper", searchCondition.OrderAmountUpper));
                    if (searchCondition.OrderAmountLower != null) command.Parameters.Add(new SqlParameter("@OrderAmountLower", searchCondition.OrderAmountLower));
                }


                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var fileTransferHistory = new FileTransferHistory
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ZyutyuuID = Convert.ToDecimal(reader["ZyutyuuID"]),
                        DefaultFullPath = Convert.ToString(reader["DefaultFullPath"]) ?? "",
                        TemporaryStorageFullPath = Convert.ToString(reader["TemporaryStorageFullPath"]) ?? "",
                        TemporaryStorageTime = reader["TemporaryStorageTime"] != DBNull.Value ? Convert.ToDateTime(reader["TemporaryStorageTime"]) : null,
                        TemporaryStorageLimit = reader["TemporaryStorageLimit"] != DBNull.Value ? Convert.ToDateTime(reader["TemporaryStorageLimit"]) : null,
                        ArchiveFullPath = Convert.ToString(reader["ArchiveFullPath"]) ?? "",
                        ArchiveTime = reader["ArchiveTime"] != DBNull.Value ? Convert.ToDateTime(reader["ArchiveTime"]) : null,
                        OrderAmount = Convert.ToDecimal(reader["OrderAmount"])
                    };
                    fileTransferHistorys.Add(fileTransferHistory);
                }
            }
            catch (Exception ex)
            {

            }
            return fileTransferHistorys;
        }





    }
}
