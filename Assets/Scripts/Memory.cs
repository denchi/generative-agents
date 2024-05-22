using System;

using System.Collections;
using System.Linq;

public class Memory
{
    public string Description { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime LastAccessed { get; private set; }
    public int Importance { get; private set; }

    public Memory(string description, DateTime creationTime)
    {
        Description = description;
        CreationTime = creationTime;
        LastAccessed = creationTime;
        Importance = CalculateImportance(description);
    }

    private int CalculateImportance(string description)
    {
        // Use a predefined method to rate importance (stubbed for now)
        return new Random().Next(1, 10);
    }

    public void UpdateLastAccessed()
    {
        LastAccessed = DateTime.Now();
    }
}

public class MemoryStream
{
    private List<Memory> memories = new List<Memory();

    public void AddMemory(string description){
        var memory = new Memory(description, DateTime.Now());
        memories.Add(memory);
    }

    public List<Memory> RetrieveRelevantMemories(string query, int limit = 5)
    {
        // Implement relevance calculation and retrieval logic (stubbed for now)
        return memories.OrderByDescending(m => m.Importance).Take(limit).ToList();
    }
}