using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelsMain : MonoBehaviour
{
    [SerializeField] private List<string> levelNames;
    [SerializeField] private GameObject levelPrefab;

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
        for (int i = 0; i < levelNames.Count; i++)
        {
            string levelName = levelNames[i];
            int x = i / 2 * 4;
            
            float y = 2;
            if((i + 1) % 2 == 0)
            {
                y = -2;
            }


            GameObject level = Instantiate(levelPrefab, new Vector3(x, y, 0), Quaternion.identity);
            TextMeshPro text = level.GetComponent<TextMeshPro>();

            Debug.Log(text);
        }
    }
    //===========================================================

}
