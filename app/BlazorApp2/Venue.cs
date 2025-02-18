using System;

namespace EventConnect;

public class Venue
{
    // Venue fields
    public Guid id { get; set; }

    public string address { get; set; }

    public string phone { get; set; }

    public VenueAdmin venueAdmin { get; set; }

    public string website { get; set; }
    
    public int totalSpace { get; set; }

    public List<EventSpace> availableSpaces { get; set; }

    public List<EventSpace> occupiedSpaces { get; set; }

    public List<string> services { get; set; }

    public List<VenueStaff> staff { get; set; }
    


    // Venue constructors
    // New venue
    public Venue(string address, string phone, VenueAdmin venueAdmin, string website, int totalSpace, List<EventSpace> availableSpaces, List<string> services) {
        this.address = address;
        this.phone = phone;
        this.venueAdmin = venueAdmin;
        this.website = website;
        this.totalSpace = totalSpace;
        this.availableSpaces = availableSpaces;
        occupiedSpaces = new List<EventSpace>();
        this.services = services;

        // Create account in database and return GUID
        id = Guid.NewGuid();
    }
    // Venue loaded using already created objects
    public Venue(Guid id, string address, string phone, VenueAdmin venueAdmin, string website, int totalSpace,
     List<EventSpace> availableSpaces, List<EventSpace> occupiedSpaces, List<string> services) {
        this.id = id;
        this.address = address;
        this.phone = phone;
        this.venueAdmin = venueAdmin;
        this.website = website;
        this.totalSpace = totalSpace;
        this.availableSpaces = availableSpaces;
        this.occupiedSpaces = occupiedSpaces;
        this.services = services;
    }
    // Venue loaded from database using GUID
    public Venue(Guid id) {
        this.id = id;
        // Query database to initialize other fields
    }
}
