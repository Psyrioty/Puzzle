using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string levelScene = "MainMenu";
    public void OnClick()
    {
        SceneManager.LoadScene(levelScene);
    }
}
