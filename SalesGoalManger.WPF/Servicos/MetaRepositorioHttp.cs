using SalesGoalManager.RegraDeNegocio.Dto;
using SalesGoalManager.RegraDeNegocio.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace SalesGoalManger.WPF.Servicos
{
    public class MetaRepositorioHttp : IMetaRepositorio
    {
        private readonly HttpClient _http;

        public MetaRepositorioHttp(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MetaVendedorDto>> ObterTodasAsync()
        {
            return await _http.GetFromJsonAsync<List<MetaVendedorDto>>("Meta") ?? new();
        }

        public async Task<MetaVendedorDto> ObterPorIdAsync(string id)
        {
            return await _http.GetFromJsonAsync<MetaVendedorDto>($"Meta/{id}");
        }

        public async Task AdicionarAsync(MetaVendedorDto meta)
        {
            var resposta = await _http.PostAsJsonAsync("Meta", meta);
            resposta.EnsureSuccessStatusCode();
        }

        public async Task AtualizarAsync(MetaVendedorDto meta)
        {
            var resposta = await _http.PutAsJsonAsync($"Meta/{meta.Id}", meta);
            resposta.EnsureSuccessStatusCode();
        }

        public async Task RemoverAsync(string id)
        {
            var resposta = await _http.DeleteAsync($"Meta/{id}");
            resposta.EnsureSuccessStatusCode();
        }
    }
}
