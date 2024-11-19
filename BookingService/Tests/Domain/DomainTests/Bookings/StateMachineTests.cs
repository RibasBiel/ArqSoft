
using Application;
using Application.Booking;
using Application.Guest;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;
using NUnit.Framework;


namespace DomainTests
{
    public class StateMachineTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        //Iniciar status como criado
         [Test]
        public async Task ShouldAwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.AreEqual(booking.CurrentStatus, Domain.Enums.Status.Created);
        }

        //Definir status como pago ao pagar por uma reserva com status criado
        [Test]
        public async Task ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
        {
            var booking = new Booking { Status = Status.Paid };
            booking.ChangeState(Domain.Enums.Action.Finish);
            Assert.AreEqual(Status.Finished, booking.Status);
        }
        
        //Definir status como concluido ao finalizar uma reserva paga
        [Test]
        public async Task ShouldSetStatusToRefoundedWhenRefoundingAPaidBooking()
        {
            var booking = new Booking{Status = Status.Paid};
            booking.ChangeState(Domain.Enums.Action.Refound);
            Assert.AreEqual(Status.Refounded,booking.Status);
            
        }

        //Definir o status como criado ao reabrir uma reserva
        [Test]
        public async Task ShouldSetStatusToCreatedWhenReopeningACanceledBooking()
        {
            var booking = new Booking { Status = Status.Canceled };
            booking.ChangeState(Domain.Enums.Action.Reopen);
            Assert.AreEqual(Status.Created, booking.Status);
            
        }
        
        //Não alterar status ao reabrir uma reserva com status criado
        [Test]
        public async Task ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking{Status = Status.Created};
            booking.ChangeState(Domain.Enums.Action.Refound);
            Assert.AreEqual(booking.Status, Status.Created);
        }
        
        //Não alterar status ao abrir uma reserva concluída
        [Test]
        public async Task ShouldNotChangeStatusWhenRefoundingAFinishedBooking()
        {

            var booking = new Booking { Status = Status.Finished };
            booking.ChangeState(Domain.Enums.Action.Refound);
            Assert.AreEqual(booking.Status, Status.Finished);
        }
        
    

    }
}