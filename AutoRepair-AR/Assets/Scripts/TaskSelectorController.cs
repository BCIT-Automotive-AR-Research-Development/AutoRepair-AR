using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelector : MonoBehaviour
{
    public TaskList list;
    GameObject spawnedTask;
    // Start is called before the first frame update
    void Start()
    {
        //TODO populate UI list with task list then enable Instructions UI
        //Instantiate(list.tasks[0].target);
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
