using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{

    public Button next, back, replay;
    private int stepCounter = 0;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Model").GetComponent<Animator>();
        next.onClick.AddListener(() => {
            stepCounter++;
            animator.SetInteger("StepCount", stepCounter);
        });

        back.onClick.AddListener(() => {
            stepCounter--;
            animator.SetInteger("StepCount", stepCounter);
        });

        replay.onClick.AddListener(() => {
            Debug.Log("Replaying: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0);
        });
    }

}
