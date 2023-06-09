﻿using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Todos.Queries.GetTodos;

public sealed record GetTodosQuery : IRequest<ICollection<Todo>>;

public sealed record
    GetTodosQueryHandler : IRequestHandler<GetTodosQuery,
        ICollection<Todo>>
{
    private readonly ITodoDbContext _gpmDbContext;

    public GetTodosQueryHandler(ITodoDbContext gpmDbContext)
    {
        _gpmDbContext = gpmDbContext;
    }

    public async Task<ICollection<Todo>> Handle(GetTodosQuery request,
        CancellationToken cancellationToken)
    {
        var query = _gpmDbContext.Todo
            .AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }
}