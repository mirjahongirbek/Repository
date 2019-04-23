
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryRule.Enums
{
    [Flags]
    public enum Actions
    {
        Create=0,
        Read=1,
        Update=2,
        Delete=3, 
        GetAll,
        GetById,

    }

}
