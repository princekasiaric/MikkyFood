using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository.Implementations
{
    public class ReservationRepo : BaseRepo<Reservation>, IReservationRepo
    {
        public ReservationRepo(MFRDbContext context) : base(context){}

        public async Task AddReservationAsync(Reservation reservation) 
            => await Add(reservation);

        public MFRDbContext MFRDbContext => _context as MFRDbContext;
    }
}
