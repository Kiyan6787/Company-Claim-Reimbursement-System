using ClaimsReimbursement.Infrastructure.Context;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using ClaimsReimbursement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDBContext _context;
        private readonly DbSet<T> _entities;

        protected Repository(AppDBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        /// <summary>
        /// Repository method for getting all claims.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _entities.AsNoTracking().ToListAsync();

        /// <summary>
        /// Repository method for getting a claim by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetByIdAsync(int? id) =>
            await _entities.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);

        /// <summary>
        /// Repository method for creating a claim.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<bool> InsertAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Repository method for editing a claim.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Repository method for deleting a claim.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
