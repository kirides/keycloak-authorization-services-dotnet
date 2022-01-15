namespace Api.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Commands;
using Api.Application.Queries;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class WorkspacesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Workspace>> Get() =>
        await this.Mediator.Send(new GetWorkspacesQuery());

    [HttpGet("{id}")]
    public async Task<Workspace> GetById(Guid id) =>
        await this.Mediator.Send(new GetWorkspaceByIdQuery(id));

    /// <summary>
    /// Creates workspace with related projects
    /// </summary>
    /// <remarks>
    /// {
    ///     "name": "New Project",
    ///     "projects": [
    ///         {
    ///             "alias": "awesome-project"
    ///         }
    ///     ]
    /// }
    /// </remarks>
    [HttpPost]
    public async Task Create(CreateWorkspaceCommand command) =>
        await this.Mediator.Send(command);

    [HttpDelete("{id}")]
    public async Task Delete(Guid id) =>
        await this.Mediator.Send(new DeleteWorkspaceCommand(id));

    [HttpDelete("")]
    public async Task DeleteAll([FromServices] ApplicationDbContext db) =>
        await db.Database.ExecuteSqlRawAsync("TRUNCATE public.\"Workspaces\" CASCADE");
}