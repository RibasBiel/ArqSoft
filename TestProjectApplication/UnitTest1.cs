using Application;
using Application.Dtos;
using Application.Guest;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace TestProjectApplication
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
            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));
            var guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            Assert.AreEqual(res.Data.Name, guestDto.Name);
            Assert.AreEqual(res.Data.Surname, guestDto.Surname);
            Assert.AreEqual(res.Data.Email, guestDto.Email);
            Assert.AreEqual(res.Data.IdNumber, guestDto.IdNumber);
            Assert.AreEqual(res.Data.IdTypeCode, guestDto.IdTypeCode);
        }

        
        [TestCase("")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task ShouldReturnInvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDto
            {
                Name = "Fulana",
                Surname = "De Tal",
                Email = "Fulana@email.com",
                IdNumber = docNumber,
                IdTypeCode = 1,

            };

            int expectedId = 222;
            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };
            var fakeRepo = new Mock<IGuestRepository>();

            var guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The passed ID is not valid");

        }

        [TestCase("","Surname Teste","email@teste.com")]
        [TestCase("Fulano", "", "email@teste.com")]
        [TestCase("Fulano", "Surname Teste", "")]
        [TestCase("", "", "")]
        public async Task ShouldReturnMissingRequiredInformation_WhenDocsAreInvalid(string name,string surname, string email)
        {
            var guestDto = new GuestDto
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1,

            };

            
            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };
            var fakeRepo = new Mock<IGuestRepository>();

            var guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing passed required information");

        }

        [TestCase("emailsemarrobaesemponto")]
        [TestCase("b@b.com")]
        
        public async Task Should_Return_InvalidEmailExcpetion_WhenDocsAreInvalid( string email)
        {
            var guestDto = new GuestDto
            {
                Name = "Fulana",
                Surname = "De Tal",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1,

            };


            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };
            var fakeRepo = new Mock<IGuestRepository>();

            var guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.INVALID_EMAIL);
            Assert.AreEqual(res.Message, "The given email is not valid");

        }

        [Test]
        public async Task Should_Return_GuestNotFound_GuestDoesntExist()
        {
            
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest>(null));
            
            var guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.GetGuest(333);
            Assert.IsNotNull(res);
            Assert.False(res.Success);

            Assert.AreEqual(res.ErrorCode, ErrorCode.NOT_FOUND);
            Assert.AreEqual(res.Message, "No Guest record was found with the given id");

        }

        [Test]
        public async Task Should_Return_Guest_Success()
        {

            var fakeRepo = new Mock<IGuestRepository>();
            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    DocumentType = Domain.Enums.DocumentType.DriveLicence,
                    IdNumber = "123"
                }
            };
            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Guest?)fakeGuest));//O ? quer dizer que pode ser nulo.

            var guestManager = new GuestManager(fakeRepo.Object);
            var res = await guestManager.GetGuest(333);
            Assert.IsNotNull(res);
            Assert.True(res.Success);

            Assert.AreEqual(res.Data.Id, fakeGuest.Id);
            Assert.AreEqual(res.Data.Name, fakeGuest.Name);
            

        }
    }
}