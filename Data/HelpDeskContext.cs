using IT_ELECTIVE_PREFINALS_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Data;

public class HelpDeskContext : DbContext
{
    public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options) { }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketAssignment> TicketAssignments => Set<TicketAssignment>();
    public DbSet<TicketAttachment> TicketAttachments => Set<TicketAttachment>();
    public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    public DbSet<TicketPriority> TicketPriorities => Set<TicketPriority>();
    public DbSet<TicketStatus> TicketStatuses => Set<TicketStatus>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TicketTag> TicketTags => Set<TicketTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Team>().HasIndex(x => new { x.DepartmentId, x.Name }).IsUnique();
        modelBuilder.Entity<TicketPriority>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<TicketStatus>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Tag>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<TeamMember>().HasKey(x => new { x.TeamId, x.EmployeeId });
        modelBuilder.Entity<TeamMember>()
            .HasOne(x => x.Team).WithMany(x => x.TeamMembers).HasForeignKey(x => x.TeamId);
        modelBuilder.Entity<TeamMember>()
            .HasOne(x => x.Employee).WithMany(x => x.TeamMembers).HasForeignKey(x => x.EmployeeId);

        modelBuilder.Entity<TicketAssignment>().HasKey(x => new { x.TicketId, x.EmployeeId });
        modelBuilder.Entity<TicketAssignment>()
            .HasOne(x => x.Ticket).WithMany(x => x.Assignments).HasForeignKey(x => x.TicketId);
        modelBuilder.Entity<TicketAssignment>()
            .HasOne(x => x.Employee).WithMany(x => x.TicketAssignments).HasForeignKey(x => x.EmployeeId);

        modelBuilder.Entity<TicketTag>().HasKey(x => new { x.TicketId, x.TagId });
        modelBuilder.Entity<TicketTag>()
            .HasOne(x => x.Ticket).WithMany(x => x.TicketTags).HasForeignKey(x => x.TicketId);
        modelBuilder.Entity<TicketTag>()
            .HasOne(x => x.Tag).WithMany(x => x.TicketTags).HasForeignKey(x => x.TagId);

        modelBuilder.Entity<TicketCategory>()
            .HasOne(x => x.ParentCategory)
            .WithMany(x => x.ChildCategories)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId);
        modelBuilder.Entity<Team>()
            .HasOne(x => x.Department).WithMany(x => x.Teams).HasForeignKey(x => x.DepartmentId);
        modelBuilder.Entity<Ticket>()
            .HasOne(x => x.Customer).WithMany(x => x.Tickets).HasForeignKey(x => x.CustomerId);
        modelBuilder.Entity<Ticket>()
            .HasOne(x => x.Category).WithMany(x => x.Tickets).HasForeignKey(x => x.CategoryId);
        modelBuilder.Entity<Ticket>()
            .HasOne(x => x.Priority).WithMany(x => x.Tickets).HasForeignKey(x => x.PriorityId);
        modelBuilder.Entity<Ticket>()
            .HasOne(x => x.Status).WithMany(x => x.Tickets).HasForeignKey(x => x.StatusId);
        modelBuilder.Entity<TicketComment>()
            .HasOne(x => x.Employee).WithMany(x => x.TicketComments).HasForeignKey(x => x.EmployeeId).IsRequired(false);
        modelBuilder.Entity<TicketAttachment>()
            .HasOne(x => x.Ticket).WithMany(x => x.Attachments).HasForeignKey(x => x.TicketId);
    }
}
