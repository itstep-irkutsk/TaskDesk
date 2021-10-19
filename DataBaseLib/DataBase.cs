using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DataBaseLib
{
    public class DataBase
    {
        public NamesBDAndTables namesBdAndTables;
        private SQLiteConnection connection;
        private SQLiteCommand command;

        public DataBase()
        {
            namesBdAndTables = new NamesBDAndTables();
            namesBdAndTables.DeserializeClass();
            connection = new SQLiteConnection("Data Source=" + namesBdAndTables.nameBD + ";Version=3; FailIfMissing=False");
            command = new SQLiteCommand(connection);
        }

        private bool Connect()
        {
            try
            {
                connection.Open();
                command = new SQLiteCommand(connection);
                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public Dictionary<int, string> ReqvestDict(string nameTable)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            if (Connect())
            {
                var sqlite_select_query = $"SELECT * from {nameTable}";
                command.CommandText = sqlite_select_query;
                SQLiteDataReader reader = command.ExecuteReader();
            
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var name = reader.GetString(1);
                    dict.Add(id, name);
                }
                command.Dispose();
                Disconect();
            }
            return dict;
        }

        private async Task ReqvestAsync(string reqvest)
        {
            if (Connect())
            {
                command.CommandText = reqvest;
                await command.ExecuteNonQueryAsync();
                Disconect(); 
            }
        }

        private TaskData SearchByData(TaskData data)
        {
            TaskData record = null;
            if (Connect())
            {
                var sqlite_select_query = $"SELECT * from {namesBdAndTables.nameTableTask} WHERE name = '{data.Name}' and description = '{data.Description}' and  creation_date = '{data.CreationDate}' and execution_date = '{data.ExecutionDate}' and status_id = '{data.Status}' and priority_id = '{data.Priority}' and is_deleted = '{data.IsDeleted}'";
                command.CommandText = sqlite_select_query;
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                
                    record = new TaskData();
                    record.Id = Convert.ToInt32(reader[0]);
                    record.Name = reader.GetString(1);
                    record.Description = reader.GetString(2);
                    record.CreationDate = reader.GetString(3);
                    record.ExecutionDate = reader.GetString(4);
                    record.Status = Convert.ToInt32(reader[5]);
                    record.Priority = Convert.ToInt32(reader[6]);
                    record.IsDeleted = Convert.ToBoolean(reader[7]);
                
                reader.Dispose();
                command.Dispose();
                Disconect();
            }

            return record;
        }

        public async Task<TaskData> AddDataInTableAsync(TaskData data)
        {
            
            var reqvest = $"INSERT INTO {namesBdAndTables.nameTableTask} (name, description, creation_date, execution_date, status_id, priority_id , is_deleted) VALUES ('{data.Name}' , '{data.Description}' , '{data.CreationDate}' , '{data.ExecutionDate}' , '{data.Status}' , '{data.Priority}' , '{data.IsDeleted}')";
            await ReqvestAsync(reqvest);
            var temp = SearchByData(data);
            return temp;
        }

        public async Task EditDataInTableAsync(TaskData data)
        {
            var reqvest = $"UPDATE {namesBdAndTables.nameTableTask} SET name = '{data.Name}', description = '{data.Description}' ,  creation_date = '{data.CreationDate}', execution_date = '{data.ExecutionDate}', status_id = '{data.Status}', priority_id = '{data.Priority}', is_deleted = '{data.IsDeleted}' WHERE id = '{data.Id}'";
            await ReqvestAsync(reqvest);
        }
        
        public async Task<List<TaskData>> ReadDataInTableAsync()
        {
            
            List<TaskData> tableDatas = new List<TaskData>();

            if (Connect())
            {
                var sqlite_select_query = $"SELECT * from {namesBdAndTables.nameTableTask}";
                command.CommandText = sqlite_select_query;
                SQLiteDataReader reader = command.ExecuteReader();
            
                while (reader.Read())
                {
                    TaskData record = new TaskData();
                    record.Id = Convert.ToInt32(reader[0]);
                    record.Name = reader.GetString(1);
                    record.Description = reader.GetString(2);
                    record.CreationDate = reader.GetString(3);
                    record.ExecutionDate = reader.GetString(4);
                    record.Status = Convert.ToInt32(reader[5]);
                    record.Priority = Convert.ToInt32(reader[6]);
                    record.IsDeleted = Convert.ToBoolean(reader[7]);
                    tableDatas.Add(record);
                }
                reader.Close();
                command.Dispose();
                Disconect();
            }
            return tableDatas;
        }
        
        public async Task DeleteDataInTableAsync(TaskData data)
        {
            var reqvest = $"UPDATE {namesBdAndTables.nameTableTask} SET is_deleted = True WHERE id = '{data.Id}'";
            await ReqvestAsync(reqvest);
        }

        private bool Disconect()
        {
            try
            {
                connection.Close();
            return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }
    }
}