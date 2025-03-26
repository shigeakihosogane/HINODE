using FileSearchSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchSystem.Services
{
    public class FileRegistryService
    {
        private readonly string _connectionString;


        public FileRegistryService(DbConnectionService dbConnectionService)
        {
            _connectionString = dbConnectionService.GetConnectionString();
        }



        public async Task<List<FileRegistry>> GetFileRegistryByConditionAsync(SearchCondition searchCondition)
        {
            var fileRegistrys = new List<FileRegistry>();
            var process = 0;
            var sql = @"SELECT dbo.T_TF_D_FileRegistry.* FROM dbo.T_TF_D_FileRegistry ";

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
                    var fileRegistry = new FileRegistry
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FileFullPath = Convert.ToString(reader["FileFullPath"]) ?? "",
                        Thumbnail = reader["Thumbnail"] != DBNull.Value ? reader["Thumbnail"] as Byte[] : null,
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        ZyutyuuID = Convert.ToDecimal(reader["ZyutyuuID"]),
                        ConsignorName = Convert.ToString(reader["ConsignorName"]) ?? "",
                        Department = Convert.ToString(reader["Department"]) ?? "",
                        Remarks = Convert.ToString(reader["Remarks"]) ?? "",
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : null,
                        OrderAmount = Convert.ToDecimal(reader["OrderAmount"]),
                        IsExecutionCompleted = Convert.ToBoolean(reader["IsExecutionCompleted"]),
                        RootDirectory = Convert.ToString(reader["RootDirectory"]) ?? "",
                        DirectoryPath = Convert.ToString(reader["DirectoryPath"]) ?? "",
                        FileName = Convert.ToString(reader["FileName"]) ?? ""
                    };
                    fileRegistrys.Add(fileRegistry);
                }
            }
            catch (Exception ex)
            {

            }
            return fileRegistrys;
        }










    }
}
