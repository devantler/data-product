namespace Devantler.DataMesh.Core.Models.Contract
{
    public class Storage
    {
        public Storage(string type, string connectionString, string userName, string password)
        {
            Type = type;
            ConnectionString = connectionString;
            UserName = userName;
            Password = password;
        }

        public string Type { get; set; }
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}