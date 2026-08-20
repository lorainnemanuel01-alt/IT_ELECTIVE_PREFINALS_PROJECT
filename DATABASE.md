# Database Investigation

Source: supplied `lycevm.db`. The database is treated as the source of truth. No migrations, seed data, scaffolding, or schema changes are used.

## Tables and keys

| Table | Primary key | Foreign keys / notes |
|---|---|---|
| Departments | Id | Name unique |
| Employees | Id | DepartmentId -> Departments.Id; Email unique |
| Customers | Id | None |
| Teams | Id | DepartmentId -> Departments.Id; (DepartmentId, Name) unique |
| TeamMembers | (TeamId, EmployeeId) | TeamId -> Teams.Id; EmployeeId -> Employees.Id |
| Tickets | Id | CustomerId, CategoryId, PriorityId, StatusId |
| TicketAssignments | (TicketId, EmployeeId) | TicketId -> Tickets.Id; EmployeeId -> Employees.Id |
| TicketAttachments | Id | TicketId -> Tickets.Id |
| TicketCategories | Id | nullable ParentCategoryId -> TicketCategories.Id (self-reference) |
| TicketComments | Id | TicketId -> Tickets.Id; nullable EmployeeId -> Employees.Id |
| TicketPriorities | Id | Name unique |
| TicketStatuses | Id | Name unique |
| Tags | Id | Name unique |
| TicketTags | (TicketId, TagId) | TicketId -> Tickets.Id; TagId -> Tags.Id |

## Relationships

- Department 1-to-many Employees.
- Department 1-to-many Teams.
- Team and Employee are many-to-many through TeamMembers.
- Customer 1-to-many Tickets.
- TicketCategory 1-to-many Tickets.
- TicketCategory has a self-referencing one-to-many hierarchy through nullable ParentCategoryId.
- TicketPriority 1-to-many Tickets.
- TicketStatus 1-to-many Tickets.
- Ticket 1-to-many TicketAssignments.
- Employee 1-to-many TicketAssignments.
- Ticket 1-to-many TicketComments.
- Employee optionally 1-to-many TicketComments.
- Ticket 1-to-many TicketAttachments.
- Ticket and Tag are many-to-many through TicketTags.

## Nullable columns discovered

- Departments.Description
- Customers.Phone
- Team.Description
- TicketAssignments.UnassignedAt
- Ticket.DueAt
- Ticket.ResolvedAt
- Ticket.ClosedAt
- TicketCategories.ParentCategoryId
- TicketComments.EmployeeId

Date/time values are stored in SQLite TEXT columns in the supplied database. The model maps them to `DateTime`/`DateTime?` because the supplied values use ISO-style date/time representations.

## Composite primary keys

- TeamMembers: TeamId + EmployeeId
- TicketAssignments: TicketId + EmployeeId
- TicketTags: TicketId + TagId

## Required application queries

The application includes Employee Workload, Department Workload, Unassigned Tickets, Multiple-Assignee Tickets, Primary Assignee, and Category Hierarchy queries using LINQ over EF Core relationships.
