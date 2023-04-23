using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;


namespace TestTask.Models;
public class LoginUserModel
{
    public string Name {get; set;} = null!;
    public string Password {get; set;} = null!;

}
