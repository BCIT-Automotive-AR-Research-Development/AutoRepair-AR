using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class SlateController : MonoBehaviour
{
    public Action HideMenu { get; internal set; }
    public PinchSlider slider;
    public TaskController task;
    public Interactable next, previous;
    public TextMeshPro label;

    private int stepCounter = 0, instructionsLength;

    //public VisualElement info, sliderHere;

    List<Step> instructions;

    //private bool isHidden = false;
    //private VisualElement[] allElements;

    //private void SetLabel()
    //{
    //    // Create a new Label
    //    label = new Label();
    //    label.style.color = Color.white;
    //    label.style.fontSize = Screen.height * 0.02f;
    //    label.style.whiteSpace = WhiteSpace.Normal;
    //    label.style.unityTextAlign = TextAnchor.MiddleCenter;

    //    label.text = instructions[0].Desc; // Set the text to the first instruction

    //    // Add the Label to the info VisualElement
    //    info.Add(label);
    //}

    private void SetSlider()
    {
        slider.SliderStepDivisions = instructionsLength;
        slider.OnValueUpdated.AddListener((value) => {
            stepCounter = (int) value.NewValue * instructionsLength;
            task.GoToStep(stepCounter);
            updateTaskInfo();
        });

    }

    private void Start()
    {
        // Get Buttons
        //getButtons();
        
        SetTask();
        next.OnClick.AddListener(() => {
            task.NextStep();
            //slider.value = task.GetCurrentStep();
        });

        previous.OnClick.AddListener(() => {
            task.PreviousStep();
            //slider.value = task.GetCurrentStep();
        });

        //replay.onClick.AddListener(() => {
        //    //Debug.Log("Replaying: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        //    //animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0);
        //    task.ReplayStep();
        //});
    }

    // Start is called before the first frame update
    public void SetTask()
    {
        // Get Task
        task = GameObject.FindGameObjectWithTag("Task").GetComponentInParent<TaskController>();

        // Get Instructions
        instructions = task.GetInstructions();
        instructionsLength = instructions.Count;

        //SetLabel();
        SetSlider();

    }

    //void getButtons()
    //{
    //    //VisualElement root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

    //    next = Child
    //    previous = root.Q<Button>("BackBtn");
    //    info = root.Q<VisualElement>("InfoText");
    //    sliderHere = root.Q<VisualElement>("SliderHere");
    //    hide = root.Q<Button>("HideBtn");

    //    // Store all Visual Elements in an array for easy access later
    //    allElements = new VisualElement[] { next, previous, info, sliderHere, hide };
    //}

    void updateTaskInfo()
    {
        // // Update the instructions UI element with the text for the previous step
        label.text = instructions[stepCounter].Desc;

        //stepLabel.text = (stepCounter + 1) + "/" + instructionsLength;
    }

}
