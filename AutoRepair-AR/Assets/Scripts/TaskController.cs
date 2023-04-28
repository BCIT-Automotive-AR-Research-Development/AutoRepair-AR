using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskController : MonoBehaviour
{
    public List<Step> instructions = new List<Step>();
    public int stepCounter = 0;

    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //animator.clip = instructions[stepCounter].clip;
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        PlayStep();
    }

    public void NextStep()
    {
        if (stepCounter < instructions.Count - 1)
        {
            stepCounter++;
            //PlayStep();
        }
    }

    public void PreviousStep()
    {
        if (stepCounter > 0)
        {
            stepCounter--;
            //PlayStep();
        }
    }

    public int GetCurrentStep()
    {
        return stepCounter;
    }

    public void GoToStep(int stepToGo)
    {
        stepCounter = stepToGo;
        //PlayStep();
    }

    public List<Step> GetInstructions()
    {
        return instructions;
    }

    public string[] GetStepDescriptions()
    {
        string[] descriptions = new string[instructions.Count];
        for (int i = 0; i < instructions.Count; i++)
        {
            descriptions[i] = instructions[i].Desc;
        }
        //descriptions[instructions.Count] = "All Done!";
        return descriptions;
    }


    public void PlayStep()
    {
        animatorOverrideController["Initial"] = instructions[stepCounter].clip;
        animator.Play("Initial", -1, 0);
    }
}
