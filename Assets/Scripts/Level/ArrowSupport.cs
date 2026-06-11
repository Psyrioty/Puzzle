using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ArrowSupport : MonoBehaviour
{
    private List<GameObject> items;
    [SerializeField] private int speed;
    private Vector3 min, max;
    SpriteRenderer sprite;

    void Start()
    {
        LevelLogic levelLogic = transform.parent.GetComponent<LevelLogic>();
        items = levelLogic.GetItems();
        sprite = GetComponentInChildren<SpriteRenderer>();
    } 


    void Update()
    {
        SupportArrow();
    }


    //--------------------НАПРАВЛЯЮЩЕЯ ПОМОЩНИК-------------------
    private void SupportArrow()
    {
        CheckCameraPoints();
        MoveToItem();
        CheckTargetInCamera();
    }

    private void CheckTargetInCamera()
    {
        if(items.Count == 0)
        {
            if (sprite.enabled)
            {
                sprite.enabled = false;
            }
            return;
        }

        Transform target = items[0].transform;
        
        if(target == null)
        {
            return;
        }

        Vector3 pos = target.position;

        if(
            !(
                pos.x <= max.x &&
                pos.y <= max.y &&
                pos.x >= min.x &&
                pos.y >= min.y
            )
        )
        {
            if (!sprite.enabled)
            {
                sprite.enabled = true;
            }
            return;
        }

        if (sprite.enabled)
        {
            sprite.enabled = false;
        }
    }

    private void CheckCameraPoints()
    {
        Camera cam = Camera.main;

        min = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        max = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    private void MoveToItem()
    {
        if(items.Count == 0)
        {
            return;
        }

        Transform target = items[0].transform;

        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);


        transform.position = Vector3.MoveTowards
        (
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        Vector3 pos = transform.position;

        float x = pos.x;
        float y = pos.y;

        if(
            pos.x < min.x
        )
        {
            x = min.x;
        }else if(
            pos.x > max.x
        )
        {
            x = max.x;
        }

        if(
            pos.y < min.y
        )
        {
            y = min.y;
        }else if(
            pos.y > max.y
        )
        {
            y = max.y;
        }

        if(
            x == pos.x &&
            y == pos.y
        )
        {
            return;
        }

        transform.position = new Vector3(x, y, 0);
    }
    //=============================================================
}
