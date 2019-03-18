using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IControllerCommand<T, TKey>
        where T:class, IEntity<TKey>
    {
        string Name { get; }
        Task<ICommandResult> Add(T model, ClaimsPrincipal user);
        Task<ICommandResult> Read(T model, ClaimsPrincipal user);
        Task<ICommandResult> Update(T model, ClaimsPrincipal user);
        Task<ICommandResult> Delete(T model, ClaimsPrincipal user);


    }
}
