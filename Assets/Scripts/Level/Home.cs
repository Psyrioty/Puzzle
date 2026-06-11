using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene(0);
    }
}
