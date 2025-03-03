using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.Models;

namespace ApiCrud.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly List<Produto> _produtos = new List<Produto>();

        public async Task Add(Produto produto)
        {
            if(produto == null){
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");
            }
            if(produto.Id <= 0){
                throw new ArgumentException("O ID do produto deve ser maior que zero.", nameof(produto.Id));
            }
            if(string.IsNullOrWhiteSpace(produto.Nome){
                throw new ArgumentException("Nome do produto não pode ser nulo ou vazio.", nameof(produto.Nome));
            }
            if(produto.Preco < 0){
                throw new ArgumentException("O preço do produto não pode ser negativo.", nameof(produto.Preco));
            }
            if(_produtos.Any(p=>p.Id == produto.Id)){
                throw new InvalidOperationException($"Já existe um produto com o ID {produto.Id}");
            }

            try{
                _produtos.Add(produto);
                await Task.CompletedTask;
            }catch(Exception ex){
                throw new Exception("Erro ao adicionar produto.", ex);
            }

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

        public async Task<IEnumerable<Produto>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    throw new ArgumentException(
                        "Número da página e tamanho da página devem ser maiores que zero."
                    );
                }
                var produtosPaginados = _produtos
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return await Task.FromResult(produtosPaginados.AsReadOnly());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter produtos: {ex.Message}", ex);
            }
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
                throw new InvalidOperationException(
                    "Mais de um produto com o mesmo ID encontrado.",
                    ex
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado.", ex);
            }
        }

        public async Task Update(Produto produto)
        {
            if(produto == null){
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");
            }
            if(produto.Id <= 0){
                throw new ArgumentException("O ID do produto deve ser maior que zero.", nameof(produto.Id));
            }
            if(string.IsNullOrWhiteSpace(produto.Nome)){
                throw new ArgumentException("O nome do produto não pode ser nulo ou vazio.", nameof(produto.Nome));
            }
            if(produto.Preco < 0){
                throw new ArgumentException("O preço do produto não pode ser negativo.", nameof(produto.Preco));
            }

             var produtoExistente = _produtos.FirstOrDefault( p => p.Id == produto.Id);
             if(produtoExistente == null){
                throw new KeyNotFoundException($"Produto com ID {produto.Id} não encontrado.");
             }

             try{
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Preco = produto.Preco;
                await Task.CompletedTask;
             }catch(Exception ex){
                throw new Exception("Erro ao atualizar o produto.", ex);
             }
        }
    }
}
