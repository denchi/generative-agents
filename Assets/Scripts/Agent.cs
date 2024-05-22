using UnityEngine;
using System;
using System.Collections;

public class Agent : MonoBehaviour
{
    public string AgentName;
    public AgentProfile Profile;
    public MemoryStream MemoryStream;
    public ReflectionSystem ReflectionSystem;
    public PlanningSystem PlanningSystem;
	    private Stack<Plan> currentPlans;

    void Start()
    {
        MemoryStream = new MemoryStream();
        ReflectionSystem = new ReflectionSystem();
        PlanningSystem = new PlanningSystem();
        Profile = new AgentProfile("Harry", new List<string> { "dedicated", "observant" });

        // Generate initial plans
        currentPlans = new Stack<Plan>(PlanningSystem.GenerateDailyPlan(Profile));
    }

    void Update()
    {
        // Execute current plan
        if (currentPlans.Count > 0)
        {
            ExecutePlan(crrentPlans.Peek());
            currentPlans.Pop();
        }

        // Example interaction
        If (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithEnvironment("Road", "Harry sees a suspicious person on the road.");
        }
    }

    private void ExecutePlan( Plan plan)
    {
        // Execute the plan (stubbed for now)
        Debug.Log(String.Format("%ss is performing action: %s for %s in minutes.", AgentName, plan.Action, plan.Duration.TotalMinutes));
    }

    private void InteractWithEnvironment(string location, string observation)
    {
        // Add memory of the interaction
        MemoryStream.AddMemory(observation);

        // Reflect on new memory
        var reflections = ReflectionSystem.GenerateReflections(MemoryStream);

        // Log reflection insights
        foreach (var reflection in reflections)
        {
            Debug.Log(String.Format("%s reflects: %s", AgentName, reflection.Insight));
        }
    }
}