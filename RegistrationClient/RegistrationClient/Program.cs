using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using VotingSystem.Registration;

class Program
{
    static async Task Main()
    {
        using var channel = CreateChannel();

        var client =
            new VoterRegistrationService.VoterRegistrationServiceClient(channel);

        Console.Write("Número do Cartão de Cidadão: ");
        string cc = Console.ReadLine() ?? "";

        var response = await client.IssueVotingCredentialAsync(
            new VoterRequest { CitizenCardNumber = cc });

        if (response.IsEligible)
            Console.WriteLine($"Credencial emitida: {response.VotingCredential}");
        else
            Console.WriteLine("Eleitor não elegível.");
    }

    static GrpcChannel CreateChannel()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        return GrpcChannel.ForAddress(
            "https://ken01.utad.pt:9091",
            new GrpcChannelOptions { HttpHandler = handler });
    }
}
