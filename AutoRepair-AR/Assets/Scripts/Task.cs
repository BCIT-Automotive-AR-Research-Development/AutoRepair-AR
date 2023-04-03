using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task : MonoBehaviour
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
        //animatorOverrideController["Initial"] = instructions[stepCounter].clip;
        animator.Play("Initial", -1, 0);
    }

    public void NextStep()
    {
        if (stepCounter < instructions.Count)
        {
            stepCounter++;
            //animator.clip = instructions[stepCounter].clip;
            animatorOverrideController["Initial"] = instructions[stepCounter - 1].clip;
            animator.Play("Initial", -1, 0);
        }
    }

    public void PreviousStep()
    {
        if (stepCounter > 0)
        {
            stepCounter--;
            //animator.clip = instructions[stepCounter].clip;
            //animator.Play();
            animatorOverrideController["Initial"] = instructions[stepCounter - 1].clip;
            animator.Play("Initial", -1, 0);
        }
    }

    public void ReplayStep()
    {
        animator.Play("Step", -1, 0);
    }
}
