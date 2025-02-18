using System;

namespace EventConnect;

public class VenueStaff : Account
{
    // Venue staff fields
    public string service { get; set; }



    // Venue staff constructors
    // New venue staff
    public VenueStaff(string name, string email, string phone, string service) : base(name, email, phone) {
        this.service = service;
    }
    // Load venue staff using already created objects
    public VenueStaff(Guid id, string name, string email, string phone, string service, List<Ticket> openTickets, List<Ticket> closedTickets) : base(id, name, email, phone, openTickets, closedTickets) {
        this.service = service;
    }
    // Load venue staff from database using GUID
    public VenueStaff(Guid id) : base(id) {
        // Query database to initialize other fields
    }



    // Venue staff methods
    // Update ticket status on ticket object
    public void updateTicketStatus(Ticket ticket, TicketStatus status) {
        ticket.status = status;

        // Update ticket in database
    }
    // Update ticket status from database using ticketId and eventId (preferred)
    public void updateTicketStatus(Guid ticketId, Guid eventId, TicketStatus status) {

    }
    // Update ticket status using only ticketId
    public void updateTicketStatus(Guid ticketId, TicketStatus status) {

    }
    
}
