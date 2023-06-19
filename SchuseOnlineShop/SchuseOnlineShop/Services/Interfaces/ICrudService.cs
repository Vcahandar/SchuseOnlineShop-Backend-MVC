using SchuseOnlineShop.Models.Common;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface ICrudService<T>where T : BaseEntity
    {
        Task CreateAsync(T entity);
        void Delete(T entity);
        Task SaveAsync();
        void Edit(T entity);
        //Task<T> GetByIdWithTrack(int? id);
    }
}
