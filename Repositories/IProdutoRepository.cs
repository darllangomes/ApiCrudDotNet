using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCrud.Models;

namespace ApiCrud.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll(int pageNumber, int pageSize);
        Task<Produto> GetById(int id);
        Task Add(Produto produto);
        Task Update(Produto produto);
        Task Delete(int id);
    }
}
