using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBar : MonoBehaviour
{
    private bool clicked = false;
    private GameObject levelLogic;
    
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Vector2 startPosition = new Vector2(0, -1460); 
    private Animator animator;
    [SerializeField] private GameObject item;
    private bool blocked = true;
    private float step = 1200;


    void Start()
    {
        CheckAnimator();
        cameraController = transform.parent.GetComponent<CameraController>();
    }

    void Update()
    {
        CheckClick();

        if (blocked)
        {
            return;
        }

        Move();
    }

    public void SetItem(GameObject item)
    {
        this.item = item;
    }

    public void SetStartPosition(float x, float y)
    {
        this.startPosition = new Vector2(x, y);
    }

    public void SetTexture(Sprite sprite)
    {
        GameObject image = transform.GetChild(0).GameObject();
        image.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Разблокировка ячейки
    public void Unblock()
    {
        blocked = false;
        UnblockedAnimation();
    }


    //нажал
    public void Click()
    {
        clicked = true;
        cameraController.StopCamera();

        if (blocked)
        {
            return;
        }

        StartMoveAnimation();
    }


    private void CheckCollider()
    {
        CircleCollider2D itemCollider = item.GetComponent<CircleCollider2D>();
        CircleCollider2D itemBarCollider = GetComponent<CircleCollider2D>();

        bool overlaps = itemCollider.IsTouching(itemBarCollider);

        if (!overlaps)
        {
            return;
        }

        item.GetComponent<Item>().PasteItem();
    }

    //----------------ПЕРЕМЕЩЕНИЕ------------------------
    //отпустил
    private void CheckClick()
    {
        if (!clicked)
        {
            return;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CheckCollider();

            clicked = false;
            cameraController.ReturnCamera();
            MoveLocalPosition(startPosition);
            EndMoveAnimation();
        }

    }


    private void Move()
    {
        if (!clicked)
        {
            return;
        }

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        MovePosition(mouseWorldPos);
    }

    private void MovePosition(Vector2 pos)
    {
        this.transform.position = pos;
    }

    private void MoveLocalPosition(Vector2 pos)
    {
        this.transform.localPosition = new Vector3(pos.x, pos.y, 10);
    }
    //=====================================================


    //--------------------------АНИМАЦИЯ-----------------------------
    private void StartMoveAnimation()
    {
        CheckAnimator();
        animator.SetBool("move", true);
    }

    private void EndMoveAnimation()
    {
        CheckAnimator();
        animator.SetBool("move", false);
    }

    private void UnblockedAnimation()
    {
        CheckAnimator();
        animator.SetBool("blocked", false);
    }

    private void CheckAnimator()
    {
        if(animator != null)
        {
            return;
        }

        animator = this.GetComponent<Animator>();
    }
    //==========================================================
}