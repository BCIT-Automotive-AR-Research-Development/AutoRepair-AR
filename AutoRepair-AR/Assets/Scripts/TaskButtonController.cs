using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskButtonController : MonoBehaviour
{
    [SerializeField]
    TextMeshPro taskButtonName;
    [SerializeField]
    GameObject InstructionsSlatePrefab;
    TaskList.Task task;

    GameObject spawnedTask, instructionSlate;

    public void SetTask(TaskList.Task task)
    {
        this.task = task;
        taskButtonName.text = task.name;
    }

    public void OnButtonClick()
    {
        spawnedTask = GameObject.Instantiate(task.target);
        instructionSlate = GameObject.Instantiate(InstructionsSlatePrefab, spawnedTask.transform.position + new Vector3(0, 0, -1), spawnedTask.transform.rotation * Quaternion.Euler(0, -90, 0), spawnedTask.transform);
    }
}
