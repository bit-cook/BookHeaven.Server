namespace BookHeaven.Server.Features.Api.Abstractions;

public interface IOpdsEndpoint
{
    void MapOpdsEndpoint(IEndpointRouteBuilder endpoint);
}