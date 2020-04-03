using MFR.DomainModels;
using System.Threading.Tasks;

namespace MFR.Persistence.Repository
{
    public interface IReservationRepo 
    {
        Task AddReservationAsync(Reservation reservation);
    }
}
