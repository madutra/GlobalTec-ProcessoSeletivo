using Microsoft.EntityFrameworkCore;
using GlobalTec.Models;

namespace GlobalTec.Data {
  public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options)
      :base(options) {

      }
      public DbSet<User> User { get; set; }
  }

}