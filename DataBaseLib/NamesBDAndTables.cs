using System.IO;
using System.Text.Json;

namespace DataBaseLib
{
    public class NamesBDAndTables
    {
        private string nameConfig = "Config.cfg";
        public string nameBD {get; set;}
        public string nameTableTask {get; set;}
        public string nameTableStatus {get; set;}
        public string nameTablePriority {get; set;}

        public void SerializeClass()
        {
            var text = JsonSerializer.Serialize(this);
            WriteInConfig(text);
        }

        public void DeserializeClass()
        {
            string text = ReadInConfig();
            var temp = JsonSerializer.Deserialize < NamesBDAndTables > (text);
            if (temp != null)
            {
                nameBD = temp.nameBD;
                nameTableTask = temp.nameTableTask;
                nameTableStatus = temp.nameTableStatus;
                nameTablePriority = temp.nameTablePriority;
            }
        }

        private void WriteInConfig(string text)
        {
            FileStream file = new FileStream(nameConfig, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(text);
            file.Dispose();
        }

        private string ReadInConfig()
        {
            FileStream file = new FileStream(nameConfig, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            var config = reader.ReadToEnd();
            file.Dispose();
            return config;
        }
    }
}