using System;

namespace EventConnect;

public abstract class Account
{
    // Account fields
    public Guid id { get; set; }

    public string name { get; set; }
    
    public string email { get; set; }

    public string phone { get; set; }

    public List<Ticket> openTickets { get; set; }

    public List<Ticket> closedTickets { get; set; }


    // Account constructors
    // New account
    public Account(string name, string email, string phone) {
        this.name = name;
        this.email = email;
        this.phone = phone;
        openTickets = new List<Ticket>();
        closedTickets = new List<Ticket>();

        // Create account in database and return GUID
        id = Guid.NewGuid();
    }
    // Account loaded from already created objects
    public Account(Guid id, string name, string email, string phone, List<Ticket> openTickets, List<Ticket> closedTickets) {
        this.id = id;
        this.name = name;
        this.email = email;
        this.phone = phone;
        this.openTickets = openTickets;
        this.closedTickets = closedTickets;
    }
    // Account loaded from database using Account GUID and querying the database
    public Account(Guid id) {
        this.id = id;
        // Query database to initialize other fields
    }
}

