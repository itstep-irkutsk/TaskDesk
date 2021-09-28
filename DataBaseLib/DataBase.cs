using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DataBaseLib
{
    public class DataBase
    {
        private string db;
        static SQLiteConnection connection;
        static SQLiteCommand command;

        public DataBase()
        {
            db = "SQLiteDB.sqlite";
        }
        public DataBase(string nameDB)
        {
            db = $"{nameDB}.sqlite";
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

        public async Task CreateTableAsinc(string nameTable, string column)
        {
            var reqvest = $"CREATE TABLE IF NOT EXISTS [{nameTable}]({column});";
            await ReqvestAsync(reqvest);
        }
        
        public async Task CreateTableAsinc(string nameTable, TableData data)
        {
            var reqvest = $"create table {nameTable} (id INTEGER PRIMARY KEY AUTOINCREMENT,name TEXT NOT NULL,description TEXT,creation_date TEXT NOT NULL,execution_date TEXT NOT NULL,status_id INTEGER NOT NULL,priority_id INTEGER NOT NULL,is_deleted INTEGER NOT NULL);";
            await ReqvestAsync(reqvest);
        }
        
        public async Task AddDataInTableAsinc(string nameTable, string column, string data)
        {
            var reqvest = $"INSERT INTO {nameTable} ({column}) VALUES ({data})";
            await ReqvestAsync(reqvest);
        }
        
        public async Task AddDataInTableAsinc(string nameTable, TableData data)
        {
            
            var reqvest = $"INSERT INTO {nameTable} (name, description, creation_date, execution_date, status_id, priority_id,) VALUES ('{data.name}' , '{data.description}' , '{data.creation_date}' , '{data.execution_date}' , '{data.status})' , '{data.priority}'";
            await ReqvestAsync(reqvest);
        }
        
        public async Task EditDataInTableAsinc(string nameTable, string column, string data)
        {
            var reqvest = $""; //Todo Написать cтроку для изменения
            await ReqvestAsync(reqvest);
        }
        
        public async Task EditDataInTableAsinc(string nameTable, TableData data)
        {
            var reqvest = $""; //Todo Написать cтроку для изменения
            await ReqvestAsync(reqvest);
        }
        
        public async Task<List<TableData>> ReadDataInTableAsinc(string nameTable)
        {
            TableData data = null;
            List<TableData> tableDatas = null;
                
            var sqlite_select_query = $"SELECT * from {nameTable}";
            SQLiteCommand command = new SQLiteCommand(sqlite_select_query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                data.id = (int) reader[0];
                data.name = (string) reader[1];
                data.description = (string) reader[2];
                data.creation_date = (string) reader[3];
                data.execution_date = (string) reader[4];
                data.status = (Status) reader[5];
                data.priority = (Priority) reader[6];
                tableDatas.Add(data);
            }
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