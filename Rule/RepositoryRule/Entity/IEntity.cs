﻿namespace RepositoryRule.Entity
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}