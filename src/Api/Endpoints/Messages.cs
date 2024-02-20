using CoduTeam.Application.Messages.Commands.CreateMessageCommand;
using CoduTeam.Application.Messages.Commands.DeleteMessageCommand;
using CoduTeam.Application.Messages.Models;
using CoduTeam.Application.Messages.Queries;

namespace CoduTeam.Web.Endpoints;

public class Messages : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateMessageEndpoint)
            .MapDelete(DeleteMessageEndpoint, "{Id}")
            .MapGet(GetMessageEndpoint, "{Id}")
            .MapGet(GetAllMessageEndpoint)
            .MapPut(UpdateMessageEndpoint);
    }

    public async Task CreateMessageEndpoint(ISender sender, CreateMessageCommand command)
    {
        await sender.Send(command);
    }

    public async Task DeleteMessageEndpoint(ISender sender, int Id)
    {
        await sender.Send(new DeleteMessageCommand(Id));
    }

    public async Task UpdateMessageEndpoint(ISender sender, CreateMessageCommand command)
    {
        await sender.Send(command);
    }

    public async Task<MessageDto> GetMessageEndpoint(ISender sender, int Id)
    {
        MessageQuery query = new MessageQuery { MessageId = Id };
        MessageDto messages = await sender.Send(query);
        return messages;
    }

    public async Task<ICollection<MessageDto>> GetAllMessageEndpoint(ISender sender,
        [AsParameters] AllMessageQuery query)
    {
        return await sender.Send(query);
    }
}
