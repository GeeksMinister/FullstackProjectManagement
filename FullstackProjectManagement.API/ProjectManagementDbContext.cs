public class ProjectManagementDbContext : DbContext
{
#pragma warning disable CS8618
    public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options)
    {

    }

    public DbSet<Employee> Employee { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<Todo> Todo { get; set; }
    public DbSet<UserStory> UserStory { get; set; }

}
