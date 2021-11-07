public struct Objective
{
    public float TimeGoal { get; private set; }
    public int Current { get; private set; }

    public Objective(float timeGoal, int starCount)
    {
        TimeGoal = timeGoal;
        Current = starCount;
    }
}
