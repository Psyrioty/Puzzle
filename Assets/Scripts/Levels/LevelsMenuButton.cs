using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuButton : MonoBehaviour
{
    private int level = 1;
    void OnMouseDown()
    {
        Debug.Log("Нажат уровень " + level);
    }

    public void setLevel(int level)
    {
        this.level = level;
    }
}
