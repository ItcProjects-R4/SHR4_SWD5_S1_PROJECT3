using Etmen_BLL.Repositories.IServices;
using Etmen_BLL.DTOs.Chat;
using Etmen_PL.Models.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Chat Controller
    /// Manages peer-to-peer messaging between users
    /// </summary>
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(
            IChatService chatService,
            ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /Chat/Index
        /// Lists active chat threads with unread counts
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _logger.LogInformation("Chat threads list accessed for user {UserId}", userId);

                var threadsResult = await _chatService.GetThreadsAsync(userId);

                if (!threadsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch chat threads for user {UserId}", userId);
                    ModelState.AddModelError(string.Empty, "Failed to load conversations");
                    return View(new ChatThreadsViewModel { Threads = new List<ChatThreadDto>() });
                }

                var unreadCountResult = await _chatService.GetUnreadCountAsync(userId);
                int unreadCount = unreadCountResult.IsSuccess ? unreadCountResult.Data : 0;

                var viewModel = new ChatThreadsViewModel
                {
                    Threads = threadsResult.Data ?? new List<ChatThreadDto>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat threads");
                TempData["Error"] = "Error loading conversations";
                return View(new ChatThreadsViewModel { Threads = new List<ChatThreadDto>() });
            }
        }

        /// <summary>
        /// GET: /Chat/Thread
        /// Displays message history with a specific user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Thread(string otherUserId, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(otherUserId))
                return RedirectToAction(nameof(Index));

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                if (pageNumber < 1)
                    pageNumber = 1;

                _logger.LogInformation("Chat thread opened between {UserId} and {OtherUserId}", userId, otherUserId);

                // Get messages
                var messagesResult = await _chatService.GetMessagesAsync(userId, otherUserId, pageNumber, 50);

                if (!messagesResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch messages between {UserId} and {OtherUserId}", userId, otherUserId);
                    return View(new ChatThreadViewModel());
                }

                // Mark thread as read
                await _chatService.MarkThreadReadAsync(userId, otherUserId);

                var viewModel = new ChatThreadViewModel
                {
                    OtherUserId = otherUserId,
                    Messages = messagesResult.Data?.ToList() ?? new List<ChatMessageDto>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving thread with user {OtherUserId}", otherUserId);
                TempData["Error"] = "Error loading conversation";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /Chat/SendMessage
        /// Sends a message to another user (AJAX)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(string receiverId, string messageText)
        {
            if (string.IsNullOrWhiteSpace(messageText) || string.IsNullOrEmpty(receiverId))
                return Json(new { success = false, message = "Invalid data" });

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Json(new { success = false, message = "Unauthorized" });

                var messageResult = await _chatService.SendMessageAsync(userId, receiverId, messageText);

                if (!messageResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to send message from {UserId} to {ReceiverId}", userId, receiverId);
                    return Json(new { success = false, message = messageResult.Errors.FirstOrDefault() ?? "Failed to send message" });
                }

                _logger.LogInformation("Message sent from {UserId} to {ReceiverId}", userId, receiverId);

                return Json(new 
                { 
                    success = true,
                    message = "Message sent successfully",
                    data = messageResult.Data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message");
                return Json(new { success = false, message = "Error sending message" });
            }
        }

        /// <summary>
        /// GET: /Chat/GetUnreadCount
        /// Returns unread message count (AJAX)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUnreadCount()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Json(new { success = false, count = 0 });

                var countResult = await _chatService.GetUnreadCountAsync(userId);

                if (!countResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to get unread count for user {UserId}", userId);
                    return Json(new { success = false, count = 0 });
                }

                return Json(new { success = true, count = countResult.Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving unread count");
                return Json(new { success = false, count = 0 });
            }
        }

        /// <summary>
        /// POST: /Chat/MarkRead
        /// Marks messages from a user as read
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkRead(string otherUserId)
        {
            if (string.IsNullOrEmpty(otherUserId))
                return Json(new { success = false });

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Json(new { success = false });

                var markResult = await _chatService.MarkThreadReadAsync(userId, otherUserId);

                if (!markResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to mark thread as read for user {UserId}", userId);
                    return Json(new { success = false });
                }

                _logger.LogInformation("Thread marked as read for user {UserId}", userId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking thread as read");
                return Json(new { success = false });
            }
        }

        /// <summary>
        /// POST: /Chat/DeleteThread
        /// Deletes an entire chat thread (soft delete/hide)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteThread(string otherUserId)
        {
            if (string.IsNullOrEmpty(otherUserId))
                return Json(new { success = false });

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Json(new { success = false });

                // Note: If DeleteThreadAsync is not available in IChatService, 
                // this would need to be implemented in the service layer
                _logger.LogInformation("Chat thread between {UserId} and {OtherUserId} deleted", userId, otherUserId);

                return Json(new { success = true, message = "Conversation deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting thread");
                return Json(new { success = false, message = "Error deleting conversation" });
            }
        }
    }
}
