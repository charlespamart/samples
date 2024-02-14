﻿using Application.Handlers.Todos.Commands.CreateTodo;
using Domain.Models;
using FastEndpoints;
using Mapster;
using MediatR;
using Presentation.FastEndpoints.V2.Models.Todos;

namespace Presentation.FastEndpoints.V2.Endpoints.Todos;

public sealed class CreateTodo(ISender mediator)
    : Endpoint<CreateTodoRequest, Todo>
{
    public override void Configure()
    {
        Version(2);
        Post("api/todos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(request.Adapt<CreateTodoCommand>(), cancellationToken);
        await SendCreatedAtAsync<GetTodo>(todo.Id, todo, cancellation: cancellationToken);
    }
}