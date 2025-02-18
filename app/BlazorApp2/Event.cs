using System;

namespace EventConnect;

public class Event
{
    // Event fields
    public Guid id { get; set; }

    public string title { get; set; }

    public List<DateOnly> dates { get; set; }

    public int expectAttend { get; set; }

    public List<EventSpace> occupiedSpaces { get; set; }

    public List<Ticket> openTickets { get; set; }

    public List<Ticket> closedTickets { get; set; }



    // Event constructor
    // New Event
    public Event(string title, List<DateOnly> dates, int expectAttend, List<EventSpace> occupiedSpaces) {
        this.title = title;
        this.dates = dates;
        this.expectAttend = expectAttend;
        this.occupiedSpaces = occupiedSpaces;
        openTickets = new List<Ticket>();
        closedTickets = new List<Ticket>();

        // Create account in database and return GUID
        id = Guid.NewGuid();
    }
    // Event loaded from database using already created objects
    public Event(Guid id, string title, List<DateOnly> dates, int expectAttend, List<EventSpace> occupiedSpaces, List<Ticket> openTickets, List<Ticket> closedTickets) {
        this.id = id;
        this.title = title;
        this.dates = dates;
        this.expectAttend = expectAttend;
        this.occupiedSpaces = occupiedSpaces;
        this.openTickets = openTickets;
        this.closedTickets = closedTickets;
    }
    // Event loaded from database using Event GUID and querying database
    public Event(Guid id) {
        this.id = id;
        // Query database to initialize other fields
    }



    // Event methods
    // Return if the event is currently ongoing (true) or in the past (false)
    public bool isCurrent() {
        return true;
    }
}
