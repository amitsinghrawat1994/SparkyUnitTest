

using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;

namespace Bongo.Core.Tests
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private StudyRoomBooking _request;
        private List<StudyRoom> _availalbeStudyRoom;
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;

        private StudyRoomBookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _request = new StudyRoomBooking
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmail.com",
                Date = new DateTime(2022, 1, 1)
            };

            _availalbeStudyRoom = new List<StudyRoom> {
                new StudyRoom
                {
                    Id = 10,
                    RoomName = "Michigan",
                    RoomNumber = "A202"
                }
            };

            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();

            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepoMock.Setup(x => x.GetAll())
                .Returns(_availalbeStudyRoom);


            _bookingService = new StudyRoomBookingService(
                _studyRoomBookingRepoMock.Object,
                _studyRoomRepoMock.Object
                );
        }

        [TestCase]
        public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
        {
            _bookingService.GetAllBooking();
            _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }

        [Test]
        public void BookingException_NullRequest_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => _bookingService.BookStudyRoom(null));

            Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }

        [Test]
        public void StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnResultWithAllValue()
        {
            //asert
            StudyRoomBooking saveStudyRoomBooking = null;

            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    saveStudyRoomBooking = booking;
                });


            // act
            _bookingService.BookStudyRoom(_request);

            //assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

            Assert.NotNull(saveStudyRoomBooking);
            Assert.AreEqual(_request.FirstName, saveStudyRoomBooking.FirstName);

            Assert.AreEqual(_availalbeStudyRoom.First().Id, saveStudyRoomBooking.StudyRoomId);
        }

        [Test]
        public void StudyRoomBookingResultCheck_InputRequest_ValuesMatchInResult()
        {
            StudyRoomBookingResult result = _bookingService.BookStudyRoom(_request);

            Assert.NotNull(result);

            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
        }

        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]

        public StudyRoomBookingCode ResultCodeSucces_RoomAvability_ReturnSuccessResultCode(bool roomAvailabllity)
        {
            if (!roomAvailabllity)
            {
                _availalbeStudyRoom.Clear();
            }

            //Assert.AreEqual(StudyRoomBookingCode.Success, result.Code); 
            return _bookingService.BookStudyRoom(_request).Code;
        }

        [TestCase(0, false)]
        [TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailality_ReturnBookingId(int expectedBookingId, bool roomAvailabiltiy)
        {
            if (!roomAvailabiltiy)
            {
                _availalbeStudyRoom.Clear();
            }

            //asert

            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    booking.BookingId = 55;
                });

            var result = _bookingService.BookStudyRoom(_request);

            Assert.AreEqual(expectedBookingId, result.BookingId);
        }

        [Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookNethodNotInvoked()
        {
                _availalbeStudyRoom.Clear();

            var result = _bookingService.BookStudyRoom(_request);

           _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }
}
