# Data Mesh

This repo contains a Data Mesh platform, that can manage Data Products. A Data Product is package unit (small k8s cluster) that has dedicated data storage, data processing, data discovery-, and data governance- tooling for a specific domain model.

The Data Mesh platform is built to support most cloud providers and provisioning tools. For local development the platform uses [kind](https://kind.sigs.k8s.io/).

## Prerequisites

- .NET 6.0+
- Docker (for local development)
- Go (for local development)
- Kind (for local development)

## Getting started

To run the Data Mesh platform locally ensure that you have the prerequisites installed. Then start either the `DevAntler.DataMesh.Platform.Api` or `DevAntler.DataMesh.Platform` project. The API project will start an API server which can be used to manage the Data Mesh in a headless manner. The platform project will start a web server and the API server, so the Data Mesh can be managed and visualized through a web interface, or through the API.
