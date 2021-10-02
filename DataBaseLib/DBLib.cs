using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks; 

namespace DataBaseLib
{
    public class DBLib
    {
        private string db;
        private string nameTable;
        static SQLiteConnection connection;
        static SQLiteCommand command;

        public DBLib()
        {
            db = "SQLiteDB.sqlite";
            nameTable = "table_task";
        }
        public DBLib(string nameDB)
        {
            db = $"{nameDB}.sqlite";
            nameTable = "table_task";
        }

        public bool Connect()
        {
            try
            {
                connection = new SQLiteConnection("Data Source=" + db + ";Version=3; FailIfMissing=False");
                connection.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public async Task ReqvestAsync(string reqvest)
        {
            command = new SQLiteCommand(connection)
            {
                CommandText = reqvest
            };
            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateTableAsync()
        {
            var reqvest = $"create table {nameTable} (id INTEGER PRIMARY KEY AUTOINCREMENT,name TEXT NOT NULL,description TEXT,creation_date TEXT NOT NULL,execution_date TEXT NOT NULL,status_id INTEGER NOT NULL,priority_id INTEGER NOT NULL,is_deleted INTEGER NOT NULL);";
            await ReqvestAsync(reqvest);
        }

        public async Task AddDataInTableAsync(TableData data)
        {
            
            var reqvest = $"INSERT INTO {nameTable} (name, description, creation_date, execution_date, status_id, priority_id , is_deleted) VALUES ('{data.name}' , '{data.description}' , '{data.creation_date}' , '{data.execution_date}' , '{data.status}' , '{data.priority}' , '{data.is_deleted}')";
            await ReqvestAsync(reqvest);
        }

        public async Task EditDataInTableAsync(TableData data)
        {
            var reqvest = $"UPDATE {nameTable} SET name = ‘{data.name}’, description = '{data.description}' ,  creation_date = '{data.creation_date}', execution_date = '{data.execution_date}, status_id = '{data.status}, priority_id = '{data.priority}', is_deleted = '{data.is_deleted}' WHERE id = {data.id};"; //Todo Написать cтроку для изменения
            await ReqvestAsync(reqvest);
        }
        
        public async Task<List<TableData>> ReadDataInTableAsync()
        {
            
            List<TableData> tableDatas = new List<TableData>();

            var sqlite_select_query = $"SELECT * from {nameTable}";
            SQLiteCommand command = new SQLiteCommand(sqlite_select_query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                TableData record = new TableData();
                record.id = Convert.ToInt32(reader[0]);
                record.name = reader.GetString(1);
                record.description = reader.GetString(2);
                record.creation_date = reader.GetString(3);
                record.execution_date = reader.GetString(4);
                record.status = (Status) Convert.ToInt32(reader[5]);
                record.priority = (Priority) Convert.ToUInt32(reader[6]);
                record.is_deleted = Convert.ToBoolean(reader[7]);
                tableDatas.Add(record);
            }
            reader.Close();
            return tableDatas;
        }
        

        public bool Disconect()
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