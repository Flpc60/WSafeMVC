using System.Collections.Generic;
using System.Threading.Tasks;
using WSafe.Domain.Data;

namespace WSafe.Domain.Repositories.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EmpresaContext _empresaContext;

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if(entity != null)
            {
                await _empresaContext.Set<TEntity>().Remove(entity);

            }
        }

        public Task<IEnumerable<TEntity>> GetALL()
        {
            throw new System.NotImplementedException();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _empresaContext.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity> Insert(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
