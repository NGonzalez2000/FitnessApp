using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Fitness.ClassLibrary.DBAccess;

public abstract class SQLDataAccess
{
    public static void SaveData<T>(string query, T parameters)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        connection.Execute(query, parameters);
    }
    public static IEnumerable<T> LoadData<T, U>(string query, U parameters)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        return connection.Query<T>(query, parameters);
    }
    public static async Task SaveData_Async<T>(string query, T parameters)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        await connection.ExecuteAsync(query, parameters);
    }
    public static async Task<IEnumerable<T>> LoadData_Async<T, U>(string query, U parameters)
    {
        using IDbConnection connection = new SQLiteConnection(LoadConnectionString());
        return await connection.QueryAsync<T>(query, parameters);
    }
    public static IEnumerable<int> GetLastId()
        => LoadData<int, dynamic>("SELECT  last_insert_rowid()", new { });
    private static string LoadConnectionString(string id = "CnnString")
    {
        
        return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }
}
