﻿using RepositoryRule.Entity;

namespace LanguageService.Interfaces
{
    public interface ILanguageService<TKey>
        where TKey:class , IEntity<TKey>
    {

    }

}
