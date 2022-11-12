namespace Devantler.DataMesh.Core.Models.Contract
{
    public class DataSource
    {
        public DataSource(string type, string database, string connectionString, string userName, string password)
        {
            Type = type;
            Database = database;
            ConnectionString = connectionString;
            UserName = userName;
            Password = password;
        }

        public string Type { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}