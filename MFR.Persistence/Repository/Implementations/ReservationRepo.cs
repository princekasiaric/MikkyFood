using MFR.DomainModels;

namespace MFR.Persistence.Repository.Implementations
{
    public class ReservationRepo : BaseRepo<Reservation>, IReservationRepo
    {
        public ReservationRepo(MFRDbContext context) : base(context){}

        public MFRDbContext MFRDbContext { get; set; }
    }
}
