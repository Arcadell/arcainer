using Domain.Entities;

namespace Domain.Dtos;

public class UpdateSettingDto: BaseEntity
{
    public bool DisableRegistration { get; set; } = true;
}
