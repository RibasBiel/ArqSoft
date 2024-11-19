using Domain.Guest.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;
using Shared;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname {get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(DocumentId.IdNumber) ||
                DocumentId.IdNumber.Length <=3 ||
                DocumentId.DocumentType ==0 )
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (string.IsNullOrEmpty(Name) || 
                string.IsNullOrEmpty(Surname) || 
                string.IsNullOrEmpty(Email))
            {
                throw new MissingRequiredInformation();
            }

            if (Utils.ValidateEmail(this.Email) == false)
            {
                throw new InvalidEmailException();
            }

        }

        public async Task Save(IGuestRepository guestRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            }
            else
            {
                //await
            }
        }

        

        public async Task Update(IGuestRepository guestRepository)
        {
            

            if (this.Id == 0)
            {
                throw new InvalidOperationException("Cannot update a schedule that has not been created.");
            }
            var guest = await guestRepository.Get(this.Id);
            if (guest == null) {
                throw new InvalidOperationException("Guest not found for update.");
            }
            
            guest.Name = this.Name;
            guest.Surname = this.Surname;
            guest.Email = this.Email;
            guest.DocumentId = this.DocumentId;
            await guestRepository.Update(guest);
        }

        public async Task Delete(IGuestRepository guestRepository)
        {


            if (this.Id == 0)
            {
                throw new InvalidOperationException("Cannot update a schedule that has not been created.");
            }
            var guest = await guestRepository.Get(this.Id);
            if (guest == null)
            {
                throw new InvalidOperationException("Guest not found for update.");
            }
            await guestRepository.Delete(guest);
            
        }

    }
}
