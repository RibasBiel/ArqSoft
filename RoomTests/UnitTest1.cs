using Application;
using Application.Dtos;
using Application.Guest.Requests;
using Application.Guest;
using Application.Room;
using Application.Room.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;
using Domain.Enums;

namespace RoomTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task ShouldInsertDataWithSuccess()
        {
            var roomDto = new RoomDto
            {
                Name = "Fulana",
                Level = 1,
                InMaintenance = false,
                

            };

            int expectedId = 222;
            var request = new CreateRoomRequest()
            {
                Data = roomDto,
            };
            var fakeRepo = new Mock<IRoomRepository>();
            fakeRepo.Setup(x => x.Create(It.IsAny<Room>())).Returns(Task.FromResult(expectedId));
            var roomManager = new RoomManager(fakeRepo.Object);
            var res = await roomManager.CreateRoom(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            Assert.AreEqual(res.Data.Name, roomDto.Name);
            Assert.AreEqual(res.Data.Level, roomDto.Level);
            Assert.AreEqual(res.Data.InMaintenance, roomDto.InMaintenance);
            
        }

        [Test]
        public async Task ShouldReturnCouldNotStoreData_WhenSaveFails()
        {
            // Arrange: Preparando o DTO com dados válidos
            var roomDto = new RoomDto
            {
                Name = "Valid Name",
                Level = 1,
                InMaintenance = false,
            };

            var request = new CreateRoomRequest()
            {
                Data = roomDto,
            };

            var fakeRepo = new Mock<IRoomRepository>();

            
            fakeRepo.Setup(repo => repo.Create(It.IsAny<Room>())).ThrowsAsync(new Exception("Database save failed"));

            var roomManager = new RoomManager(fakeRepo.Object);

            
            var res = await roomManager.CreateRoom(request);
            

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCode.COULD_NOT_STORE_DATA);
            Assert.AreEqual(res.Message, "There was an error when saving to DB");
        }

        [Test]
        public async Task Should_Return_RoomNotFound_RoomDoesntExist()
        {

            var fakeRepo = new Mock<IRoomRepository>();
            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Room>(null));

            var roomManager = new RoomManager(fakeRepo.Object);
            var res = await roomManager.GetRoom(333);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.NOT_FOUND);
            Assert.AreEqual(res.Message, "No room record was found with the given id");

        }

        [Test]
        public async Task Should_Return_IsAvailableFalse_IfRoom_IsInMaintenace()
        {
            var room = new Room { InMaintenance = true };
            var isAvailable = room.IsAvailable;
            Assert.IsFalse(isAvailable);


        }

        
    }
}