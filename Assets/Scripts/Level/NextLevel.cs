using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    int buildIndex;

    void Start()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            return;
        }
        gameObject.SetActive(false);
    }
    void OnMouseDown()
    {
        SceneManager.LoadScene(buildIndex + 1);
    }
}
