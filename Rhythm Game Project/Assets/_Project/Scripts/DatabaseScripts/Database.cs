namespace DatabaseScripts
{
    using UnityEngine;
    using Mono.Data.Sqlite;
    using System.Threading.Tasks;
    using UIScripts;
    using System;
    using System.Data.Common;
    using System.Collections.Generic;
    using System.Threading;

    public sealed class Database : MonoBehaviour
    {
        [SerializeField] private Notification notification = default;
        private string connectionString;

        private void Awake() => connectionString = $"URI=file:{Application.dataPath}/_Project/Database/GameDB.db";
        public void CreateDatabase()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
            }
            connection.Close();
        }
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
        public string[] GetLeaderboardData(string _databaseTable, int _index)
        {
            List<string> leaderboardDataList = new List<string>();
            try
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM '{_databaseTable}' ORDER BY 'score' DESC LIMIT 1 OFFSET '{_index}';";
                    using DbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            leaderboardDataList.Add(reader["username"].ToString());
                            leaderboardDataList.Add(reader["score"].ToString());
                            leaderboardDataList.Add(reader["combo"].ToString());
                            leaderboardDataList.Add(reader["accuracy"].ToString());
                            leaderboardDataList.Add(reader["rank"].ToString());
                        }
                    }
                    reader.Close();
                }
                connection.Close();
            }
            catch (SqliteException e)
            {
                notification.Show(Colors.Red_80, e.ErrorCode.ToString(), e.Message);
            }
            if (leaderboardDataList.Count != 0)
            {
                leaderboardDataList.Add(GetPlayerImage(leaderboardDataList[0]));
            }
            return leaderboardDataList.ToArray();
        }
        public string GetPlayerImage(string _username)
        {
            string playerImage = string.Empty;
            try
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT playerImage FROM player WHERE username = '{_username}';";
                    using DbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            playerImage = reader["playerImage"].ToString();
                        }
                    }
                    reader.Close();
                }
                connection.Close();
            }
            catch (SqliteException e)
            {
                notification.Show(Colors.Red_80, e.ErrorCode.ToString(), e.Message);
            }
            return playerImage;
        }
        public string[] GetPlayerBeatmapData(string _username, string _databaseTable)
        {
            List<string> dataList = new List<string>();
            try
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM '{_databaseTable}' WHERE username = '{_username}';";
                    using DbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            dataList.Add(reader["score"].ToString());
                            dataList.Add(reader["combo"].ToString());
                            dataList.Add(reader["accuracy"].ToString());
                            dataList.Add(reader["rank"].ToString());
                            dataList.Add(reader["perfect"].ToString());
                            dataList.Add(reader["great"].ToString());
                            dataList.Add(reader["okay"].ToString());
                            dataList.Add(reader["miss"].ToString());
                            dataList.Add(reader["feverscore"].ToString());

                        }
                    }
                    reader.Close();
                }
                connection.Close();
            }
            catch (SqliteException e)
            {
                notification.Show(Colors.Red_80, e.ErrorCode.ToString(), e.Message);
            }
            return dataList.ToArray();
        }
        public void CreateBeatmapTable()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS beatmap2
                    (id INTEGER NOT NULL UNIQUE,
                    username TEXT NOT NULL UNIQUE,
                    score INTEGER NOT NULL,
                    combo INTEGER NOT NULL,
                    accuracy REAL NOT NULL,
                    rank TEXT NOT NULL,
                    perfect  INTEGER NOT NULL,
                    great INTEGER NOT NULL,
                    okay INTEGER NOT NULL,
                    miss INTEGER NOT NULL,
	            feverscore INTEGER NOT NULL,
                    PRIMARY KEY(id AUTOINCREMENT));";
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}