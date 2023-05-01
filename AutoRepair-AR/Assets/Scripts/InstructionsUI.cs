using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

public class InstructionsUI
{
    public Action HideMenu { get; internal set; }

    VisualElement root;
    public Button next, back, hide;
    private int stepCounter = 0, instructionsLength;
    TaskController task;
    private SliderInt slider;
    private Label label, stepLabel;
    private StyleSheet circularStyleSheet;

    public VisualElement info, sliderHere;

    List<Step> instructions;

    private bool isHidden = false;
    private VisualElement[] allElements;

    private void CreateLabel()
    {
        // Create a new Label
        label = new Label();
        label.style.color = Color.white;
        label.style.fontSize = Screen.height * 0.02f;
        label.style.whiteSpace = WhiteSpace.Normal;
        label.style.unityTextAlign = TextAnchor.MiddleCenter;

        label.text = instructions[0].Desc; // Set the text to the first instruction

        // Add the Label to the info VisualElement
        info.Add(label);
    }

    private void CreateSlider()
    {
        slider = new SliderInt(0, instructions.Count - 1, SliderDirection.Horizontal, 1);
        slider.AddToClassList("circular");
        slider.style.height = Screen.height * 0.1f;
        slider.style.width = Screen.width * 0.75f;
        slider.style.marginTop = Screen.height * 0.05f;
        slider.value = stepCounter;
        slider.RegisterValueChangedCallback(evt => {
            stepCounter = evt.newValue;
            //label.text = instructions[stepCounter];
            //stepLabel.text = stepCounter.ToString() + "/" + instructionLengthString;
            updateTaskInfo();
            task.GoToStep(stepCounter);
            task.PlayStep();
        });

        // Load and apply circular.uss stylesheet
        if (circularStyleSheet != null)
        {
            slider.styleSheets.Add(circularStyleSheet);
        }

        // Create container for slider and step label
        VisualElement sliderContainer = new VisualElement();
        sliderContainer.style.justifyContent = Justify.Center;
        sliderContainer.style.alignItems = Align.Center;
        sliderContainer.style.flexDirection = FlexDirection.Column;

        // Create label for step number
        stepLabel = new Label();
        stepLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        stepLabel.text = (stepCounter + 1) + "/" + instructionsLength;
        stepLabel.style.fontSize = Screen.height * 0.02f;
        stepLabel.style.position = Position.Absolute;
        stepLabel.style.top = new StyleLength(30);

        // Add slider and step label to container
        sliderContainer.Add(stepLabel);
        sliderContainer.Add(slider);
        sliderHere.Add(sliderContainer);
    }

    public InstructionsUI(VisualElement root)
    {
        this.root = root;

        // Get Buttons
        getButtons();

        next.clicked += () => {
            task.NextStep();
            slider.value = task.GetCurrentStep();
            //// Check if the step counter is still within the range of instructions
            //if (stepCounter < instructions.Count)
            //{
            //    // Increase the step counter
            //    stepCounter++;
            //    // // Update the instructions UI element with the text for the next step
            //    // info.text = instructions[stepCounter];

            //    // // Update the TextField text for the next step
            //    // textField.SetValueWithoutNotify(instructions[stepCounter]);

            //    //label.text = instructions[stepCounter - 1];

            //    //stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();
            //    // Update the SliderInt value to the new step counter
            //    slider.value = stepCounter;
            //    updateTaskInfo();
            //    // Move to the next step in the task
            //    task.NextStep();
            //    task.PlayStep();
            //}
        };

        back.clicked += () => {
            // Check if the step counter is still within the range of instructions
            if (stepCounter > 0)
            {
                // Decrease the step counter
                stepCounter--;
                //// // Update the instructions UI element with the text for the previous step
                //label.text = instructions[stepCounter];

                //stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();

                // Update the SliderInt value to the new step counter
                slider.value = stepCounter;
                updateTaskInfo();

                // Move to the previous step in the task
                task.PreviousStep();
                task.PlayStep();
            }
        };

        hide.clicked += () => {
            if (isHidden)
            {
                // Show all Visual Elements
                foreach (VisualElement element in allElements)
                {
                    element.style.display = DisplayStyle.Flex;
                }
                // Rotate the Hide button back to its original position
                hide.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                isHidden = false;
            }
            else
            {
                // Hide all Visual Elements except the Hide button
                foreach (VisualElement element in allElements)
                {
                    if (element != hide)
                    {
                        element.style.display = DisplayStyle.None;
                    }
                }
                // Rotate hide button 180 degrees
                hide.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                isHidden = true;
            }
        };

        //replay.onClick.AddListener(() => {
        //    //Debug.Log("Replaying: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        //    //animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0);
        //    task.ReplayStep();
        //});
    }

    // Start is called before the first frame update
    public void SetTask(TaskController task)
    {
        // Get Task
        this.task = task;
        // Get Instructions
        instructions = task.GetInstructions();
        instructionsLength = instructions.Count;

        CreateLabel();

        CreateSlider();

    }

    void getButtons()
    {
        //VisualElement root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        next = root.Q<Button>("NextBtn");
        back = root.Q<Button>("BackBtn");
        info = root.Q<VisualElement>("InfoText");
        sliderHere = root.Q<VisualElement>("SliderHere");
        hide = root.Q<Button>("HideBtn");

        // Store all Visual Elements in an array for easy access later
        allElements = new VisualElement[] { next, back, info, sliderHere, hide };
    }

    void updateTaskInfo()
    {
        // // Update the instructions UI element with the text for the previous step
        label.text = instructions[stepCounter].Desc;

        stepLabel.text = (stepCounter + 1) + "/" + instructionsLength;
    }

}
