using FileBrowser.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Services
{
    public class FileRegistryService
    {
        private readonly string _connectionString;

        public FileRegistryService(DbConnectionService dbConnectionService)
        {
            _connectionString = dbConnectionService.GetConnectionString();
        }




        public async Task<List<DisplayContent>> GetDisplayContentAsync(SearchCondition searchCondition)
        {            
            var networkPath = @"\\192.168.2.240\受注FAX保管";
            List<DisplayContent> displayContents = new List<DisplayContent>();
            var process = 0;
            var sql = @"SELECT dbo.T_TF_D_FileRegistry.* FROM dbo.T_TF_D_FileRegistry ";

            if (searchCondition.ZyutyuuID != null)
            {
                process = 1;
                sql += "WHERE (ZyutyuuID = @ZyutyuuID)";
            }
            else
            {
                if (searchCondition.ConsignorName == "" && searchCondition.Department == "" && searchCondition.StartDate == null && searchCondition.EndDate == null)
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
                    if (searchCondition.StartDate != null) wherestr += " AND (EndDate >= @sdate)";
                    if (searchCondition.EndDate != null) wherestr += " AND (StartDate <= @edate)";
                    if (searchCondition.OrderAmountUpper != null) wherestr += " AND (OrderAmount <= @OrderAmountUpper)";
                    if (searchCondition.OrderAmountLower != null) wherestr += " AND (OrderAmount >= @OrderAmountLower)";
                    wherestr = wherestr.Substring(5);
                    sql += "WHERE " + wherestr;
                }
            };

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
                    if (searchCondition.StartDate != null) command.Parameters.Add(new SqlParameter("@sdate", searchCondition.StartDate));
                    if (searchCondition.EndDate != null) command.Parameters.Add(new SqlParameter("@edate", searchCondition.EndDate));
                    if (searchCondition.OrderAmountUpper != null) command.Parameters.Add(new SqlParameter("@OrderAmountUpper", searchCondition.OrderAmountUpper));
                    if (searchCondition.OrderAmountLower != null) command.Parameters.Add(new SqlParameter("@OrderAmountLower", searchCondition.OrderAmountLower));
                }

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var displayContent = new DisplayContent();
                    var path = Convert.ToString(reader["FileFullPath"]) ?? "";
                    var rootPath = Convert.ToString(reader["RootDirectory"]) ?? "";
                    var relativePath = Convert.ToString(reader["DirectoryPath"]) ?? "";
                    var fileName = Convert.ToString(reader["FileName"]) ?? "";
                    displayContent.Name = fileName;
                    displayContent.IsFolder = false;
                    displayContent.FullPath = path;                    
                    displayContent.NetworkPath = networkPath + @"\" + relativePath + @"\" + fileName;
                    displayContents.Add(displayContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return displayContents;
        }
















    }
}
