// Scripts/Interfaces/IApiService.cs
using System.Threading.Tasks;

public interface IApiService
{
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<TResponse> GetAsync<TResponse>(string endpoint);
    Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<bool> DeleteAsync(string endpoint);
}
