using System;

namespace EventConnect;

public class EventSpace
{
    // Event space fields
    public Guid id { get; set; }
    
    public string name { get; set; }

    public int size { get; set; }

    public int capacity { get; set; }



    // Event space constructors
    // New event space
    public EventSpace(string name, int size, int capacity) {
        this.name = name;
        this.size = size;
        this.capacity = capacity;

        // Create account in database and return GUID
        id = Guid.NewGuid();
    }
    // Event space loaded using already created objects
    public EventSpace(Guid id, string name, int size, int capacity) {
        this.id = id;
        this.name = name;
        this.size = size;
        this.capacity = capacity;
    }
    // Event space loaded from database using GUID
    public EventSpace(Guid id) {
        this.id = id;
        // Query database to initialize other fields
    }
}
