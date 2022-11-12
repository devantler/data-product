using System.Collections.Generic;
using Devantler.DataMesh.Core.Models.Contract;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.SourceGenerators
{
    [Generator]
    public class ModelGenerator : ISourceGenerator
    {
        private readonly List<Model> _models;

        public void Initialize(GeneratorInitializationContext context)
        {
            //TODO: Parse appsettings.Development.json into Contract
            
            //TODO: Assign the result to _contract
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // while (!Debugger.IsAttached)
            //     System.Threading.Thread.Sleep(500);
            
            //TODO: Analyze and save usings

            //ForEach Model

            //TODO: Read in Model template

            //TODO: Replace {{className}} with Type

            //TODO: Replace {{properties}} with Properties
        }

    }
}