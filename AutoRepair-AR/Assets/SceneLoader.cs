using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("ButtonStart");

        buttonStart.clickable.clicked += () =>
        {
            LoadMainScene();
        };
    }

    public string mainSceneName = "TutorialStart";

    void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
