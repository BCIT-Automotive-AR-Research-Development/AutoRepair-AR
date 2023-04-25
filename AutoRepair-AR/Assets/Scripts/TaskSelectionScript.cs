using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelectionScript : MonoBehaviour
{
    [System.Serializable]
    public struct Task
    {
        public string name;
        public string description;
        public GameObject target;
    }

    public List<Task> tasks = new List<Task>();

    private void Start()
    {
        //TODO populate UI list with task list
        Instantiate(tasks[0].target);
    }
}
