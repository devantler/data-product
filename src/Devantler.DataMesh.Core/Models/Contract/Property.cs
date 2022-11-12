namespace Devantler.DataMesh.Core.Models.Contract
{
    public class Property
    {
        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public string Type { get; set; }
    }
}