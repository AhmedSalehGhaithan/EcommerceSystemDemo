using eCommerce.Application.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    /// <summary>
    /// A generic repository for performing CRUD operations on entities of type TEntity.
    /// This class implements the IGeneric interface and provides methods for creating, retrieving,
    /// updating, and deleting entities in the database using the provided AppDbContext.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository operates on, constrained to class types.</typeparam>
    /// <param name="_context">An instance of AppDbContext used to interact with the database.</param>
    public class GenericRepository<TEntity>(AppDbContext _context) : IGeneric<TEntity> where TEntity : class
    {

        /// <summary>
        /// Asynchronously creates a new entity of type TEntity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null) return 0;
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type TEntity.
        /// </summary>
        /// <returns>A collection of TEntity.</returns>
        public async Task<ICollection<TEntity>> GetAllAsync() 
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The requested TEntity.</returns>
        /// <exception cref="ItemNotFoundException">Thrown when the entity is not found.</exception>
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id) ??
                throw new ItemNotFoundException($"item with {id} Not found");
            return entity!;
        }

        /// <summary>
        /// Asynchronously updates an existing entity of type TEntity.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity!);
            return await _context.SaveChangesAsync();
        }
    }
}
