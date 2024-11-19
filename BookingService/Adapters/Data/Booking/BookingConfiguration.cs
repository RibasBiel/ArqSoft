
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;



namespace Data.Booking
{
    public class BookingConfiguration : IEntityTypeConfiguration<Domain.Entities.Booking>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Booking> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e=>e.PlacedAt).IsRequired();
            builder.Property(e=>e.Start).IsRequired();
            builder.Property(e=>e.End).IsRequired();
            builder.Property(e=>e.Status).IsRequired();



        }
    }
}
