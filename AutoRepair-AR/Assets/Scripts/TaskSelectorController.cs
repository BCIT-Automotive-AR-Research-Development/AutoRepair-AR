using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TaskSelectorController
{
    public Action HideMenu { get; internal set; }

    // UXML template for list entries
    VisualTreeAsset listEntryTemplate;
    VisualElement root;
    ListView taskList;

    TaskList list;
    public TaskList.Task selectedTask;
    public GameObject spawnedTask;


    public TaskSelectorController(VisualElement root, VisualTreeAsset listEntryTemplate, TaskList list)
    {
        this.root = root;
        this.listEntryTemplate = listEntryTemplate;
        this.list = list;
        InitializeTaskList();
    }
    //private void OnEnable()
    //{

    //}

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
        selectedTask = taskList.selectedItem as TaskList.Task;
        if (selectedTask == null) return;

        spawnedTask = GameObject.Instantiate(selectedTask.target);
        //Hide Menu
        HideMenu();
    }

    public void LoadTask(int index)
    {
        spawnedTask = GameObject.Instantiate(list.tasks[index].target);
        //Hide TaskSelection UI
    }

    public void UnloadTask()
    {
        GameObject.Destroy(spawnedTask);
        //Show TaskSelection UI
    }
}
