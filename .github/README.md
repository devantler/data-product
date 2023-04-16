# Data Product

[![codecov](https://codecov.io/gh/devantler/data-mesh/branch/main/graph/badge.svg?token=9lh1Z59deC)](https://codecov.io/gh/devantler/data-mesh)

This repo contains a Data Product as defined by Zhamak Dehghani in the Book [Data Mesh](https://www.oreilly.com/library/view/data-mesh/9781492092384/). A data product is a Platform as a Service (PaaS) with dedicated data storage, data processing, data discovery-, and data governance- tooling for a specific domain model. In this context, a domain model is the schema of some data that covers a concrete domain, e.g. Accounts, Books, Authors etc.

The data product is built to support most cloud providers and provisioning tools by being built as a container.

## Prerequisites

- .NET 7.0+

Optionally external services/clusters for:

- Kafka
- Kafka Schema Registry
- LinkedIn DataHub
- PostgreSQL
- MongoDB
