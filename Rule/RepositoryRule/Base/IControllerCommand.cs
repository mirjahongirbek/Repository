using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using RepositoryRule.Entity;

namespace RepositoryRule.Base
{
    public interface IControllerCommand<TKey>
      
    {
        string Name { get; }
        Task<bool> GetById(TKey id, ClaimsPrincipal user);
        Task<ICommandResult> Add<T>(T model, ClaimsPrincipal user);
        Task<ICommandResult> Read<T>(T model, ClaimsPrincipal user);
        Task<ICommandResult> Update<T>(T model, ClaimsPrincipal user);
        Task<ICommandResult> Delete<T>(T model, ClaimsPrincipal user);
        

    }
}
