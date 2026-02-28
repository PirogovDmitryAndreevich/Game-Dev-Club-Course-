using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private const int DefaultStartValueCondition = 0;

    [SerializeField] private TasksView _taskView;
    [SerializeField] private BusStop[] _interactObjects;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Trophy[] _trophies;
    
    private int _completedConditions;
    private bool _finished;

    private Dictionary<TaskType, ConditionsBase> _conditionsData = new();
    private List<ITask> _tasks = new List<ITask>();

    public event Action FinishLevelConditionsCompleted;

    private void Awake()
    {
        if (_enemies != null)
        {
            foreach (Enemy enemy in _enemies)
            {
                _tasks.Add(enemy);
                enemy.TaskCompleted += UpdateCondition;
            }

            AddNewCondition(_enemies.Length, TaskType.Enemies);
        }

        if (_trophies != null)
        {
            foreach (Trophy trophy in _trophies)
            {
                _tasks.Add(trophy);
                trophy.TaskCompleted += UpdateCondition;
            }

            AddNewCondition(_trophies.Length, TaskType.Trophy);
        }
    }

    private void Start()
    {
        foreach (var condition in _conditionsData)
        {
            _taskView.AddCondition(condition.Value.Type,
                DefaultStartValueCondition, condition.Value.Task);
        }
    }

    private void OnDestroy()
    {
        foreach (ITask task in _tasks)
            task.TaskCompleted -= UpdateCondition;
    }

    private void AddNewCondition(int value, TaskType type)
    {
        ConditionsBase newCondition;

        newCondition = new ConditionsBase(value, type);

        if (!_conditionsData.ContainsKey(type))
            _conditionsData[type] = newCondition;
    }

    private void UpdateCondition(ITask task)
    {
        if (_finished)
            return;

        if (!_conditionsData.ContainsKey(task.Type))
            return;

        bool reached = _conditionsData[task.Type].AddProgress();

        int currentValue = _conditionsData[task.Type].Current;

        _taskView.ChangeValueTask(currentValue, task.Type);

        if (reached)
        {
            _completedConditions++;
            _taskView.TaskReached(task.Type);
            CheckFinished();
        }
    }

    private void CheckFinished()
    {
        if (_finished)
            return;

        if (_completedConditions == _conditionsData.Count)
        {
            _finished = true;
            FinishLevelConditionsCompleted?.Invoke();
        }
    }
}
