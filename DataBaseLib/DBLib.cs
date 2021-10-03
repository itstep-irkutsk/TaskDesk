using System.Data.SQLite;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DataBaseLib
{
    public class DBLib
    {
        private readonly string _connectionString;
        private SQLiteConnection _connection;
        private SQLiteCommand _command;
        private SQLiteDataReader _reader;
        public ObservableCollection<DBTask> _tasks;
        public DBLib()
        {
            _connectionString = @"Data Source=..\..\..\..\TaskDeskDB\TaskDeskDB.db; Version=3;";
            _tasks = new();
        }
        public DBLib(string DBName)
        {
            _connectionString = @"Data Source=..\..\..\..\TaskDeskDB\" + DBName + "; Version=3;";
            _tasks = new();
        }
        public async void LoadFromDB()
        {
            await using (_connection = new SQLiteConnection(_connectionString))
            {
                string sqlExp = "SELECT * FROM table_task";
                _command = new SQLiteCommand(sqlExp, _connection);
                _connection.Open();
                using (_reader = _command.ExecuteReader())
                {
                    if (_reader.HasRows)
                    {                        
                        while (_reader.Read())
                        {
                            if (_reader.GetInt32(7) == 0)
                            {
                                DBTask task = new();
                                task._id = _reader.GetInt32(0);
                                task._name = _reader.GetString(1);
                                task._description = _reader.GetString(2);
                                task._creation_date = _reader.GetString(3);
                                task._execution_date = _reader.GetString(4);
                                task._status = _reader.GetInt32(5); //TODO
                                task._priority = _reader.GetInt32(6); //TODO
                                _tasks.Add(task);
                            }
                        }
                    }
                }
                _connection.Close();
            }
        }
        public async Task<int> AddTask(DBTask newTask)
        {
            int taskId;
            await using (_connection = new SQLiteConnection(_connectionString))
            {
                string SQLExpression = @"INSERT INTO table_task (name, description, creation_date, execution_date, status_id, priority_id, is_deleted)"
                              + @"VALUES ('" + newTask._name + @"', '" + newTask._description + @"', '" + newTask._creation_date + @"', '" + newTask._execution_date + @"', " + newTask._status + @", " + newTask._priority + @", 0);"
                              + @"SELECT last_insert_rowid();"; //TODO status priority
                _command = new SQLiteCommand(SQLExpression, _connection);
                _connection.Open();
                taskId = (int)(long)_command.ExecuteScalar();
                newTask._id = taskId;
                _connection.Close();
            }
            _tasks.Add(newTask);
            return taskId;
        }
        public async void DeleteTask(int taskId)
        {
            await using (_connection = new SQLiteConnection(_connectionString))
            {
                string SQLExpression = @"UPDATE table_task SET is_deleted = 1 WHERE id = " + taskId + @";";
                _command = new SQLiteCommand(SQLExpression, _connection);
                _connection.Open();
                _command.ExecuteNonQuery();
                _connection.Close();
            }
            foreach (DBTask task in _tasks)
            {
                if (task._id == taskId)
                {
                    _tasks.Remove(task);
                    break;
                }
            }
        }
        public async void EditTask(int taskId, DBTask newTask)
        {
            await using (_connection = new SQLiteConnection(_connectionString))
            {
                string SQLExpression = @"UPDATE table_task SET (name, description, creation_date, execution_date, status_id, priority_id, is_deleted) ="
                                     + @"('" + newTask._name + @"', '" + newTask._description + @"', '" + newTask._creation_date + @"', '" + newTask._execution_date + @"', " + newTask._status + @", " + newTask._priority + @", 0) "
                                     + @"WHERE id = " + taskId + @";"; //TODO status priority
                _command = new SQLiteCommand(SQLExpression, _connection);
                _connection.Open();
                _command.ExecuteNonQuery();
                _connection.Close();
            }
            foreach (DBTask task in _tasks)
            {
                if (task._id == taskId)
                {
                    int taskIndex = _tasks.IndexOf(task);
                    _tasks[taskIndex] = newTask;
                    break;
                }
            }
        }
        /* TODO
        public void AddPriority() { }
        public void DeletePriority() { }
        public void EditPriority() { }
        public void AddStatus() { }
        public void DeleteStatus() { }
        public void EditStatus() { }
        */
    }
}