using System;

namespace EventConnect;

public class EventSpace
{
    // Event space fields
    public Guid id { get; set; }
    
    public string name { get; set; }

    public int size { get; set; }

    public int ceilings { get; set; }

    Dictionary<string, int> setups { get; }


    // Event space constructors
    // New event space
    public EventSpace(string name, int size, int ceilings)
    {
        this.name = name;
        this.size = size;
        this.ceilings = ceilings;
        setups = new Dictionary<string, int>();

        // Create account in database and return GUID
        id = Guid.NewGuid();
        this.ceilings = ceilings;
    }
    // Event space loaded using already created objects
    public EventSpace(Guid id, string name, int size, int ceilings, Dictionary<string, int> setups) {
        this.id = id;
        this.name = name;
        this.size = size;
        this.ceilings = ceilings;
        this.setups = setups;
    }
    // Event space loaded from database using GUID
    public EventSpace(Guid id) {
        this.id = id;
        // Query database to initialize other fields
    }



    // Event space methods
    // Add setup to dictionary using name as key and the capacity
    public void addSetup(string name, int capacity) {
        setups.Add(name, capacity);
    }
}
