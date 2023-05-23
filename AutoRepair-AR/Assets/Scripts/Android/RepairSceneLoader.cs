using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RepairSceneLoader : MonoBehaviour
{

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("StepDownBtn");

        buttonStart.clickable.clicked += () =>
        {
            LoadRepairScene();
        };
    }

    public string mainSceneName = "TutorialStart";

    void LoadRepairScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
