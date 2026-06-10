using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuButton : MonoBehaviour
{
    private int level = 1;
    void OnMouseDown()
    {
        SceneManager.LoadScene("Level_" + level);
    }

    public void setLevel(int level)
    {
        this.level = level;
    }
}
