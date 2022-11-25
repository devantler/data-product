using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.Core.Interfaces.Controllers;

public interface IWriteController<T>
{
    Task<ActionResult> Post(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Put(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default);
}
