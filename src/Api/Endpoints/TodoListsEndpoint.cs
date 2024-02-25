using CoduTeam.Api.Infrastructure;
using CoduTeam.Application.TodoLists.Commands.CreateTodoList;
using CoduTeam.Application.TodoLists.Commands.DeleteTodoList;
using CoduTeam.Application.TodoLists.Commands.UpdateTodoList;
using CoduTeam.Application.TodoLists.Queries.GetTodos;

namespace CoduTeam.Api.Endpoints;

public class TodoListsEndpoint : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTodoLists)
            .MapPost(CreateTodoList)
            .MapPut(UpdateTodoList, "{id}")
            .MapDelete(DeleteTodoList, "{id}");
    }

    public async Task<TodosVm> GetTodoLists(ISender sender)
    {
        return await sender.Send(new GetTodosQuery());
    }

    public async Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateTodoList(ISender sender, int id, UpdateTodoListCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteTodoList(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoListCommand(id));
        return Results.NoContent();
    }
}
