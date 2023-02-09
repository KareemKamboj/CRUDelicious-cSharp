#pragma warning disable CS8618
/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
using Microsoft.EntityFrameworkCore;
namespace CRUDelicious.Models;
// the CrudContext class representing a session with our MySQL database, allowing us to query for or save data
public class CrudContext : DbContext 
{ 
    public CrudContext(DbContextOptions options) : base(options) { }
    // the "Monsters" table name will come from the DbSet property name
    public DbSet<Dish> Dishes { get; set; } 
}
