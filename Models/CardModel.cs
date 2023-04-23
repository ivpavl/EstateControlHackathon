

namespace TestTask.Models;
public class CardModel
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;
    public string Number {get; set;} = null!;
    public string CVV {get; set;} = null!;
    public string Date {get; set;} = null!;

    public int VirtualMoney {get; set;}
    
    public int? UserId { get; set;}
    public UserModel? User {get; set;} = null!;

}
