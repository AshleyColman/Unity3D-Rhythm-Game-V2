namespace DatabaseScripts
{
    using UnityEngine;
    using System.Data;
    using Mono.Data.Sqlite;
    using System.Threading.Tasks;

    public sealed class Database : MonoBehaviour
    {
        private string connectionString;

        private void Awake() => connectionString = $"URI=file:{Application.dataPath}/_Project/Database/GameDB.db";

        public async Task InsertPlayerAsync(string _username, string _password)
        {
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO player (username, password) VALUES ('{_username}', '{_password}');";
                await cmd.ExecuteNonQueryAsync();
            }
            connection.Close();
        }
    }
}