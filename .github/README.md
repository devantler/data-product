# Data Mesh

This repo contains a Data Mesh Provisiong API that can provision data products. A Data Product is an IaaS unit (small k8s cluster) that has dedicated data storage, data processing, data discovery-, and data governance- tooling for a specific domain model. A domain model is a single data model that covers a concrete domain, e.g. Accounts, Books, Authors etc.

The Data Mesh is built to support most cloud providers and provisioning tools. For local development, the platform uses [kind](https://kind.sigs.k8s.io/).

## Prerequisites

- .NET 6.0+
- Docker (for local development)
- Go (for local development)
- Kind (for local development)

## Getting started
