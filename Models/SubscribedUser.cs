

namespace TestTask.Models;
public class SubscribedUser
{
    public int Id {get; set;}
    public int SubscriberId {get; set;}
    
    public int UserId { get; set;}
    public UserModel? User {get; set;} = null!;

}
