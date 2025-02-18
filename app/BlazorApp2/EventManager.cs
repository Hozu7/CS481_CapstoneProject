using System;

namespace EventConnect;

public class EventManager : Account
{
    //Event manager fields
    List<Event> currentEvents { get; set; }

    List<Event> pastEvents { get; set; }



    // New Event Manager
    public EventManager(string name, string email, string phone) : base(name, email, phone) {
        currentEvents = new List<Event>();
        pastEvents = new List<Event>();
    }
    // Event Manager loaded using already created objects
    public EventManager(Guid id, string name, string email, string phone, List<Event> currentEvents, List<Event> pastEvents, List<Ticket> openTickets, List<Ticket> closedTickets) : base(id, name, email, phone, openTickets, closedTickets) {
        this.currentEvents = currentEvents;
        this.pastEvents = pastEvents;
    }
    // Event Manager loaded from database using Account GUID and querying the database
    public EventManager(Guid id) : base(id) {
        // Query database to initialize other fields
    }



    // Event manager methods
    // Add past event to list using Event GUID and querying the database
    public void addEventToList(Guid eventId) {
        // Create Event object from database
    }
    // Add past event to list using event object
    public void addEventToList(Event newEvent) {
        if (newEvent.isCurrent()) {
            currentEvents.Add(newEvent);
        }
        else {
            pastEvents.Add(newEvent);
        }
    }
    // Create a ticket and add to open tickets of current event
    public void createTicket(string service, TicketPriority priority, string description, Event currentEvent) {
        Ticket newTicket = new Ticket(currentEvent.id, service, priority, TicketStatus.Unresolved, description, id);
        currentEvent.openTickets.Add(newTicket);

        // Add fields of newTicket in database
    }
}
