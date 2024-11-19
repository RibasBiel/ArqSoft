using Domain.Booking.Exceptions;
using Domain.Enums;
using Domain.Ports;
using Shared;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Action = Domain.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            Status = Status.Created;
            PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }

        public Guest Guest { get; set; }

        public Status Status { get; set; }
        public Status CurrentStatus {
            get { return this.Status; }set { this.Status = value; }
        }

        //Máquina de estado - POO
        public void ChangeState(Action action){
            
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,

                _=> Status
            };
        }

        private void ValidateState()
        {
            

            if (PlacedAt ==DateTime.MinValue ||
                Start ==DateTime.MinValue ||
                End == DateTime.MinValue)
            {
                throw new MissingRequiredInformation();
            }

            

        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await bookingRepository.Create(this);
            }
            else
            {
                //await
            }
        }

        public async Task Update(IBookingRepository bookingRepository)
        {


            if (this.Id == 0)
            {
                throw new InvalidOperationException("Cannot update a schedule that has not been created.");
            }
            var booking = await bookingRepository.Get(this.Id);
            if (booking == null)
            {
                throw new InvalidOperationException("Guest not found for update.");
            }

            booking.PlacedAt = this.PlacedAt;
            booking.Start = this.Start;
            booking.End = this.End;
            
            await bookingRepository.Update(booking);
        }

        public async Task Delete(IBookingRepository bookingRepository)
        {


            if (this.Id == 0)
            {
                throw new InvalidOperationException("Cannot update a schedule that has not been created.");
            }
            var guest = await bookingRepository.Get(this.Id);
            if (guest == null)
            {
                throw new InvalidOperationException("Guest not found for update.");
            }
            await bookingRepository.Delete(guest);

        }
    }
}
