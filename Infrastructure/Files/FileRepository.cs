using System.Linq.Expressions;
using System.Reflection;
using Domain.Files;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

namespace Infrastructure.Files;

public class FileRepository : BaseRepository<FileEntity>,IRepository<FileEntity>, IFileRepository
{
    public FileRepository(DBContext context) : base(context)
    {
    }

    public async Task<bool> IsRecordReferencedAsync(Guid id)
    {
        IEntityType entityType = this.Context.Model.FindEntityType(typeof(FileEntity)) 
                                 ?? throw new Exception("FileEntity not found");
        IEnumerable<IForeignKey> foreignKeys = entityType.GetReferencingForeignKeys();
        foreach (IForeignKey foreignKey in foreignKeys)
        {
            string tableName = foreignKey.DeclaringEntityType.GetTableName() ?? 
                               foreignKey.DeclaringEntityType.ShortName();
            string columnName = foreignKey.Properties.First().GetColumnName() ?? 
                                foreignKey.Properties.First().Name;
        
            string sql = $"SELECT COUNT(*) FROM \"{tableName}\" WHERE \"{columnName}\" = @id";
            var param = new NpgsqlParameter("id", id);

            await using var command = this.Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add(param);

            await this.Context.Database.OpenConnectionAsync();
            var result = await command.ExecuteScalarAsync();

            int count = Convert.ToInt32(result);
        
            if (count > 0)
            {
                return true;
            }
        }
        return false;
    }
}