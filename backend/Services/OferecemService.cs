using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class OferecemService : IOferecemService
    {
        private readonly IOferecemRepository _oferecemRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IServicoRepository _servicoRepository;

        public OferecemService(
            IOferecemRepository oferecemRepository,
            IEmpresaRepository empresaRepository,
            ICidadeRepository cidadeRepository,
            IServicoRepository servicoRepository)
        {
            _oferecemRepository = oferecemRepository;
            _empresaRepository = empresaRepository;
            _cidadeRepository = cidadeRepository;
            _servicoRepository = servicoRepository;
        }

        public async Task<IEnumerable<Oferecem>> GetAllAsync()
        {
            return await _oferecemRepository.GetAllAsync();
        }

        public async Task<Oferecem?> GetByKeyAsync(int idEmpresa, int idCidade, string nomeServico)
        {
            var item = await _oferecemRepository.GetByKeyAsync(idEmpresa, idCidade, nomeServico);
            if (item == null)
            {
                throw new KeyNotFoundException("Associação de preço para esta empresa, cidade e serviço não foi encontrada.");
            }
            return item;
        }

        public async Task<bool> CreateAsync(Oferecem oferecem)
        {
            if (oferecem.PrecoHora <= 0)
                throw new ArgumentException("O preço por hora deve ser um valor maior que zero.");

            var empresa = await _empresaRepository.GetByIdAsync(oferecem.IdEmpresa);
            if (empresa == null)
                throw new KeyNotFoundException($"Empresa com ID {oferecem.IdEmpresa} não encontrada.");

            var cidade = await _cidadeRepository.GetByIdAsync(oferecem.IdCidade);
            if (cidade == null)
                throw new KeyNotFoundException($"Cidade com ID {oferecem.IdCidade} não encontrada.");

            var servico = await _servicoRepository.GetByIdAsync(oferecem.NomeServico);
            if (servico == null)
                throw new KeyNotFoundException($"Serviço '{oferecem.NomeServico}' não encontrado.");

            return await _oferecemRepository.CreateAsync(oferecem);
        }
    }
}
