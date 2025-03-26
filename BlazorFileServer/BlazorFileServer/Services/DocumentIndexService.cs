using BlazorFileServer.Models;
using Microsoft.Data.SqlClient;

namespace BlazorFileServer.Services
{
    public class DocumentIndexService
    {
        private readonly string _connectionString;

        public DocumentIndexService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;                
        }





























        public async Task CreateDocumentIndexAsync(DocumentIndex documentIndex)
        {
            var sql = @"INSERT INTO T_TF_D_FileTransferHistory (
FileName, 
FilePath, 
FileSize, 
CreatedDate, 
ModifiedDate, 
FileType,
IsDeleted,
Checksum,
ThumbnailPath,
OrderId,
ClientName,
Department,
Remarks,
StartDate,
EndDate,
OrderAmount
) VALUES (
@FileName, 
@FilePath, 
@FileSize, 
@CreatedDate, 
@ModifiedDate, 
@FileType,
@IsDeleted,
@Checksum,
@ThumbnailPath,
@OrderId,
@ClientName,
@Department,
@Remarks,
@StartDate,
@EndDate,
@OrderAmount
)";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    command.Parameters.Add(new SqlParameter("@FileName", documentIndex.FileName));
                    command.Parameters.Add(new SqlParameter("@FilePath", documentIndex.FilePath));
                    command.Parameters.Add(new SqlParameter("@FileSize", documentIndex.FileSize));
                    command.Parameters.Add(new SqlParameter("@CreatedDate", (object?)documentIndex.CreatedDate ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@ModifiedDate", (object?)documentIndex.ModifiedDate ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@FileType", documentIndex.FileType));
                    command.Parameters.Add(new SqlParameter("@IsDeleted",documentIndex.IsDeleted));
                    command.Parameters.Add(new SqlParameter("@Checksum", documentIndex.Checksum));
                    command.Parameters.Add(new SqlParameter("@ThumbnailPath", documentIndex.ThumbnailPath));
                    command.Parameters.Add(new SqlParameter("@OrderId", documentIndex.OrderId));
                    command.Parameters.Add(new SqlParameter("@ClientName", documentIndex.ClientName));
                    command.Parameters.Add(new SqlParameter("@Department", documentIndex.Department));
                    command.Parameters.Add(new SqlParameter("@Remarks", documentIndex.Remarks));
                    command.Parameters.Add(new SqlParameter("@StartDate", (object?)documentIndex.StartDate ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@EndDate", (object?)documentIndex.EndDate ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@OrderAmount", documentIndex.OrderAmount));

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQLエラー: " + ex.Message);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("CreateDocumentIndexAsync: " + ex.Message);
                }
            }
        }






    }
}
