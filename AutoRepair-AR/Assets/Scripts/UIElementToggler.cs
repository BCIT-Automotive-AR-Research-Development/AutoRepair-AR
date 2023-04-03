using UnityEngine;
using UnityEngine.UIElements;

public class UIElementToggler : MonoBehaviour
{
    public VisualElement leftBtn;
    public VisualElement rightBtn;
    public VisualElement info;
    public VisualElement infoText;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button stepUpBtn = root.Q<Button>("StepUpBtn");
        Button stepDownBtn = root.Q<Button>("StepDownBtn");

        leftBtn = root.Q<VisualElement>("LeftBtn");
        rightBtn = root.Q<VisualElement>("RightBtn");
        info = root.Q<VisualElement>("InfoText");
        infoText = root.Q<VisualElement>("AllSteps");

        // Hide the info text and disable the step down button on start
        infoText.style.display = DisplayStyle.None;
        stepDownBtn.SetEnabled(false);

        stepUpBtn.clickable.clicked += () =>
        {
            // Hide the left button, right button, and info element
            leftBtn.style.display = DisplayStyle.None;
            rightBtn.style.display = DisplayStyle.None;
            info.style.display = DisplayStyle.None;
            stepDownBtn.style.display = DisplayStyle.Flex;

            // Show the info text element
            infoText.style.display = DisplayStyle.Flex;
            stepUpBtn.style.display = DisplayStyle.None;

            // Enable the step down button
            stepDownBtn.SetEnabled(true);
        };

        stepDownBtn.clickable.clicked += () =>
        {
            // Show the left button, right button, and info element
            leftBtn.style.display = DisplayStyle.Flex;
            rightBtn.style.display = DisplayStyle.Flex;
            info.style.display = DisplayStyle.Flex;
            stepUpBtn.style.display = DisplayStyle.Flex;

            // Hide the info text element
            infoText.style.display = DisplayStyle.None;
            stepDownBtn.style.display = DisplayStyle.None;

            // Disable the step down button
            stepDownBtn.SetEnabled(false);
        };
    }
}
