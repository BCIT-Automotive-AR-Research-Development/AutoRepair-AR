using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class InstructionsUI : MonoBehaviour
{

    public Button next, back;
    private int stepCounter = 0, instructionsLength;
    TaskController task;
    private string instructionLengthString;
    private SliderInt slider;
    private Label label, stepLabel;

    public VisualElement info, sliderHere;

    string[] instructions;
    //  = new string[]{
    // "Using a metal pick tool, remove screw cover on the door handle then remove the screw (+)",
    // "Using a metal pick tool, remove screw cover on the door latch handle then remove the screw (+)",
    // "Using a plastic tool, pry the tweeter speaker starting from the hinge side, then remove the speaker connector cable.",
    // "Using a plastic tool, pry the whole door interior panel starting from the bottom (9 clips).", 
    // "Once all clips are loose, lift up the door panel that is now loosely hanging on the door. (Do not pull the panel away from the door, there are connectors still attached to the interior component)",
    // "Before completely taking off the door panel, disconnect the power window and side window connectors and the cable for the door locking/release mechanism. (2 connectors + 2 wire cables)",
    // "Remove the metal bracket that was holding the door handle screw in Step 1 by removing 2 screws(+).",
    // "Remove the vapour barrier carefully without ripping the filament and avoiding getting the bentyl adhesive on self. Feed the 2 cables and 2 connectors in Step 5 through the barrier before removing.",
    // "Remove black metal panel to access the window regulator. 4 Screws",
    // "Remove the main speaker by twisting counter clockwise then remove speaker connector before fully removing it from the car.",
    // "Using a 10mm shallow socket, remove 2 bolts holding the window glass on to the regulator.",
    // "Remove the window glass by carefully lifting the glass through the window slit, towards the exterior of the car.",
    // "Remove the upper black plastic cover to access the window regulator power connector. Press down the topside notch to release the cover.",
    // "Remove the power connector for the window regulator.",
    // "Using a 10 mm shallow socket, remove 6 bolts holding the window regulator."
    // };

    private void CreateLabel()
    {
        // Create a new Label
        label = new Label();
        label.style.color = Color.white;
        label.style.fontSize = Screen.height * 0.015f;
        label.style.whiteSpace = WhiteSpace.Normal;
        label.style.unityTextAlign = TextAnchor.MiddleCenter;

        label.text = instructions[0]; // Set the text to the first instruction

        // Add the Label to the info VisualElement
        info.Add(label);
    }

    private void CreateSlider()
    {
        slider = new SliderInt(0, instructions.Length, SliderDirection.Horizontal, 1);
        slider.style.height = Screen.height * 0.1f;
        slider.style.width = Screen.width * 0.7f;
        slider.value = stepCounter;
        slider.RegisterValueChangedCallback(evt => {
            stepCounter = evt.newValue;
            label.text = instructions[stepCounter];
            stepLabel.text = stepCounter.ToString() + "/" + instructionLengthString;
            task.GoToStep(stepCounter);
        });

        var sliderStyleSheet = Resources.Load<StyleSheet>("Assets/USS/slider-style.uss");
        if (sliderStyleSheet != null)
        {
            slider.styleSheets.Add(sliderStyleSheet);
        }

        // Create container for slider and step label
        VisualElement sliderContainer = new VisualElement();
        sliderContainer.style.justifyContent = Justify.Center;
        sliderContainer.style.alignItems = Align.Center;
        sliderContainer.style.flexDirection = FlexDirection.Column;

        // Create label for step number
        stepLabel = new Label();
        stepLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();
        stepLabel.style.fontSize = Screen.height * 0.015f;
        stepLabel.style.marginTop = Screen.height * 0.01f;

        // Add slider and step label to container
        sliderContainer.Add(stepLabel);
        sliderContainer.Add(slider);
        sliderHere.Add(sliderContainer);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        // Get Task
        task = GameObject.FindGameObjectWithTag("Task").GetComponent<TaskController>();
        // Get Instructions
        instructions = task.GetStepDescriptions();
        int instructionsLength = instructions.Length - 1;
        instructionLengthString = instructionsLength.ToString();
        // Get Buttons
        getButtons();

        CreateLabel();

        CreateSlider();

        next.clicked += () => {
            // Check if the step counter is still within the range of instructions
            if (stepCounter < instructions.Length)
            {
                // Increase the step counter
                stepCounter++;
                // // Update the instructions UI element with the text for the next step
                // info.text = instructions[stepCounter];

                // // Update the TextField text for the next step
                // textField.SetValueWithoutNotify(instructions[stepCounter]);

                label.text = instructions[stepCounter];

                stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();

                // Update the SliderInt value to the new step counter
                slider.value = stepCounter;

                // Move to the next step in the task
                task.NextStep();
            }
        };

        back.clicked += () => {
            // Check if the step counter is still within the range of instructions
            if (stepCounter > 0)
            {
                // Decrease the step counter
                stepCounter--;
                // // Update the instructions UI element with the text for the previous step
                // info.text = instructions[stepCounter];

                // Update the TextField text for the previous step
                // textField.SetValueWithoutNotify(instructions[stepCounter]);

                label.text = instructions[stepCounter];

                stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();

                // Update the SliderInt value to the new step counter
                slider.value = stepCounter;

                // Move to the previous step in the task
                task.PreviousStep();
            }
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
        info = root.Q<VisualElement>("InfoText");
        sliderHere = root.Q<VisualElement>("SliderHere");

    }

}
