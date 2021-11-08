public struct Objective
{
    public float TimeGoal { get; set; }
    public int Current { get; set; }

    public Objective(float timeGoal, int current)
    {
        TimeGoal = timeGoal;
        Current = current;
    }

    public void Update(float timeGoal, int current)
    {
        TimeGoal = timeGoal;
        Current = current;
    }
}
