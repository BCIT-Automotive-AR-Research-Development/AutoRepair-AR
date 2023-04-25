using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TaskList : ScriptableObject
{
    [System.Serializable]
    public struct Task
    {
        public string name;
        public string description;
        public GameObject target;
    }

    public List<Task> tasks = new List<Task>();
}
