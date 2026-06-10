using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject itemBar;
    private LevelLogic levelLogic;
    void Start()
    {
        
    }

    //нажал
    void OnMouseDown()
    {
        PasteItem();
    }

    //отпустил
    void OnMouseUp()
    {
        PasteItem();
    }
    

    //--------------ВСТАВКА ItemBar в свою ячейку-----------
    public void PasteItem()
    {
        List<GameObject> itemBars = levelLogic.GetItemBars();

        if(itemBars.Count == 0)
        {
            return;
        }

        GameObject firstItemBar = itemBars[0];

        if(firstItemBar != itemBar)
        {
            return;
        }

        itemBars.Remove(firstItemBar);
        levelLogic.GetItems().Remove(gameObject);
        Destroy(itemBar);
        Destroy(gameObject);

        levelLogic.Step();
    }
    //========================================



    public void SetItemBar(GameObject itemBar)
    {
        this.itemBar = itemBar;
    }

    public void SetLevelLogic(LevelLogic levelLogic)
    {
        this.levelLogic = levelLogic;
    }
}
