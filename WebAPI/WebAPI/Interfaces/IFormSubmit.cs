using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface IFormSubmit
{
    bool Post(string nazwa,decimal kwota, DateTime datum);
    bool Put(int id,string nazwa,decimal kwota, DateTime datum);
    bool Delete(int id);
}