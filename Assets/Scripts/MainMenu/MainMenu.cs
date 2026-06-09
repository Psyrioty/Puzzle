using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string levelScene;
    public void OnClick()
    {
        SceneManager.LoadScene(levelScene);
    }
}
