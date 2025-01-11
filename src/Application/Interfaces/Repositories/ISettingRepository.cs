using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISettingRepository
    {
        List<Setting> GetSettings(string? useranme = null);
    }
}
