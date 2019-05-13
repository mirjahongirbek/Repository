using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IValidator
    {
        int Id { get; set; }
        string ClassName { get; set; }
        int? StatusCode { get; set; }
        string Tags { get; set; }
        string Fields { get; set; }
        string Description { get; set; }
        int? HttpSatusCode { get; set; }

    }
}
