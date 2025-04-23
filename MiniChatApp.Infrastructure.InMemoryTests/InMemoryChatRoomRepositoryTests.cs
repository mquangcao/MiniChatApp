using MiniChatApp.SDK.Entities;
using MiniChatApp.SDK.Repositories;

namespace MiniChatApp.Infrastructure.InMemory.Tests
{
    [TestClass]
    public class InMemoryChatRoomRepositoryTests
    {
        private IChatRoomRepository _repository = default!;

        [TestInitialize]
        public void Setup()
        {
            _repository = new InMemoryChatRoomRepository();
        }

        [TestMethod]
        public async Task AddParticipantAsyncTest_AddsParticipantToExistingChatRoom()
        {
            // Arrange
            var chatRoom = new ChatRoom { Id = Guid.NewGuid(), ManagerId = Guid.NewGuid(), Participants = [] };
            await _repository.CreateChatRoomAsync(chatRoom);
            var userId = Guid.NewGuid();

            // Act
            var result = await _repository.AddParticipantAsync(chatRoom.Id, userId);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(chatRoom.Participants.Contains(userId));
        }

        [TestMethod]
        public async Task AddParticipantAsyncTest_ReturnsFalseForNonExistentChatRoom()
        {
            // Act
            var result = await _repository.AddParticipantAsync(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateChatRoomAsyncTest_CreatesChatRoomSuccessfully()
        {
            // Arrange
            var chatRoom = new ChatRoom { Id = Guid.NewGuid(), ManagerId = Guid.NewGuid(), Participants = [] };

            // Act
            await _repository.CreateChatRoomAsync(chatRoom);

            // Assert
            var retrievedChatRoom = await _repository.GetChatRoomAsync(chatRoom.Id);
            Assert.AreEqual(chatRoom, retrievedChatRoom);
        }

        [TestMethod]
        public async Task GetChatRoomAsyncTest_ReturnNull()
        {
            // Act
            var chatRoom = await _repository.GetChatRoomAsync(Guid.NewGuid());
            Assert.IsNull(chatRoom);
        }

        [TestMethod]
        public async Task GetChatRoomsJoinAsyncTest_ReturnsChatRoomsForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var chatRoom1 = new ChatRoom { Id = Guid.NewGuid(), ManagerId = userId, Participants = new List<Guid>() };
            var chatRoom2 = new ChatRoom { Id = Guid.NewGuid(), ManagerId = Guid.NewGuid(), Participants = new List<Guid> { userId } };
            var chatRoom3 = new ChatRoom { Id = Guid.NewGuid(), ManagerId = Guid.NewGuid(), Participants = new List<Guid>() };

            await _repository.CreateChatRoomAsync(chatRoom1);
            await _repository.CreateChatRoomAsync(chatRoom2);
            await _repository.CreateChatRoomAsync(chatRoom3);

            // Act
            var result = await _repository.GetChatRoomsJoinAsync(userId);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(chatRoom1));
            Assert.IsTrue(result.Contains(chatRoom2));
        }

        [TestMethod]
        public async Task GetChatRoomsJoinAsyncTest_ReturnsEmptyListForUserWithNoRooms()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var result = await _repository.GetChatRoomsJoinAsync(userId);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}