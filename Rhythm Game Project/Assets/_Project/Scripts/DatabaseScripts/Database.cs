namespace DatabaseScripts
{
    using UnityEngine;
    using System.Data;
    using Mono.Data.Sqlite;
    using System.Threading.Tasks;
    using UIScripts;
    using StaticDataScripts;
    using System;
    using System.Data.Common;

    public sealed class Database : MonoBehaviour
    {
        [SerializeField] private Notification notification = default;
        private string connectionString;

        private void Awake() => connectionString = $"URI=file:{Application.dataPath}/_Project/Database/GameDB.db";
        public async Task<bool> InsertPlayerAsync(string _username, string _password)
        {
            bool isSuccess = false;
            try
            {
                using var connection = new SqliteConnection(connectionString);
                await connection.OpenAsync();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT 1 FROM player WHERE username = '{_username}';";
                    var result = await cmd.ExecuteScalarAsync();
                    int count = Convert.ToInt32((result == DBNull.Value) ? null : result);
                    if (count == 0)
                    {
                        cmd.CommandText = $"INSERT INTO player (username, password) VALUES ('{_username}', '{_password}');";
                        await cmd.ExecuteNonQueryAsync();
                        notification.Show(Colors.Green_80, "success", "account created");
                        isSuccess = true;
                    }
                    else
                    {
                        notification.Show(Colors.Red_80, "error", "user already exists");
                    }
                }
                connection.Close();
            }
            catch (SqliteException e)
            {
                notification.Show(Colors.Red_80, e.ErrorCode.ToString(), e.Message);
            }
            return isSuccess;
        }
        public async Task<bool> LoginPlayerAsync(string _username, string _password)
        {
            bool isSuccess = false;
            try
            {
                using var connection = new SqliteConnection(connectionString);
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT username, password FROM player WHERE username = '{_username}';";
                    using DbDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            if (reader["username"].ToString() == _username
                                && reader["password"].ToString() == _password)
                            {
                                notification.Show(Colors.Green_80, "success", $"logged in as {_username}");
                                isSuccess = true;
                            }
                            else
                            {
                                notification.Show(Colors.Red_80, "error", "incorrect details entered");
                            }
                        }
                    }
                    else
                    {
                        notification.Show(Colors.Red_80, "error", "incorrect details entered");
                    }
                    reader.Close();
                }
                connection.Close();
            }
            catch (SqliteException e)
            {
                notification.Show(Colors.Red_80, e.ErrorCode.ToString(), e.Message);
            }
            return isSuccess;
        }
    }
}