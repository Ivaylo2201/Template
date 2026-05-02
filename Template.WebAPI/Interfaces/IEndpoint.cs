namespace Template.WebAPI.Interfaces;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder builder);
}