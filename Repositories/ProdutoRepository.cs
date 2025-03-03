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

            if (id <= 0)
            {
                throw new ArgumentException("ID inválido. ID deve ser maior que zero.");

            }

            try
            {
                var produto = _produtos.SingleOrDefault(p => p.Id == id);
                if (produto == null)
                {
                    throw new KeyNotFoundException($"Produto com ID ${id} não encontrado.");
                }

                return await Task.FromResult(produto);

            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Mais de um produto com o mesmo ID encontrado.", ex);

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado.", ex);
            }


        }

        public Task Update(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}