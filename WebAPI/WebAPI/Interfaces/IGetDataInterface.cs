using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface IGetDataInterface
{
    IEnumerable<Produkt> Get(string? filtr = null, int? page = null, int pageSize = 5);
    Produkt GetById(int id);
}