using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class HandTracking : MonoBehaviour
{

    [SerializeField]
    public GameObject TaskWindowPrefab, TaskWindow;

    [SerializeField]
    public GameObject ToggleSwitchPrefab, ToggleSwitch;

    MixedRealityPose pose;

    // Start is called before the first frame update
    void Start()
    {
        TaskWindow = Instantiate(TaskWindowPrefab, this.transform);
        ToggleSwitch = Instantiate(ToggleSwitchPrefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out pose))
        {
            TaskWindowPrefab.transform.position = pose.Position;
            TaskWindowPrefab.transform.Translate(0f,0f,-0.05f);
            TaskWindowPrefab.transform.rotation = pose.Rotation;
            TaskWindowPrefab.transform.Rotate(90f, 0f, 90f);

            ToggleSwitch.transform.position = pose.Position;
            // x axis (negative direction) how far away from wrist joint (or Task selector screen)
            ToggleSwitch.transform.Translate(-0.15f, 0f, -0.05f);
            ToggleSwitch.transform.rotation = pose.Rotation;
            ToggleSwitch.transform.Rotate(90f, 0f, 90f);

        }

        TaskWindowPrefab.SetActive(ToggleSwitch.GetComponent<Interactable>().IsToggled);

    }
}
