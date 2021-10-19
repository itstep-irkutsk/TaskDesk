using System.Collections.Generic;

namespace DataBaseLib
{
    public class TaskData
    {
        static public Dictionary<int, string>statusDic;
        static public Dictionary<int, string>priorityDic;
        
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public string CreationDate {get; set;}
        public string ExecutionDate {get; set;}
        public int Status {get; set;}
        public int Priority {get; set;}
        public bool IsDeleted {get; set;}

        public TaskData() 
        {
            var taskData = new DataBase();
            statusDic = taskData.ReqvestDict(taskData.namesBdAndTables.nameTableStatus);
            priorityDic = taskData.ReqvestDict(taskData.namesBdAndTables.nameTablePriority);
        }
    }
}