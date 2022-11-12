using System.Collections.Generic;

namespace Devantler.DataMesh.Core.Models.Contract
{
    public class Contract
    {
        public Contract(string domain, Storage storage, DataSource dataSource)
        {
            Domain = domain;
            Storage = storage;
            DataSource = dataSource;
        }

        public Contract(string domain, IEnumerable<Model> models, Storage storage, DataSource dataSource)
        {
            Domain = domain;
            Models = models;
            Storage = storage;
            DataSource = dataSource;
        }

        public string Domain { get; set; }
        public IEnumerable<Model> Models { get; } = new List<Model>();
        public Storage Storage { get; set; }
        public DataSource DataSource { get; set; }
    }
}