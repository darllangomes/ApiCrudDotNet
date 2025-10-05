using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        // private readonly List<Produto> _produtos = new List<Produto>();

        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException(
                    "Nome do produto não pode ser nulo ou vazio.",
                    nameof(produto.Nome)
                );
            }
            if (produto.Preco < 0)
            {
                throw new ArgumentException(
                    "O preço do produto não pode ser negativo.",
                    nameof(produto.Preco)
                );
            }

            try
            {
                await _context.Produtos.AddAsync(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar produto.", ex);
            }
        }

        public async Task Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
            await _context.SaveChangesAsync();
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
                var produtosPaginados = await _context
                    .Produtos.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return produtosPaginados;
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
                var produto = await _context.Produtos.SingleOrDefaultAsync(p => p.Id == id);
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
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");
            }
            if (produto.Id <= 0)
            {
                throw new ArgumentException(
                    "O ID do produto deve ser maior que zero.",
                    nameof(produto.Id)
                );
            }
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException(
                    "O nome do produto não pode ser nulo ou vazio.",
                    nameof(produto.Nome)
                );
            }
            if (produto.Preco < 0)
            {
                throw new ArgumentException(
                    "O preço do produto não pode ser negativo.",
                    nameof(produto.Preco)
                );
            }

            var produtoExistente = await _context.Produtos.FirstOrDefaultAsync(p =>
                p.Id == produto.Id
            );
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException($"Produto com ID {produto.Id} não encontrado.");
            }

            try
            {
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Preco = produto.Preco;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o produto.", ex);
            }
        }
    }
}
