using System.Collections;
using System.Collections.Generic;

namespace RepositoryRule.Entity
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
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
