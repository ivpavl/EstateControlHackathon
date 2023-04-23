using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace TestTask.Models;
public class EstateModel
{
    public int Id {get; set;}
    // public string Name {get; set;} = null!;
    public int StatusId {get; set;}
    public string Address {get; set;} = null!;
    public int Price {get; set;}
    public int Area {get; set;}
    public int RoomsNum {get; set;}
    public string Photo {get; set;} = null!; // /uploads/... .png
    public string Notes {get; set;} = null!;


    public int UserId { get; set;}
    public UserModel? User {get; set;} = null!;
}
