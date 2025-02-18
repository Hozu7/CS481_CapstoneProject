using System;

namespace EventConnect;

public class VenueAdmin : VenueStaff
{
    // Venue admin fields
    Venue venue;

    List<Event> currentEvents { get; set; }

    List<Event> pastEvents { get; set; }



    // Venue admin constructors
    // New venue admin
    public VenueAdmin(string name, string email, string phone, string service, Venue venue) : base(name, email, phone, service) {
        this.venue = venue;
        currentEvents = new List<Event>();
        pastEvents = new List<Event>();
    }
    // Venue admin loaded using already created objects
    public VenueAdmin(Guid id, string name, string email, string phone, string service, Venue venue, List<Event> currentEvents, List<Event> pastEvents, List<Ticket> openTickets, List<Ticket> closedTickets) : base(id, name, email, phone, service, openTickets, closedTickets){
        this.venue = venue;
        this.currentEvents = currentEvents;
        this.pastEvents = pastEvents;
    }
    // New venue admin
    public VenueAdmin(Guid id) : base (id) {
        // Query database to initialize other fields
    }



    // Venue admin methods
    // Add staff to venue with strings
    public void addStaff(string name, string email, string phone, string service) {
        venue.staff.Add(new(name, email, phone, service));

        // add staff to database 
    }
    // Add staff to venue by querying database with GUID
    public void addStaff(Guid staffId) {
        venue.staff.Add(new VenueStaff(staffId));
    }
    // Remove staff using GUID
    public void removeStaff(Guid staffId) {
        int index = venue.staff.FindIndex(st => st.id == staffId);
        if (index != -1) {
            venue.staff.RemoveAt(index);
        }
        else {
            Console.Write("Error: Staff Does Not Exist");
        }

        // Remove staff from database
    }

    // Add a service
    public void addService(string service) {
        venue.services.Add(service);

        // Add service to database
    }
    // Remove service
    public void removeService(string service) {
        venue.services.Remove(service);

        // Remove service from database
    }

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
    public void createEvent(string title, List<DateOnly> dates, int expectAttend, List<EventSpace> occupiedSpaces) {
        Event newEvent = new Event(title, dates, expectAttend, occupiedSpaces);
        addEventToList(newEvent);

        // Add event to database
    }
}
