using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WSafe.Domain.Data;

namespace WSafe.Domain.Repositories.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EmpresaContext _empresaContext;
        public GenericRepository(EmpresaContext empresaContext)
        {
            _empresaContext = empresaContext;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new Exception("La entidad es nula");

            _empresaContext.Set<TEntity>().Remove(entity);
            await _empresaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetALL()
        {
            return await _empresaContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _empresaContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {


            // _empresaContext.Set<Riesgo>().Add(entity);
            await _empresaContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _empresaContext.Entry(entity).State = EntityState.Modified;
            await _empresaContext.SaveChangesAsync();
            return entity;
        }
    }
}