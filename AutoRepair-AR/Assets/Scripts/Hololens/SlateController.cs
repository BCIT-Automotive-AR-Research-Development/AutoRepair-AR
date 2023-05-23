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
    public Interactable next, previous, replay, offset;
    public TextMeshPro stepLabel, stepDesc;
    

    [SerializeField]
    private int stepCounter = 0, instructionsLength, sliderStepDivisions;

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
        sliderStepDivisions = instructionsLength - 1;
        slider.SliderStepDivisions = sliderStepDivisions;
    }

    public void OnSliderValueUpdate(SliderEventData call)
    {
        stepCounter = (int) (call.NewValue * sliderStepDivisions);
        updateTaskInfo();
        task.GoToStep(stepCounter);
        task.PlayStep();
    }

    public void OnReplayClick()
    {
        task.PlayStep();
    }

    public void OnOffsetClick()
    {
        task.OffsetToggle();
    /*   isMovementEnabled = !isMovementEnabled; // Toggle the flag

        foreach (GameObject obj in movableObjects)
        {
            // Get the object's original position
            Vector3 originalPosition = obj.GetComponent<OriginalPosition>().position;

            // Enable or disable the object's movement based on the flag
            obj.GetComponent<DraggableObject>().enabled = isMovementEnabled;

            if (!isMovementEnabled)
            {
                // Reset the object's position to its original spot
                obj.transform.position = originalPosition;
            }
        }*/
    }

    public void OnNextClick()
    {
        task.NextStep();
        slider.SliderValue = (float)task.GetCurrentStep() / sliderStepDivisions;
    }

    public void OnPreviousClick()
    {
        task.PreviousStep();
        slider.SliderValue = (float)task.GetCurrentStep() / sliderStepDivisions;
    }

    private void Start()
    {
        SetTask();
        SetSlider();
        updateTaskInfo();

    }

    // Start is called before the first frame update
    public void SetTask()
    {
        // Get Task
        task = GameObject.FindGameObjectWithTag("Task").GetComponent<TaskController>();

        // Get Instructions
        instructions = task.GetInstructions();
        instructionsLength = instructions.Count;
    }

    void updateTaskInfo()
    {
        stepLabel.text = (stepCounter + 1) + "/" + instructionsLength;
        stepDesc.text = instructions[stepCounter].Desc;
    }

}
