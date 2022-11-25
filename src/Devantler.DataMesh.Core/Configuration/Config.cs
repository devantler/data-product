using System.Collections.Generic;

namespace Devantler.DataMesh.Core.Configuration;

public class Config
{
    public Domain Domain { get; set; }
    public Owner Owner { get; set; }
    public List<Model> Models { get; set; }
    public Features Features { get; set; }
}
