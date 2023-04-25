using System.Collections;
using System.Collections.Generic;
//using UnityEditor.XR.ARKit;
using UnityEngine;
using UnityEngine.UIElements;

public class InstructionsUI : MonoBehaviour
{

    public Button next, back;
    private int stepCounter = 0;
    TaskController task;

    // Start is called before the first frame update
    void OnEnable()
    {
        // Get Task
        task = GameObject.FindGameObjectWithTag("Task").GetComponent<TaskController>();
        // Get Buttons
        getButtons();

        //animator = GameObject.Find("Model").GetComponent<Animator>();
        next.clicked += () => {
            //stepCounter++;
            //animator.SetInteger("StepCount", stepCounter);
            task.NextStep();
        };

        back.clicked += () => {
            //if (stepCounter - 1 >= 0) stepCounter--;
            //animator.SetInteger("StepCount", stepCounter);
            task.PreviousStep();
        };

        //replay.onClick.AddListener(() => {
        //    //Debug.Log("Replaying: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        //    //animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0);
        //    task.ReplayStep();
        //});
    }

    void getButtons()
    {
        VisualElement root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        next = root.Q<Button>("NextBtn");
        back = root.Q<Button>("BackBtn");

    }

}
