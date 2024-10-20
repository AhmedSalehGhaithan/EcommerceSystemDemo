namespace eCommerce.Domain.Interfaces
{
    /// <summary>
    /// Defines a generic interface for repository operations on entities of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the repository operates on.</typeparam>
    public interface IGeneric<TEntity> where TEntity : class
    {
        /// <summary>
        /// Asynchronously retrieves all entities of type TEntity.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a collection of TEntity.</returns>
        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation, containing the requested TEntity.</returns>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Asynchronously creates a new entity of type TEntity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task that represents the asynchronous operation, containing the identifier of the created entity.</returns>
        Task<int> CreateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously updates an existing entity of type TEntity.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result of the update operation.</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result of the delete operation.</returns>
        Task<int> DeleteAsync(Guid id);
    }
}
