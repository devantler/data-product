using Avro;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.ModelsGeneratorTests;

[UsesVerify]
public class GenerateTests : GeneratorTestsBase<ModelsGenerator>
{
    [Fact]
    public Task Generate_GivenRecordSchemaWithInvalidNamespaceThatIsEmpty_ThrowsCodeGenException()
    {
        return Assert.ThrowsAnyAsync<CodeGenException>(() => Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithInvalidNamespaceThatIsEmpty",
                            "Version": 1
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
    public Task Generate_GivenRecordSchemaWithInvalidNamespaceThatIsNull_ThrowsCodeGenException()
    {
        return Assert.ThrowsAnyAsync<CodeGenException>(() => Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithInvalidNamespaceThatIsNull",
                            "Version": 1
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
    public Task Generate_GivenRecordSchemaWithPrimitiveTypesAndDefaultValuesAndNullability_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypesAndDefaultValuesAndNullability",
                            "Version": 1
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
    public Task Generate_GivenRecordSchemaWithPrimitiveTypesAndDefaultValues_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypesAndDefaultValues",
                            "Version": 1
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
    public Task Generate_GivenRecordSchemaWithPrimitiveTypesAndNullability_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypesAndNullability",
                            "Version": 1
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
    public Task Generate_GivenRecordSchemaWithPrimitiveTypes_GeneratesModel()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "RecordSchemaWithPrimitiveTypes",
                            "Version": 1
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
    public Task Generate_GivenUnionSchema_GeneratesModels()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "UnionSchema",
                            "Version": 1
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
    public Task Generate_GivenUnionSchemaWithReference_GeneratesModels()
    {
        return Verify(
            new CustomAdditionalText("appsettings.json",
                /*lang=json,strict*/
                """
                {
                    "DataProduct": {
                        "Schema": {
                            "Subject": "UnionSchemaWithReference",
                            "Version": 1
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
