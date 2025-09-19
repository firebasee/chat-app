using CompanyChatService.Domain.Common;

namespace CompanyChatService.Domain.Entities;

public class Message : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatRoomId { get; set; }

    public virtual User? Sender { get; set; }
    public virtual ChatRoom? ChatRoom { get; set; }
}
