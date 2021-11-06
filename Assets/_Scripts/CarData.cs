public static class CarData
{
    public static LiveData<float> Velocity;

    static CarData()
    {
        Velocity = new LiveData<float>();
    }
}
