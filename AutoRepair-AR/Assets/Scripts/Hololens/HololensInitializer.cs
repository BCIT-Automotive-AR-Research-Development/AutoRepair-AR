using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HololensInitializer : MonoBehaviour
{
    public TaskList masterTaskList;
    [SerializeField]
    public GameObject TaskSelectorSlatePrefab;
    [SerializeField]
    public GameObject TaskSelectorButtonPrefab;

    private GameObject TaskSelector;
    // Start is called before the first frame update
    void Start()
    {
        TaskSelector = Instantiate(TaskSelectorSlatePrefab);
        InitializeTaskList();
    }

    private void InitializeTaskList()
    {
        GameObject taskGrid = TaskSelector.transform.Find("ScrollBox/Container/Grid").gameObject;
        //Instantiate button for each entry in tasklist
        foreach (var task in masterTaskList.tasks)
        {
            GameObject taskButton = Instantiate(TaskSelectorButtonPrefab);
            TaskButtonController taskButtonController = taskButton.GetComponent<TaskButtonController>();
            taskButtonController.SetTask(task);
            taskButtonController.HideMenu = () => ToggleTaskSelectorMenu(false);
            taskButton.transform.parent = taskGrid.transform;
        }
        taskGrid.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    void ToggleTaskSelectorMenu(bool enable)
    {
        TaskSelector.SetActive(enable);
    }
}
