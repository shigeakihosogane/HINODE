using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Index = FileTransfer2ForBlazor.Models.Index;

namespace FileTransfer2ForBlazor.Services
{
    public class IndexService
    {
        private readonly String _connectionString;

        public IndexService(DBConnection connection)
        {
            _connectionString = connection.GetConnectionString();
        }

        public async Task<Index> GetIndexAsync(Decimal zID)
        {            
            var index = new Index();
            var sql = @"SELECT                      dbo.T_TF_D_Index.ID, dbo.T_TF_D_Index.受注ID, dbo.T_TF_D_Index.開始日, dbo.T_TF_D_Index.終了日, dbo.T_TF_D_Index.担当部署, dbo.T_TF_D_Index.備考, 
                                      dbo.T_TF_D_Index.荷主ID, dbo.T_TF_D_Index.荷主名, dbo.T_TF_M_NinusiInfo.名称_HND, dbo.T_TF_M_NinusiInfo.名称_TF, dbo.T_TF_M_NinusiInfo.FAX番号, 
                                      dbo.T_UN_M_荷主設定.SE_請求締日
FROM                         dbo.T_TF_D_Index LEFT OUTER JOIN
                                      dbo.T_UN_M_荷主設定 ON dbo.T_TF_D_Index.荷主ID = dbo.T_UN_M_荷主設定.荷主ID LEFT OUTER JOIN
                                      dbo.T_TF_M_NinusiInfo ON dbo.T_TF_D_Index.荷主ID = dbo.T_TF_M_NinusiInfo.荷主ID
WHERE                       (dbo.T_TF_D_Index.受注ID = @ZID)";
            
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@ZID", zID));
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            try
                            {
                                while (await reader.ReadAsync())
                                {
                                    index.ID = Convert.ToInt32(reader["ID"]);
                                    index.受注ID = Convert.ToDecimal(reader["受注ID"]);
                                    if (!(reader["開始日"] is DBNull))
                                    {
                                        var dt = Convert.ToDateTime(reader["開始日"]);
                                        index.開始日 = dt.ToString("yyyyMMdd");
                                    }
                                    if (!(reader["終了日"] is DBNull))
                                    {
                                        var dt = Convert.ToDateTime(reader["終了日"]);
                                        index.終了日 = dt.ToString("yyyyMMdd");
                                    }
                                    index.担当部署 = Convert.ToString(reader["担当部署"]) ?? "";
                                    index.備考 = Convert.ToString(reader["備考"]) ?? "";
                                    index.荷主ID = Convert.ToInt32(reader["荷主ID"]);
                                    index.荷主名 = Convert.ToString(reader["荷主名"]) ??"";
                                    index.名称_HND = Convert.ToString(reader["名称_HND"]) ?? "";
                                    index.名称_TF = Convert.ToString(reader["名称_TF"]) ??"";
                                    index.FAX番号 = Convert.ToString(reader["FAX番号"]) ?? "";
                                    index.請求締日 = Convert.ToInt32(reader["SE_請求締日"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"エラーの詳細: {ex.Message}");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("GetIndexAsync: " + ex.Message);
                }
            }
            return index;
        }
    }
}
