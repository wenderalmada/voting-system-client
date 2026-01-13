using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using VotingSystem.Voting;

class Program
{
    static async Task Main()
    {
        using var channel = CreateChannel();
        var client = new VotingService.VotingServiceClient(channel);

        var candidates = await client.GetCandidatesAsync(new GetCandidatesRequest());

        Console.WriteLine("Candidatos:");
        foreach (var c in candidates.Candidates)
            Console.WriteLine($"{c.Id} - {c.Name}");

        Console.Write("ID do candidato: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Credencial de voto: ");
        string cred = Console.ReadLine() ?? "";

        var vote = await client.VoteAsync(new VoteRequest
        {
            VotingCredential = cred,
            CandidateId = id
        });

        Console.WriteLine(vote.Message);

        var results = await client.GetResultsAsync(new GetResultsRequest());

        Console.WriteLine("\nResultados:");
        foreach (var r in results.Results)
            Console.WriteLine($"{r.Name}: {r.Votes} votos");
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
