using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.ARKit;
using UnityEngine;
using UnityEngine.UIElements;

public class InstructionsUI : MonoBehaviour
{

    public Button next, back;
    private int stepCounter = 0;
    Task task;
    
    public VisualElement info;

    string[] instructions = new string[] {
    "Using a metal pick tool, remove screw cover on the door handle then remove the screw (+)",
    "Using a metal pick tool, remove screw cover on the door latch handle then remove the screw (+)",
    "Using a plastic tool, pry the tweeter speaker starting from the hinge side, then remove the speaker connector cable.",
    "Using a plastic tool, pry the whole door interior panel starting from the bottom (9 clips). Once all clips are loose, lift up the door panel that is now loosely hanging on the door. (Do not pull the panel away from the door, there are connectors still attached to the interior component)",
    "Before completely taking off the door panel, disconnect the power window and side window connectors and the cable for the door locking/release mechanism. (2 connectors + 2 wire cables)",
    "Remove the metal bracket that was holding the door handle screw in Step 1 by removing 2 screws(+).",
    "Remove the vapour barrier carefully without ripping the filament and avoiding getting the bentyl adhesive on self. Feed the 2 cables and 2 connectors in Step 5 through the barrier before removing.",
    "Remove black metal panel to access the window regulator. 4 Screws",
    "Remove the main speaker by twisting counter clockwise then remove speaker connector before fully removing it from the car.",
    "Using a 10mm shallow socket, remove 2 bolts holding the window glass on to the regulator.",
    "Remove the window glass by carefully lifting the glass through the window slit, towards the exterior of the car.",
    "Remove the upper black plastic cover to access the window regulator power connector. Press down the topside notch to release the cover.",
    "Remove the power connector for the window regulator.",
    "Using a 10 mm shallow socket, remove 6 bolts holding the window regulator."
    };


    // Start is called before the first frame update
    void OnEnable()
    {
        // Get Task
        task = GameObject.FindGameObjectWithTag("Task").GetComponent<Task>();
        // Get Buttons
        getButtons();

        // Display the first step's instructions
        info.text = instructions[0];

        //animator = GameObject.Find("Model").GetComponent<Animator>();
        next.clicked += () => {
        // Increase the step counter
        stepCounter++;
            // Check if the step counter is still within the range of instructions
            if (stepCounter < instructions.Length) {
                // Update the instructions UI element with the text for the next step
                info.text = instructions[stepCounter];
                // Move to the next step in the task
                task.NextStep();
            }
        };

        back.clicked += () => {
            // Decrease the step counter
            stepCounter--;
            // Check if the step counter is still within the range of instructions
            if (stepCounter >= 0) {
                // Update the instructions UI element with the text for the previous step
                info.text = instructions[stepCounter];
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
        info = root.Q<VisualElement>("Info");

    }

}
