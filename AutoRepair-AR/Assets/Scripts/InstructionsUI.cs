using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.ARKit;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{

    public Button next, back, replay;
    private int stepCounter = 0;
    Task task;

    // Start is called before the first frame update
    void Start()
    {
        task = GameObject.FindGameObjectWithTag("Task").GetComponent<Task>();
        //animator = GameObject.Find("Model").GetComponent<Animator>();
        next.onClick.AddListener(() => {
            //stepCounter++;
            //animator.SetInteger("StepCount", stepCounter);
            task.NextStep();
        });

        back.onClick.AddListener(() => {
            //if (stepCounter - 1 >= 0) stepCounter--;
            //animator.SetInteger("StepCount", stepCounter);
            task.PreviousStep();
        });

        replay.onClick.AddListener(() => {
            //Debug.Log("Replaying: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
            //animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0);
            task.ReplayStep();
        });
    }

}
