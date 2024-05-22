using System;

public class Plan
{
    public DateTime StartTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public string Action { get; private set; }

    public Plan(DateTime startTime, TimeSpan duration, string action)
    {
        StartTime = startTime;
        Duration = duration;
        Action = action;
    }
}

public class PlanningSystem
{    public List<Plan> GenerateDailyPlan(AgentProfile agentProfile)
    {
        var plans = new List<Plan>(;
        var currentTime = DateTime.Now;
        // Example of a rough daily plan
        plans.Add(new Plan(currentTime.AddHours(1), TimeSpan.FromHours(1), "Work on a case"));
        plans.Add((new Plan(currentTime.AddHours(2), TimeSpan.FromHours(1), "Patrol the road"));
        plans.Add(new Plan(currentTime.AddHours(3), TimeSpan.FromHours(1), "Visit the police station"));

        return plans;
    }
}

public class AgentProfile
{
    public string Name { get; private set; }
    public List<string> Traits { get; private set; }
    public AgentProfile(string name, List<string> traits)
    {
        Name = name;
        Traits = traits;
    }
}
