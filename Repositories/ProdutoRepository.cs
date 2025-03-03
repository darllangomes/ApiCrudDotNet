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


        public async Task Delete(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                _produtos.Remove(produto);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await Task.FromResult(_produtos);

        }

        public async Task<Produto> GetById(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);

            return await Task.FromResult(produto);

        }

        public Task Update(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}