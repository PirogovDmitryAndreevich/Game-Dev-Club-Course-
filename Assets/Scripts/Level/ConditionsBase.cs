public class ConditionsBase
{
    private int _task = 0;
    private int _current;
    private bool _isReached = false;

    public ConditionsBase(int task, TaskType type)
    {
        _task = task;
        Type = type;
        _isReached = false;
    }

    public TaskType Type { get; set; }
    public int Task => _task;
    public int Current  => _current;
    public bool IsReached => _isReached;

    public bool AddProgress(int amount = 1)
    {
        if (_isReached)
            return false;

        _current += amount;

        if (_current >= _task)
        {
            _isReached = true;
            return true;
        }

        return false;
    }
}