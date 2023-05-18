using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace TestTask.Models;
public class NewEstateModel
{
    public string Address {get; set;} = null!;
    public int RoomsNum {get; set;}
    public int Area {get; set;}
    public int Price {get; set;}

    public int StatusId {get; set;}

    public IFormFile File{ get; set; } = null!;
    public string? Notes {get; set;} = null!;
    public int UserId { get; set;}


}
