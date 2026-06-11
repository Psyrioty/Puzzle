using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string levelScene = "MainMenu";
    [SerializeField] List<AudioClip> sounds;
    public void OnClick()
    {
        SceneManager.LoadScene(levelScene);
        GetComponent<RandomSound>().PlaySoundRandom(sounds);
    }
}
