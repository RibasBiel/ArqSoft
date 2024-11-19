using Application;
using Application.Dtos;
using Application.Guest;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;


namespace ApplicationTest
{
    public class Test
    {
        private GuestManager guestManager;

        [Test]
        public async Task Teste1()
        {
            //Assert Pass();
            
        }

        [Test]
        public async Task HappyPath()
        {
            var guestDto = new GuestDto
            {
                Name = "Fulana",
                Surname = "De Tal",
                Email = "Fulana@email.com",
                IdNumber = "abcd",
                IdTypeCode = 1,

            };

            int expectedId = 222;
            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x=>x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));
            guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            Assert.AreEqual(res.Data.Name,guestDto.Name);
            Assert.AreEqual(res.Data.Surname,guestDto.Surname);
            Assert.AreEqual(res.Data.Email,guestDto.Email);
            Assert.AreEqual(res.Data.IdNumber, guestDto.IdNumber);
            Assert.AreEqual(res.Data.IdTypeCode, guestDto.IdTypeCode);
        }

        [TestCase("")]
        [TestCase("")]
        [TestCase("")]
        [TestCase("")]
        [TestCase("")]
        public async Task ShouldReturnInvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDto
            {
                Name = "Fulana",
                Surname = "De Tal",
                Email = "Fulana@email.com",
                IdNumber = "abcd",
                IdTypeCode = 1,

            };

            int expectedId = 222;
            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };
            var fakeRepo = new Mock<IGuestRepository>();
            
            guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);
            
            Assert.AreEqual(res.ErrorCode, ErrorCode.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message,"");
            
        }
    }
}
