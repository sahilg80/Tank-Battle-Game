
public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null) instance = new EventService();
            return instance;
        }
    }

    public EventController<int> OnNewWaveStart { get; private set; }
    public EventController<int> OnEnemyTankKilled { get; private set; }
    public EventController<bool> OnGameEnd { get; private set; }
    private EventService()
    {
        OnNewWaveStart = new EventController<int>();
        OnEnemyTankKilled = new EventController<int>();
        OnGameEnd = new EventController<bool>();
    }
}
 