using Place.Domain.Models;

namespace Place.Domain.Interface;
public interface IReservationDal
{
    Task<long> Add(Reservation entity);
    Task<List<Reservation>?> GetAll();
}