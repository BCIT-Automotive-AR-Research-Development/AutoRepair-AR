
using UnityEngine.UIElements;

public class TaskEntryController
{
    Label name;

    public void SetVisualElement(VisualElement visualElement)
    {
        name = visualElement.Q<Label>("task-name");
    }

    public void SetTaskData(TaskList.Task task)
    {
        name.text = task.name;
    }
}
