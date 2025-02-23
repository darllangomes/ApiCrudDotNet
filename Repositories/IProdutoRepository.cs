using ApiCrud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCrud.Repositories {
    public interface IProdutoRepository{
        Task<IEnumerable <Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task Add (Produto produto);
        Task Update (Produto produto);
        Task Delete (int id);
    }
}
