using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services;

public class ProduktService : IGetDataInterface , IFormSubmit
{
    private static List<Produkt> _produkty = new List<Produkt>()
    {
        new Produkt( 1, "Chleb",  4.50m,  DateTime.Now.AddDays(3)),
        new Produkt( 2, "Makaron",  6.40m,  DateTime.Now.AddDays(3)),
        new Produkt( 3, "Mleko",  2.50m,  DateTime.Now.AddDays(3)),

    };


    public IEnumerable<Produkt> Get(string? filtr = null, int? page = null, int pageSize = 5)
    {
        var query = _produkty.AsQueryable();
        if (!string.IsNullOrEmpty(filtr))
            query = query.Where(p => p.Nazwa.Contains(filtr, StringComparison.OrdinalIgnoreCase));
        if(page.HasValue)
            query = query.Skip((page.Value - 1) * pageSize).Take(pageSize);
            
        return query.ToList();
    }

    public Produkt GetById(int id)
    {
        return _produkty.FirstOrDefault(p => p.Id == id);
        
    }
    public static List<Produkt> Produkty => _produkty;
    public bool Post(string nazwa,decimal kwota, DateTime datum)
    {
        int newId = _produkty.Any() ? _produkty.Max(p => p.Id) + 1 : 1;
        _produkty.Add(new Produkt(newId, nazwa, kwota, datum));
        return true;
    }

    public bool Put(int id,string nazwa,decimal kwota, DateTime datum)
    {
        try
        {
            var index = _produkty.FindIndex(p => p.Id == id);
            if (index == -1) return false;
        
            _produkty[index] = new Produkt(id, nazwa, kwota, datum); 
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

    public bool Delete(int id)
    {
        try
        {
            var produkt = _produkty.FirstOrDefault(p => p.Id == id);
            if (produkt == null) return false;
            _produkty.Remove(produkt);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}