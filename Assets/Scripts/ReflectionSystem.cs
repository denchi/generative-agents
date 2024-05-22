using System.Collections;

public class Reflection
{
    public string Insight { get; private set; }
    public List<Memory> Evidence { get; private set; }

    public Reflection(string insight, List<Memory> evidence)
    {
        Insight = insight;
        Evidence = evidence;
    }
}

public class ReflectionSystem
{
    public List<Reflection> GenerateReflections(MemoryStream memoryStream)
    {
        var recentMemories = memoryStream.RetrieveRelevantMemories("recent");
        var reflections = new List<Reflection>(;

        // Generate questions and insights (stubbed for now)
        var insight = "Harry is dedicated to his work because of [evidence].";
        var reflection = new Reflection(insight, recentMemories);
        reflections.Add(reflection);

        return reflections;
    }
}
