namespace APBD12.Services
{
    public interface IClientService
    {
        Task<string> DeleteClientAsync(int idClient);
    }
}