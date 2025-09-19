using CompanyChatService.Domain.Common;

namespace CompanyChatService.Domain.Entities;

public class ChatRoom : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Message> Messages { get; set; } = [];
}
