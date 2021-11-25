﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSafe.Domain.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetALL();
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(int id);
    }
}
