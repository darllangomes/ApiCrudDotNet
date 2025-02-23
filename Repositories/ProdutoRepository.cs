using ApiCrud.Models;

using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace ApiCrud.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly List<Produto> _produtos = new List<Produto>();


        public async Task Add(Produto produto)
        {
            _produtos.Add(produto);
            await Task.CompletedTask;
        }


        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await Task.FromResult(_produtos);

        }

        public Task<Produto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}