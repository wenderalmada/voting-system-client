# gRPC Voting System Clients

Este repositório contém duas aplicações cliente gRPC desenvolvidas no âmbito da UC de Integração de Sistemas, para testar os serviços da Autoridade de Registo e da Autoridade de Votação do sistema de votação eletrónica.

## Estrutura do Repositório

voting-system-client/
├─ RegistrationClient/
│ ├─ Protos/
│ │ └─ voter.proto
│ ├─ Program.cs
│ └─ RegistrationClient.csproj
│
├─ VotingClient/
│ ├─ Protos/
│ │ └─ voting.proto
│ ├─ Program.cs
│ └─ VotingClient.csproj

## Requisitos

- .NET 8 SDK
- Visual Studio (recomendado)

## Compilação

```bash
dotnet restore
dotnet build

Execução:

RegistrationClient
cd RegistrationClient
dotnet run --project RegistrationClient/RegistrationClient.csproj^C

VotingClient
cd VotingClient
 dotnet run --project VotingClient/VotingClient.csproj
