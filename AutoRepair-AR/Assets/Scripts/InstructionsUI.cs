using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class InstructionsUI : MonoBehaviour
{
    public Button next, back, hide;
    private int stepCounter = 0, instructionsLength;
    TaskController task;
    private string instructionLengthString;
    private SliderInt slider;
    private Label label, stepLabel;
    private StyleSheet circularStyleSheet;
    
    public VisualElement info, sliderHere;

    string[] instructions;

    private bool isHidden = false;
    private VisualElement[] allElements;

    private void CreateLabel(){
        // Create a new Label
        label = new Label();
        label.style.color = Color.white;
        label.style.fontSize = Screen.height * 0.02f;
        label.style.whiteSpace = WhiteSpace.Normal;
        label.style.unityTextAlign = TextAnchor.MiddleCenter;

        label.text = instructions[0]; // Set the text to the first instruction

        // Add the Label to the info VisualElement
        info.Add(label);
    }

    private void CreateSlider(){
        slider = new SliderInt(0, instructions.Length, SliderDirection.Horizontal, 1);
        slider.AddToClassList("circular");
        slider.style.height = Screen.height * 0.1f;
        slider.style.width = Screen.width * 0.75f;
        slider.style.marginTop = Screen.height * 0.05f;
        slider.value = stepCounter;
        slider.RegisterValueChangedCallback(evt => {
            stepCounter = evt.newValue;
            label.text = instructions[stepCounter];
            stepLabel.text = stepCounter.ToString() + "/" + instructionLengthString;
            task.GoToStep(stepCounter);
        });
        
         // Load and apply circular.uss stylesheet
        if (circularStyleSheet != null){
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
        stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();
        stepLabel.style.fontSize = Screen.height * 0.02f;
        stepLabel.style.position = Position.Absolute;
        stepLabel.style.top = new StyleLength(30);

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

        circularStyleSheet = Resources.Load<StyleSheet>("Assets/_UI/circular.uss");

        // Create Label
        CreateLabel();

        // Create Slider
        CreateSlider();

        next.clicked += () => {
        // Increase the step counter
        stepCounter++;
            // Check if the step counter is still within the range of instructions
            if (stepCounter < instructions.Length) {
                // // Update the instructions UI element with the text for the next step
                label.text = instructions[stepCounter];

                stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();
                // Update the SliderInt value to the new step counter
                slider.value = stepCounter;
                        
                // Move to the next step in the task
                task.NextStep();
            }
        };

        back.clicked += () => {
            // Decrease the step counter
            stepCounter--;
            // Check if the step counter is still within the range of instructions
            if (stepCounter >= 0) {
                // // Update the instructions UI element with the text for the previous step
                label.text = instructions[stepCounter];

                stepLabel.text = stepCounter.ToString() + "/" + instructionsLength.ToString();
                // Update the SliderInt value to the new step counter
                slider.value = stepCounter;

                // Move to the previous step in the task
                task.PreviousStep();
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

    void getButtons()
    {
        VisualElement root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        next = root.Q<Button>("NextBtn");
        back = root.Q<Button>("BackBtn");
        info = root.Q<VisualElement>("InfoText");
        sliderHere = root.Q<VisualElement>("SliderHere");
        hide = root.Q<Button>("HideBtn");

        // Store all Visual Elements in an array for easy access later
        allElements = new VisualElement[] { next, back, info, sliderHere, hide };
    }

}
