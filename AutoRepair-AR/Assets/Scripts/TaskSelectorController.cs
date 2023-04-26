using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class TaskSelectorController : MonoBehaviour
{
    // UXML template for list entries
    [SerializeField]
    VisualTreeAsset listEntryTemplate;
    VisualElement root;
    ListView taskList;

    public TaskList list;
    GameObject spawnedTask;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        InitializeTaskList();
    }

    private void InitializeTaskList()
    {
        taskList = root.Q<ListView>("task-list");
        taskList.makeItem = () => {
            var newListEntry = listEntryTemplate.Instantiate();
            var newListEntryLogic = new TaskEntryController();
            newListEntry.userData = newListEntryLogic;
            newListEntryLogic.SetVisualElement(newListEntry);
            return newListEntry;
        };

        taskList.bindItem = (item, index) => {
            (item.userData as TaskEntryController).SetTaskData(list.tasks[index]);
        };

        taskList.fixedItemHeight = 45;
        taskList.itemsSource = list.tasks;
        taskList.onSelectionChange += OnTaskSelected;
    }

    void OnTaskSelected(IEnumerable<object> selectedItems)
    {

        // Get the currently selected item directly from the ListView
        var selectedTask = taskList.selectedItem as TaskList.Task;
        if (selectedTask == null) return;

        spawnedTask = spawnedTask = Instantiate(selectedTask.target);
    }

    public void LoadTask(int index)
    {
        spawnedTask = Instantiate(list.tasks[index].target);
        //Hide TaskSelection UI
    }

    public void UnloadTask()
    {
        Destroy(spawnedTask);
        //Show TaskSelection UI
    }
}
