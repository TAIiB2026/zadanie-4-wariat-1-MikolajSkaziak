using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Services;
namespace WebAPI.Controllers;
[ApiController]
[Route("api/Produkty")]
public class ProduktController :ControllerBase
{
    private readonly IGetDataInterface _getDataS;
    private readonly IFormSubmit _formSubmit;
    public ProduktController(IGetDataInterface getDataInterface, IFormSubmit formSubmit)
    {
        _getDataS = getDataInterface;
        _formSubmit = formSubmit;
    }
    [HttpGet]
     public ActionResult<IEnumerable<Produkt>> Get([FromQuery] string? filtr, [FromQuery] int? page, int pageSize = 5)
     {
         var produkty = _getDataS.Get(filtr, page, pageSize);
         return Ok(produkty);
     }

    [HttpGet("{id}")]
    public ActionResult<Produkt> Get(int id)
    {
        var produkt = _getDataS.GetById(id);
        if(produkt==null)
        {
            return NotFound();
        }
        return Ok(produkt);
    }

    [HttpPost]
    public ActionResult<Produkt> Post([FromBody] ProduktDto produkt)
    {
        var result = _formSubmit.Post(produkt.Nazwa,produkt.Cena,produkt.DataWaznosci);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<Produkt> Put(int id, [FromBody] ProduktDto produkt)
    {
        var resault= _formSubmit.Put(id,produkt.Nazwa,produkt.Cena,produkt.DataWaznosci);
        return Ok(resault);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var resault = _formSubmit.Delete(id);
        return Ok(resault);
    }
}