using Avro;
using Devantler.DataMesh.DataProduct.Generator.Generators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.Generators.ModelsGeneratorTests;

[UsesVerify]
public class GenerateTests : GeneratorTestsBase<ModelsGenerator>
{
    [Fact]
    public Task Generate_WithRecordSchemaWithInvalidNamespaceThatIsEmpty_ThrowsCodeGenException()
    {
        return Assert.ThrowsAnyAsync<CodeGenException>(() => Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithInvalidNamespaceThatIsEmpty",
                            "Version": "1"
                        },
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "assets/schemas"
                        }
                    }
                }
                """
            )
        ));
    }

    [Fact]
    public Task Generate_WithRecordSchemaWithInvalidNamespaceThatIsNull_ThrowsCodeGenException()
    {
        return Assert.ThrowsAnyAsync<CodeGenException>(() => Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithInvalidNamespaceThatIsNull",
                            "Version": "1"
                        },
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "assets/schemas"
                        }
                    }
                }
                """
            )
        ));
    }

    // Default values are not yet supported by Apache.Avro.
    [Fact]
    public Task Generate_WithRecordSchemaWithPrimitiveTypesAndDefaultValuesAndNullability_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypesAndDefaultValuesAndNullability",
                            "Version": "1"
                        },
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "assets/schemas"
                        }
                    }
                }
                """
            )
        );
    }

    // Default values are not yet supported by Apache.Avro.
    [Fact]
    public Task Generate_WithRecordSchemaWithPrimitiveTypesAndDefaultValues_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypesAndDefaultValues",
                            "Version": "1"
                        },
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "assets/schemas"
                        }
                    }
                }
                """
            )
        );
    }

    [Fact]
    public Task Generate_WithRecordSchemaWithPrimitiveTypesAndNullability_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypesAndNullability",
                            "Version": "1"
                        },
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "assets/schemas"
                        }
                    }
                }
                """
            )
        );
    }

    [Fact]
    public Task Generate_WithRecordSchemaWithPrimitiveTypes_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypes",
                            "Version": "1"
                        },
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "assets/schemas"
                        }
                    }
                }
                """
            )
        );
    }
}
