using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelsMain : MonoBehaviour
{
    [SerializeField] private List<string> levelNames;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameObject levelNoActivePrefab;

    void Start()
    {
        AddAllLevelButtons();
    }
    void Update()
    {
        
    }

    //--------------ДОБАВЛЕНИЕ ВСЕХ КНОПОК УРОВНЕЙ--------------
    private void AddAllLevelButtons()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);;
        for (int i = 0; i < levelNames.Count; i++)
        {
            string levelName = levelNames[i];
            int x = i / 2 * 4;
            
            float y = 2;
            if((i + 1) % 2 == 0)
            {
                y = -2;
            }

            if(i < currentLevel){
                GameObject level = Instantiate(levelPrefab, new Vector3(x, y, 0), Quaternion.identity);
                TextMeshPro text = level.GetComponent<TextMeshPro>();

                text.text = (i + 1).ToString();

                level.GetComponent<LevelsMenuButton>().setLevel(i + 1);
            }
            else
            {
                GameObject level = Instantiate(levelNoActivePrefab, new Vector3(x, y, 0), Quaternion.identity);
                TextMeshPro text = level.GetComponent<TextMeshPro>();

                text.text = (i + 1).ToString();
            }
        }
    }
    //===========================================================

}
