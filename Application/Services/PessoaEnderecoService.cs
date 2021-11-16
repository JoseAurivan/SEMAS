using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.DataStructures;

namespace Application.Services
{
    internal class PessoaEnderecoService : IPessoaEnderecoService
    {
        private readonly IContext _context;
        private readonly ILogger<PessoaEnderecoService> _logger;

        public PessoaEnderecoService(IContext context, ILogger<PessoaEnderecoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult> SavePessoa(Pessoa pessoa, Endereco endereco)
        {
            try
            {
                return await TrySavePessoa(pessoa, endereco);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao cadastrar pessoa. CPF já cadastrado no sistema." }
                };
            }
        }

        private async Task<ServiceResult> TrySavePessoa(Pessoa pessoa, Endereco endereco)
        {
            if (pessoa is null || endereco is null)
            {
                return new ServiceResult(ServiceResultType.NotFound)
                {
                    Messages = new[]
                    {
                        "Erro ao cadastrar pessoa"
                    }
                };
            }

            if (pessoa.Id == default) _context.Pessoas.Add(pessoa);
            else _context.Entry(pessoa).State = EntityState.Modified;

            if (endereco.Id == default) _context.Enderecos.Add(endereco);
            else _context.Entry(endereco).State = EntityState.Modified;

            PessoaEndereco pe = new PessoaEndereco();
            pe.Pessoa = pessoa;
            pe.Endereco = endereco;

            _context.PessoaEnderecos.Add(pe);

            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = pessoa.Id
            };
        }

        public async Task<ServiceResult> SearchForCpfAsync(string cpf)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    //    .AsNoTracking()
                    .Include(x => x.Enderecos)
                    .FirstOrDefaultAsync(x => x.Cpf == cpf);
                if (pessoa is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<Pessoa>(ServiceResultType.Success)
                {
                    Result = pessoa
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "CPF não cadastrado" }
                };
            }
        }

        public async Task<ServiceResult> FindPessoaAsync(int id)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    .Include(x => x.Enderecos)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (pessoa is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID da pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<Pessoa>(ServiceResultType.Success)
                {
                    Result = pessoa
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Pessoa não cadastrada" }
                };
            }
        }

        public async Task<ServiceResult> GetPessoaAsync(int id)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    .Include(x => x.Enderecos)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (pessoa is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID da pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<Pessoa>(ServiceResultType.Success)
                {
                    Result = pessoa
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Pessoa não cadastrada" }
                };
            }
        }

        public async Task<ServiceResult> SaveCestaBasica(Endereco endereco, CestaBasica cestaBasica)
        {
            try
            {
                return await TrySaveCestaBasica(endereco, cestaBasica);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao cadastrar pessoa" }
                };
            }
        }

        public async Task<ServiceResult> SearchForEndereco(int idPessoa)
        {
            try
            {
                var pessoaEndereco = await _context.PessoaEnderecos
                    .AsNoTracking()
                    .Include(x => x.Pessoa)
                    .Include(x => x.Endereco)
                    .FirstOrDefaultAsync(x => x.PessoaId == idPessoa);

                if (pessoaEndereco is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID da pessoa não encontrada."
                        }
                    };
                }

                var endereco = await _context.Enderecos
                    .AsNoTracking()
                    .Include(x => x.Pessoa)
                    .ThenInclude(x => x.Pessoa)
                    .Include(x => x.Cesta)
                    .ThenInclude(x => x.Entregas)
                    .FirstOrDefaultAsync(x => x.Pessoa.Contains(pessoaEndereco));

                if (endereco is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID do endereco não encontrado."
                        }
                    };
                }

                return new ServiceResult<Endereco>(ServiceResultType.Success)
                {
                    Result = endereco
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Endereco não cadastrada" }
                };
            }
        }

        public async Task<ServiceResult> SearchForCpfPessoaEnderecoAsync(string cpf)
        {
            try
            {
                var pessoaEndereco = await _context.PessoaEnderecos
                    .AsNoTracking()
                    .Where(x => x.Pessoa.Cpf == cpf)
                    .Include(x => x.Pessoa)
                    .Include(x => x.Endereco)
                    .ToListAsync();

                if (pessoaEndereco is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "CPF da pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<List<PessoaEndereco>>(ServiceResultType.Success)
                {
                    Result = pessoaEndereco
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Endereco não cadastrada" }
                };
            }
        }

        public async Task<ServiceResult> SavePessoaEndereco(PessoaEndereco pessoaEndereco)
        {
            try
            {
                return await TrySavePessoaEndereco(pessoaEndereco);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao cadastrar pessoa" }
                };
            }
        }

        public async Task<ServiceResult> SearchForPessoaEndereco(int idPessoa)
        {
            try
            {
                var pessoaEndereco = await _context.PessoaEnderecos
                    .AsNoTracking()
                    .Include(x => x.Pessoa)
                    .ThenInclude(x => x.CadastroCmas)
                    .Include(x => x.Endereco)
                    .ThenInclude(x => x.Cesta)
                    .ThenInclude(x => x.Entregas)
                    .FirstOrDefaultAsync(x => x.PessoaId == idPessoa);

                if (pessoaEndereco is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID da pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<PessoaEndereco>(ServiceResultType.Success)
                {
                    Result = pessoaEndereco
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Endereco não cadastrada" }
                };
            }
        }

        public async Task<ServiceResult> SearchForCestaBasica(int idCestaBasica)
        {
            try
            {
                var cestaBasica = await _context.CestaBasicas
                    .AsNoTracking()
                    .Include(x => x.Entregas)
                    .FirstOrDefaultAsync(x => x.Id == idCestaBasica);

                if (cestaBasica is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID da pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<CestaBasica>(ServiceResultType.Success)
                {
                    Result = cestaBasica
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Cesta Basica não cadastrada" }
                };
            }
        }

        public async Task<ServiceResult> UpdateCestaBasica(CestaBasica cestaBasica)
        {
            try
            {
                return await TryUpdateCestaBasica(cestaBasica);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao cadastrar pessoa" }
                };
            }
        }

        public async Task<ServiceResult> ListControlMonth(Unidade unidade, int mes, int ano)
        {
            try
            {
                var entregas = await _context.Entregas
                    .AsNoTracking()
                    .Where(x => x.Unidade == unidade
                                && x.DataEntrega.HasValue
                                && x.DataEntrega.Value.Month == mes
                                && x.DataEntrega.Value.Year == ano
                                && x.EntregaStatus == StatusEntrega.Entregue)
                    .ToListAsync();

                List<PessoaEndereco> pessoaEndereco = new List<PessoaEndereco>();

                foreach (var ent in entregas)
                {
                    var pessoa = _context.PessoaEnderecos
                        .AsNoTracking()
                        .Include(x => x.Pessoa)
                        .ThenInclude(x => x.CadastroCmas)
                        .Include(x => x.Endereco)
                        .ThenInclude(x => x.Cesta)
                        .ThenInclude(x => x.Entregas)
                        .Where(x => x.Endereco.Cesta.Entregas.Contains(ent))
                        .FirstOrDefaultAsync();
                    if (pessoa is not null)
                        pessoaEndereco.Add(pessoa.Result);
                }

                if (pessoaEndereco.Count == 0)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Dados nao foram encontrados"
                        }
                    };
                }

                return new ServiceResult<List<PessoaEndereco>>(ServiceResultType.Success)
                {
                    Result = pessoaEndereco.Distinct().ToList()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao gerar relatorios " }
                };
            }
        }

        public async Task<ServiceResult> ListControlAllMonth(int mes, int ano)
        {
            try
            {
                var entregas = await _context.Entregas
                    .AsNoTracking()
                    .Where(x => x.DataEntrega.HasValue
                                && x.DataEntrega.Value.Month == mes
                                && x.DataEntrega.Value.Year == ano
                                && x.EntregaStatus == StatusEntrega.Entregue)
                    .ToListAsync();

                List<PessoaEndereco> pessoaEndereco = new List<PessoaEndereco>();

                foreach (var ent in entregas)
                {
                    var pessoa = await _context.PessoaEnderecos
                        .AsNoTracking()
                        .Include(x => x.Pessoa)
                        .ThenInclude(x => x.CadastroCmas)
                        .Include(x => x.Endereco)
                        .ThenInclude(x => x.Cesta)
                        .ThenInclude(x => x.Entregas)
                        .Where(x => x.Endereco.Cesta.Entregas.Contains(ent))
                        .FirstOrDefaultAsync();
                    if (pessoa is not null)
                        pessoaEndereco.Add(pessoa);
                }

                if (pessoaEndereco.Count == 0)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Dados nao foram encontrados"
                        }
                    };
                }

                return new ServiceResult<List<PessoaEndereco>>(ServiceResultType.Success)
                {
                    Result = pessoaEndereco.Distinct().ToList()
                };
            }
            catch(Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao gerar relatorios " }
                };
            }
        }

        public async Task<ServiceResult> ListControlYear(Unidade unidade, int ano)
        {
            try
            {
                var entregas = await _context.Entregas
                    .AsNoTracking()
                    .Where(x => x.Unidade == unidade
                                && x.DataEntrega.HasValue
                                && x.DataEntrega.Value.Year == ano
                                && x.EntregaStatus == StatusEntrega.Entregue)
                    .ToListAsync();

                List<PessoaEndereco> pessoaEndereco = new List<PessoaEndereco>();

                foreach (var ent in entregas)
                {
                    var pessoa = await _context.PessoaEnderecos
                        .AsNoTracking()
                        .Include(x => x.Pessoa)
                        .ThenInclude(x => x.CadastroCmas)
                        .Include(x => x.Endereco)
                        .ThenInclude(x => x.Cesta)
                        .ThenInclude(x => x.Entregas)
                        .Where(x => x.Endereco.Cesta.Entregas.Contains(ent))
                        .FirstOrDefaultAsync();
                    if (pessoa is not null)
                        pessoaEndereco.Add(pessoa);
                }

                if (pessoaEndereco.Count == 0)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Dados nao foram encontrados"
                        }
                    };
                }

                return new ServiceResult<List<PessoaEndereco>>(ServiceResultType.Success)
                {
                    Result = pessoaEndereco.Distinct().ToList()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao gerar relatorios " }
                };
            }
        }

        public async Task<ServiceResult> ListControlAllYear(int ano)
        {
            try
            {
                var entregas = await _context.Entregas
                    .AsNoTracking()
                    .Where(x =>x.DataEntrega.HasValue
                                && x.DataEntrega.Value.Year == ano
                                && x.EntregaStatus == StatusEntrega.Entregue)
                    .ToListAsync();

                List<PessoaEndereco> pessoaEndereco = new List<PessoaEndereco>();

                foreach (var ent in entregas)
                {
                    var pessoa = await _context.PessoaEnderecos
                        .AsNoTracking()
                        .Include(x => x.Pessoa)
                        .ThenInclude(x => x.CadastroCmas)
                        .Include(x => x.Endereco)
                        .ThenInclude(x => x.Cesta)
                        .ThenInclude(x => x.Entregas)
                        .Where(x => x.Endereco.Cesta.Entregas.Contains(ent))
                        .FirstOrDefaultAsync();
                   if (pessoa is not null)
                        pessoaEndereco.Add(pessoa);
                  
                }

                if (pessoaEndereco.Count == 0)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Dados nao foram encontrados"
                        }
                    };
                }

               
                return new ServiceResult<List<PessoaEndereco>>(ServiceResultType.Success)
                {
                    Result = pessoaEndereco.Distinct().ToList()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao gerar relatorios " }
                };
            }
        }

        private async Task<ServiceResult> TryUpdateCestaBasica(CestaBasica cestaBasica)
        {
            if (cestaBasica is null)
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Dados nulos" }
                };

            if (cestaBasica.Id == default) _context.CestaBasicas.Add(cestaBasica);
            else _context.Entry(cestaBasica).State = EntityState.Modified;

            foreach (var entrega in cestaBasica.Entregas)
            {
                entrega.CestaBasica = cestaBasica;
                if (entrega.Id == default) _context.Entregas.Add(entrega);
                else _context.Entry(entrega).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = cestaBasica.Id
            };
        }

        private async Task<ServiceResult> TrySavePessoaEndereco(PessoaEndereco pessoaEndereco)
        {
            if (pessoaEndereco is null)
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Dados nulos" }
                };

            if (pessoaEndereco.PessoaId == default && pessoaEndereco.EnderecoId == default)
            {
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "IDs nao deveriam ser nulos" }
                };
            }

            _context.Entry(pessoaEndereco).State = EntityState.Modified;
            _context.Entry(pessoaEndereco.Endereco).State = EntityState.Modified;
            _context.Entry(pessoaEndereco.Pessoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = pessoaEndereco.EnderecoId
            };
        }

        private async Task<ServiceResult> TrySaveCestaBasica(Endereco endereco, CestaBasica cestaBasica)
        {
            if (endereco is null || cestaBasica is null)
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Dados nulos" }
                };

            if (cestaBasica.Id == default) _context.CestaBasicas.Add(cestaBasica);
            else _context.Entry(cestaBasica).State = EntityState.Modified;

            foreach (var entrega in cestaBasica.Entregas)
            {
                entrega.CestaBasica = cestaBasica;
                if (entrega.Id == default) _context.Entregas.Add(entrega);
                else _context.Entry(entrega).State = EntityState.Modified;
            }

            endereco.Cesta = cestaBasica;

            _context.Entry(endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = cestaBasica.Id
            };
        }
    }
}