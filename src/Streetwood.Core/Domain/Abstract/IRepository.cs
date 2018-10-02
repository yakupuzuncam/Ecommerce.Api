﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Streetwood.Core.Domain.Abstract
{
    public interface IRepository<T> where T : Entity
    {
        Task<IList<T>> GetAsync();

        Task<T> GetAsync(Guid id);

        Task<T> GetAndEnsureExist(Guid id);

        Task Update(T entity);

        Task Delete(T entity);

        Task SaveChangesAsync();
    }
}
