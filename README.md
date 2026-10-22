# Generative Agents

A basic Unity project to implement generative agents with memory, reflection, and planning capabilities for 2D side-scroller games.

This repository is an early prototype of that goal. It contains four C# scripts under `Assets/Scripts`. There is no Unity project, no scene, and no side-scroller gameplay. The scripts sketch a memory stream, a reflection step, and a daily plan for one sample character. As committed, they do not compile.

## Goal

The project aims to give characters in a 2D side-scroller a generative-agent loop: they would record what they observe, reflect on those memories, and follow a plan. That aim is the repository description. The code that exists is a class sketch of the loop, filled with placeholder data for an agent named Harry.

## What is in the repository

| Path | Types | What the file contains |
| --- | --- | --- |
| `Assets/Scripts/Memory.cs` | `Memory`, `MemoryStream` | A memory record and a list that stores and returns memories. |
| `Assets/Scripts/ReflectionSystem.cs` | `Reflection`, `ReflectionSystem` | A reflection record and a method that returns one fixed insight. |
| `Assets/Scripts/PlanningSystem.cs` | `Plan`, `PlanningSystem`, `AgentProfile` | A profile, a plan record, and a method that returns three fixed plans. |
| `Assets/Scripts/Agent.cs` | `Agent` | A Unity `MonoBehaviour` that constructs the other types and calls them. |

## What the code does today

### Memory

`Memory` stores a description, a creation time, a last-accessed time, and an importance score. `CalculateImportance` is marked stubbed and returns `new Random().Next(1, 10)`, so the score is a random integer from 1 through 9. `UpdateLastAccessed` sets the last-accessed time to `DateTime.Now`. Nothing in the repository calls it.

`MemoryStream` keeps the memories in a `List<Memory>`. `AddMemory` appends a `Memory` created with `DateTime.Now`. `RetrieveRelevantMemories(query, limit = 5)` ignores `query` and returns up to `limit` memories ordered by importance, highest first. The method comment marks the relevance logic as stubbed.

### Reflection

`Reflection` stores an insight string and a list of memories as evidence.

`ReflectionSystem.GenerateReflections` loads memories with the query `"recent"` (which retrieval ignores) and returns one reflection. The insight is the fixed string `Harry is dedicated to his work because of [evidence].` The retrieved memories are attached as evidence and are not used to write the insight. The method comment marks question and insight generation as stubbed.

### Planning

`AgentProfile` stores a name and a list of trait strings.

`Plan` stores a start time, a duration, and an action string.

`PlanningSystem.GenerateDailyPlan` takes an `AgentProfile` and does not read it. It returns three one-hour plans starting one, two, and three hours after `DateTime.Now`:

- Work on a case
- Patrol the road
- Visit the police station

### Agent

`Agent` exposes `AgentName`, `Profile`, `MemoryStream`, `ReflectionSystem`, and `PlanningSystem`, and keeps the current plans in a private `Stack<Plan>`.

`Start` constructs the three systems, sets the profile to the name `Harry` with the traits `dedicated` and `observant`, and fills a `Stack<Plan>` from the list `GenerateDailyPlan` returns. It does not assign `AgentName`.

`Update` is written to take the plan on top of the stack, pass it to `ExecutePlan`, and pop it, so each frame would consume one plan. Plan duration is not used as a schedule. `ExecutePlan` is marked stubbed and only calls `Debug.Log`. Its format string is `"%ss is performing action: %s for %s in minutes."`. `string.Format` substitutes `{0}` placeholders, so that call would not insert the name, action, or duration.

`Update` is also written to call `InteractWithEnvironment` when Space is pressed, with the location `"Road"` and the observation `"Harry sees a suspicious person on the road."` The location parameter is unused. The method stores the observation, generates reflections, and logs each insight with `Debug.Log`.

## Implemented so far, and what is still planned

The prototype has these pieces, each with placeholder behaviour:

- An in-memory list of text observations, with creation time, last-accessed time, and a random importance score
- Retrieval of the highest-importance memories, up to a limit
- A reflection type, and a method that returns one hardcoded insight
- A profile (name and traits), a plan type, and a daily plan of three hardcoded actions
- A Unity component that builds the Harry profile, stacks that plan, and defines a Space-key observation

Still planned, and absent from the tree:

- A Unity project, scene, sprite, animation, physics setup, or 2D side-scroller
- A language model, prompts, or any scoring beyond the random importance integer
- Retrieval that uses the query, recency, or relevance; last-accessed time is never updated
- A reflection whose text is produced from the evidence memories
- Plans chosen from the agent profile, broken into smaller steps, or executed on their `StartTime` and `Duration`
- Plan execution other than a log line
- More than one agent, conversation, or an environment the agent can perceive

Comments in `Memory.CalculateImportance`, `MemoryStream.RetrieveRelevantMemories`, `ReflectionSystem.GenerateReflections`, and `Agent.ExecutePlan` already describe those methods as stubbed.

## Opening and running

Cloning this repository does not give Unity a project to open. `ProjectSettings/`, `Packages/`, scenes, and `.meta` files are not in the tree. `Assets/` contains only `Scripts/`.

To try the scripts, create a new 2D project in Unity Hub and copy `Assets/Scripts/` into that project's `Assets` folder. This repository does not pin an editor version; see [Unity version](#unity-version).

Play mode is unavailable against the files as they are committed. The sources contain these errors:

- `using System.Collections.Generic` is missing wherever `List<T>` or `Stack<T>` is used (`Agent.cs`, `Memory.cs`, `PlanningSystem.cs`, `ReflectionSystem.cs`).
- Three constructions are incomplete: `new List<Memory()`, `new List<Plan>(`, and `new List<Reflection>(`.
- `PlanningSystem.GenerateDailyPlan` has an extra opening parenthesis on the `"Patrol the road"` plan.
- In `Agent.Update`, the conditional is written `If` and the stack is read as `crrentPlans`.
- `Agent.ExecutePlan` passes a `%s` format string to `string.Format`.

`Agent` calls the legacy Input Manager (`Input.GetKeyDown`). In a project whose Active Input Handling is set to the Input System package only, that call does not run. Include the Input Manager for the Space path.

After a local copy compiles, create an empty GameObject, add the `Agent` component, and enter Play mode. `Start` builds Harry's profile and the three-plan stack. Each frame logs one plan and removes it. Pressing Space stores the sample road observation and logs the stub reflection.

## Unity version

The Unity version cannot be determined from this repository. There is no `ProjectSettings/ProjectVersion.txt`, no `Packages/manifest.json`, and no `.meta` files. The only Unity types in the sources are `MonoBehaviour`, `Debug`, `Input`, and `KeyCode` in `Agent.cs`. Those APIs span many editor releases and do not identify one.

## Inspiration

The split into a memory stream, reflection, and planning follows:

Park, Joon Sung, Joseph C. O'Brien, Carrie J. Cai, Meredith Ringel Morris, Percy Liang, and Michael S. Bernstein. 2023. Generative Agents: Interactive Simulacra of Human Behavior. In *The 36th Annual ACM Symposium on User Interface Software and Technology (UIST '23)*, October 29–November 1, 2023, San Francisco, CA, USA. ACM, New York, NY, USA. <https://doi.org/10.1145/3586183.3606763>. Preprint: <https://arxiv.org/abs/2304.03442>.

In that paper, agents retrieve memories by recency, importance, and relevance, synthesise reflections, and decompose a daily plan into smaller actions, with a language model producing the language. The scripts here use those three stages and implement them with the placeholder behaviour described above.

## Licence

Released under the [MIT License](LICENSE). Copyright (c) 2026 Denis Chiriac.
