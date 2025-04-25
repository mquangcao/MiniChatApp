
using FluentAssertions;
using MiniChatApp.Infrastructure.MongoDb;
using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Repositories;

namespace RepositoryTests
{
    public class ChatRoomRepositoryTests
    {
        private readonly IChatRoomRepository _repository;

        public ChatRoomRepositoryTests()
        {
            _repository = new MongoDbChatRoomRepository(new MongoDbChatRoomOptions()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "MiniChatAppTest"
            });
        }

        [Fact]
        public async Task CreateChatRoomAsync_WithNonExistingChatRoom_CreatesChatRoomSuccessfully()
        {
            // Arrange
            var chatRoom = new ChatRoom
            {
                Id = Guid.NewGuid(),
                Name = "Test Chat Room",
                ManagerId = Guid.NewGuid(),
                Participants = new List<Guid>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            await _repository.CreateChatRoomAsync(chatRoom);
            var retrievedChatRoom = await _repository.GetChatRoomAsync(chatRoom.Id);

            // Assert
            retrievedChatRoom.Should().NotBeNull();
            retrievedChatRoom.Should().BeEquivalentTo(chatRoom, options => options.Excluding(cr => cr.CreatedAt).Excluding(cr => cr.UpdatedAt));
        }
    }
}
