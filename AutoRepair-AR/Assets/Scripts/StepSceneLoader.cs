using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StepSceneLoader : MonoBehaviour
{

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("StepUpBtn");

        buttonStart.clickable.clicked += () =>
        {
            LoadStepScene();
        };
    }

    public string mainSceneName = "Step-List";

    void LoadStepScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
