using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;


namespace TestTask.Models;
[Index("Name", IsUnique = true)]
public class UserModel
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;
    public string Password {get; set;} = null!;
    
    public ICollection<EstateModel>? Estates { get; set; } = null!;
    public CardModel? Card { get; set; } = null!;
    public ICollection<SubscribedUser>? Subscribers {get; set;} = null!;
}
