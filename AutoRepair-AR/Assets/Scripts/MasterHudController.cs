using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MasterHudController : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset listEntryTemplate;
    private VisualElement root;
    private VisualElement _taskSelectorView;
    private VisualElement _taskInstructionView;

    public TaskList masterTaskList;
    TaskSelectorController taskSelectorController;
    InstructionsUI instructionsUI;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        _taskSelectorView = root.Q("TaskSelector");
        _taskInstructionView = root.Q("InstructionUI");

        SetupTaskSelector();
        SetUpInstructionView();
    }

    void SetupTaskSelector()
    {
        taskSelectorController = new TaskSelectorController(root, listEntryTemplate, masterTaskList);
        taskSelectorController.HideMenu = () => ToggleTaskSelectorMenu(false);
    }

    void SetUpInstructionView()
    {
        instructionsUI = new InstructionsUI(root);
        instructionsUI.HideMenu = () => ToggleTaskSelectorMenu(true);
    }

    void ToggleTaskSelectorMenu(bool enable)
    {
        _taskSelectorView.Display(enable);
        _taskInstructionView.Display(!enable);
        if (!enable) { instructionsUI.SetTask(taskSelectorController.spawnedTask.GetComponentInChildren<TaskController>()); }
    }
}
