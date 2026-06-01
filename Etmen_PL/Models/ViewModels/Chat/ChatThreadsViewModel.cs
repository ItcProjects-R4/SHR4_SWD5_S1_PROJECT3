using Etmen_BLL.DTOs.Chat;

namespace Etmen_PL.Models.ViewModels.Chat
{
    public class ChatThreadsViewModel
    {
        public IEnumerable<ChatThreadDto> Threads { get; set; } = new List<ChatThreadDto>();
    }
}
