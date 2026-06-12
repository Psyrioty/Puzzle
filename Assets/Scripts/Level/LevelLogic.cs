using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour
{

    [SerializeField] private List<GameObject> itemBars;
    [SerializeField] private List<GameObject> items;
    [SerializeField] private GameObject itemBarPrefab;
    [SerializeField] private Transform camera;
    [SerializeField] private float stepOffset = 1200;
    [SerializeField] private GameObject bar;
    [SerializeField] private Scrollbar scrollbar;
    private int maxPoints = 0; //это максимальный прогресс
    private int progress = 0; //прогресс
    [SerializeField] private GameObject menu;

    //------------------звук----------------------
    [SerializeField] private List<AudioClip> trueMoveSounds; //когда вставил в нужную ячейку
    [SerializeField] private List<AudioClip> victorySounds; //звуки победы
    [SerializeField] private AudioSource otherAudio;//создание других звуков
    //=============================================

    private int level;

    void Start()
    {
        FindAllItems();
        maxPoints = items.Count;

        level = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        CheckMouseDown();
    }


    //ищу самый верхний коллайдер
    private void CheckMouseDown()
    {
        if (!Pointer.current.press.wasPressedThisFrame)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(
            Pointer.current.position.ReadValue()
        );

        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        if (hits.Length == 0)
            return;

        SpriteRenderer topSprite = null;
        SpriteMask topMask = null;
        GameObject parent = null;

        foreach (Collider2D hit in hits)
        {
            SpriteRenderer sprite = hit.GetComponent<SpriteRenderer>();
            if(sprite == null)
            {
                sprite = hit.transform.GetComponentInChildren<SpriteRenderer>();
                if(sprite != null)
                {
                    parent = hit.gameObject;
                }
            }

            SpriteMask mask = hit.GetComponent<SpriteMask>();
            if(sprite == null)
            {
                mask = hit.transform.GetComponent<SpriteMask>();
            }

            if(sprite == null && mask == null)
            {
                continue;
            }

            if(topSprite == null)
            {
                topSprite = sprite;
            }

            if (topSprite != null && sprite != null){
                if(topSprite.sortingOrder < sprite.sortingOrder)
                {
                    topSprite = sprite;
                }
            }

            if(topMask == null)
            {
                topMask = mask;
            }
        }

        if(parent != null)
        {
            ClickGameObject(parent);
            return;
        }

        if(topSprite == null && topMask != null)
        { 
            ClickGameObject(topMask.gameObject);
            return;
        }

        if(topSprite == null)
        {
            return;
        }

        ClickGameObject(topSprite.gameObject);
    }

    //ищу нажатый коллайдер
    private void ClickGameObject(GameObject gameObject)
    {
        if(gameObject == null)
        {
            return;
        }

        foreach(GameObject target in items)
        {
            if(target == gameObject)
            {
                target.GetComponent<Item>().Click();
                return;
            }
        }

        foreach(GameObject target in itemBars)
        {
            if(target == gameObject)
            {
                target.GetComponent<ItemBar>().Click();
                return;
            }
        }

        if(bar == gameObject)
        {
            bar.GetComponent<Bar>().Click();
        }
    }


    //поиск и создание Item и ItemBar
    private void FindAllItems()
    {
        float x = 0;
        float y = -1700;

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

    //правильно поставил
    public void Step()
    {
        GetComponent<RandomSound>().PlaySoundRandom(trueMoveSounds, otherAudio);
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


            
        progress++;
        float size = (float)progress / (float)maxPoints;
        scrollbar.size = size;

        if(size >= 1)
        {
            GetComponent<RandomSound>().PlaySoundRandom(victorySounds, otherAudio);
            menu.SetActive(true);
            bar.SetActive(false);
            camera.GetComponent<CameraController>().StopCamera();
            PlayerPrefs.SetInt("CurrentLevel", level);
            PlayerPrefs.Save();
        }
    }
}
