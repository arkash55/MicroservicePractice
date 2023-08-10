using Microsoft.EntityFrameworkCore;
namespace GradesApi.Models; 

public class GradeContext: DbContext {


    public GradeContext(DbContextOptions<GradeContext> options): base(options) 
    {   
        
    }




    public DbSet<GradeItem> GradeItems { get; set;}

}


// public class GradeContext: DbContext {

//     protected readonly IConfiguration Configuration;

//     public GradeContext(DbContextOptions<GradeContext> options): base(options) 
//     { 
//         Configuration = Configuration;
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder options)
//     {
//         // connect to postgres with connection string from app settings
//         options.UseNpgsql(Configuration.GetConnectionString("DATABASE_URL"));
//     }


//     public DbSet<GradeItem> GradeItems { get; set;}

// }




