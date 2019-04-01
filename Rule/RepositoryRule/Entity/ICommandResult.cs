using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface  ICommandResult
    {
        bool Success { get; }
        string ErrorText { get; set; }
    }
}
