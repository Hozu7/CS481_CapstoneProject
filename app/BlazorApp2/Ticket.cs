using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventConnect;

public enum TicketStatus {
    [Description("Unresolved")]
    Unresolved,
    [Description("In Progress")]
    InProgress,
    [Description("Resolved")]
    Resolved
};
public enum TicketPriority {
    [Description("Low")]
    Low,
    [Description("Medium")]
    Medium,
    [Description("High")]
    High
}

public class Ticket{
    // Ticket fields
    public Guid id { get; set; }

    public Guid eventId { get; set; }

    [Required]
    public string service { get; set; }

    public TicketPriority priority { get; set; } = TicketPriority.Low;

    public TicketStatus status { get; set; } = TicketStatus.Unresolved;

    [StringLength(150, ErrorMessage = "Description too long (150 character limit).")]
    public string description { get; set; }

    public DateTime dateSubmitted { get; set; }

    public DateTime? dateResolved { get; set; }

    public Guid ticketOwner { get; set; }


    // Ticket constructors
    // New ticket
    public Ticket(Guid eventId, string service, TicketPriority priority, TicketStatus status, string description, Guid ticketOwner) {
        this.eventId = eventId;
        this.service = service;
        this.priority = priority;
        this.status = status;
        this.description = description;
        dateSubmitted = DateTime.Now;
        this.ticketOwner = ticketOwner;

        // Create ticket in database and return GUID
        id = Guid.NewGuid();
    }
    // Ticket loaded using already created objects
    public Ticket(Guid id, Guid eventId, string service, TicketPriority priority, TicketStatus status, string description, DateTime dateSubmitted, DateTime dateResolved, Guid ticketOwner) {
        this.id = id;
        this.service = service;
        this.priority = priority;
        this.status = status;
        this.description = description;
        this.dateSubmitted = dateSubmitted;
        this.dateResolved = dateResolved;
        this.ticketOwner = ticketOwner;
    }
    // Ticket loaded from database using GUID
    public Ticket(Guid id) {
        this.id = id;
        // Query database to initialize other fields
    }
}