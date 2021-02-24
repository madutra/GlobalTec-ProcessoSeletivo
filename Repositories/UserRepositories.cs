using System.Collections.Generic;
using System.Linq;
using GlobalTec.Models;

namespace GlobalTec.Repositories
{
  public static class UserRepository
  {
    public static User Get(string username, string password)
    {
      var users = new List<User>();
      users.Add(new User
      {
        Id = 1,
        UserCode = "001",
        Name = "Matheus Andrade",
        CPF = "02816314170",
        UF = "GO",
        BirthDate = "02/08/1997",
        UserName = "madutra",
        Password = "123456",
        Role = "manager"
      });
      users.Add(new User {
        Id = 2,
        UserCode = "002",
        Name = "teste2",
        CPF = "0000000000",
        UF = "GO",
        BirthDate = "01/01/2020",
        UserName = "teste2",
        Password = "123456",
        Role = "employee"
      });
      return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
    }
  }
}