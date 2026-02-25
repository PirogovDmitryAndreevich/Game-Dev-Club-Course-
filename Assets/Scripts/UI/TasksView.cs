using System.Collections.Generic;
using UnityEngine;

public class TasksView : MonoBehaviour
{
    [SerializeField] private ConditionView[] _conditionsPrefabs;
    [SerializeField] private Transform _conditionsContain;

    private Dictionary<TaskType, ConditionView> _conditionViews;
    private Dictionary<TaskType, ConditionView> _templates;

    private void Awake()
    {
        _conditionViews = new Dictionary<TaskType, ConditionView>();
        _templates = new Dictionary<TaskType, ConditionView>();

        foreach (var prefab in _conditionsPrefabs)
            _templates[prefab.Type] = prefab;
    }

    public void AddCondition(TaskType type, int current, int value)
    {
        if (!_templates.ContainsKey(type))
            return;

        var newCondition = Instantiate(_templates[type], _conditionsContain);

        _conditionViews[type] = newCondition;
        newCondition.SetCondition(current, value);
    }

    public void ChangeValueTask(int value, TaskType type)
    {
        if (_conditionViews.ContainsKey(type))
            _conditionViews[type].UpdateCount(value);
    }

    public void TaskReached(TaskType type)
    {
        if (_conditionViews.ContainsKey(type))
            _conditionViews[type].Reached();
    }
}
