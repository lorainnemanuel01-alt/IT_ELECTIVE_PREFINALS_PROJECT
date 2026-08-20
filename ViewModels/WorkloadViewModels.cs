namespace IT_ELECTIVE_PREFINALS_PROJECT.ViewModels;

public class EmployeeWorkloadViewModel
{
    public string Employee { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int UnresolvedTicketCount { get; set; }
}

public class DepartmentWorkloadViewModel
{
    public string Department { get; set; } = string.Empty;
    public int EmployeeCount { get; set; }
    public int UnresolvedTicketCount { get; set; }
}

public class UnassignedTicketViewModel
{
    public int TicketId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class MultipleAssigneeViewModel
{
    public int TicketId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int NumberOfActiveAssignees { get; set; }
    public string Assignees { get; set; } = string.Empty;
}

public class PrimaryAssigneeViewModel
{
    public int TicketId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string PrimaryAssignee { get; set; } = "Unassigned";
}

public class CategoryHierarchyViewModel
{
    public string Category { get; set; } = string.Empty;
    public string ParentCategory { get; set; } = "—";
}
