using System.Collections.Generic;

namespace Devantler.DataMesh.Core.Models.Contract
{
    public class Model
    {
        public Model(string name)
        {
            Name = name;
        }

        public Model(string name, IEnumerable<Property> properties)
        {
            Name = name;
            Properties = properties;
        }

        public string Name { get; set; }
        public IEnumerable<Property> Properties { get; } = new List<Property>();
    }
}