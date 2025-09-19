using CompanyChatService.Domain.Common;

namespace CompanyChatService.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
}
