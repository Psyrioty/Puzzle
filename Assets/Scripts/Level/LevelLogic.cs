using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{

    [SerializeField] private List<GameObject> itemBars;
    [SerializeField] private List<GameObject> items;
    [SerializeField] private GameObject itemBarPrefab;
    [SerializeField] private Transform camera;
    [SerializeField] private float stepOffset = 1200;

    void Start()
    {
        FindAllItems();
    }

    void Update()
    {
        
    }


    //поиск и создание Item и ItemBar
    private void FindAllItems()
    {
        float x = 0;
        float y = -1460;

        int i = 0; //итератор

        foreach (Transform child in transform)
        {
            Item item = child.GetComponent<Item>();
            if(item == null)
            {
                continue;
            }

            GameObject itemGameObject = child.GameObject();
            itemGameObject.GetComponent<Item>().SetLevelLogic(this);

            items.Add(itemGameObject);

            Vector3 vector3 = new Vector3(x, y, 0);
            GameObject itemBar = Instantiate(itemBarPrefab, vector3, Quaternion.identity, camera);
            item.SetItemBar(itemBar);
            itemBars.Add(itemBar);

            ItemBar itemBarClass = itemBar.GetComponent<ItemBar>();
            itemBarClass.SetItem(itemGameObject);
            itemBarClass.SetStartPosition(x, y);

            Sprite sprite = itemGameObject.GetComponent<SpriteMask>().sprite;
            itemBarClass.SetTexture(sprite);

            if(i == 0)
            {
                itemBarClass.Unblock();
            }

            x+=stepOffset;
            i++;
        }
    }

    public List<GameObject> GetItemBars()
    {
        return itemBars;
    }

    public List<GameObject> GetItems()
    {
        return items;
    }

    public void Step()
    {
        int i = 0;
        foreach(GameObject itemBar in itemBars){
            Vector3 pos = itemBar.transform.localPosition;
            float x = pos.x - stepOffset;
            float y = pos.y;
            float z = pos.z;

            Vector3 vector3 = new Vector3(x, y, z);
            itemBar.transform.localPosition = vector3;
            itemBar.GetComponent<ItemBar>().SetStartPosition(x, y);

            if(i == 0)
            {
                itemBar.GetComponent<ItemBar>().Unblock();
            }

            i++;
        }
    }
}
