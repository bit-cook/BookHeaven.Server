namespace BookHeaven.Server.Features.Api.Abstractions;

public interface IKoreaderSyncEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}