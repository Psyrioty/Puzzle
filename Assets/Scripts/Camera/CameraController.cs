using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController: MonoBehaviour
{
    //-------------------ДЛЯ ДВИЖЕНИЯ КАМЕРЫ--------------------
    private float mouseClickStartX, mouseClickStartY;
    private bool clicked = false;
    private bool pause = false;
    [SerializeField] private Transform target;
    [SerializeField] private float stepModifier = 0.1F;
    [SerializeField] private float maxMoveX = 10;
    [SerializeField] private float maxMoveY = 10;
    [SerializeField] private float minMoveX = -10;
    [SerializeField] private float minMoveY = -10;
    [SerializeField] private bool blockMoveX = false;
    [SerializeField] private bool blockMoveY = false;
    //===========================================================






    void Update()
    {
        MoveCamera();
    }










    //--------------ДВИЖЕНИЕ КАМЕРЫ--------------------------
    public void StopCamera()
    {
        pause = true;
    }

    public void ReturnCamera()
    {
        pause = false;
    }

    private void MoveCamera()
    {
        CheckClick();

        if (pause)
        {
            return;
        }
        
        Move();
    }

    private void Move()
    {
        if (!clicked)
        {
            return;
        }

        Vector3 move = Vector3.zero;

        if(!blockMoveX){
            float x = (mouseClickStartX - Mouse.current.position.ReadValue().x) * stepModifier;
            move.x += x;

            if(move.x + target.position.x > maxMoveX)
            {
                move.x = 0;
            }else if(move.x + target.position.x < minMoveX)
            {
                move.x = 0;
            }
        }

        if(!blockMoveY){
            float y = (mouseClickStartY - Mouse.current.position.ReadValue().y) * stepModifier;
            move.y += y;

            if(move.y + target.position.y > maxMoveY)
            {
                move.y = 0;
            }else if(move.y + target.position.y < minMoveY)
            {
                move.y = 0;
            }
        }
       
        target.position += move;

        UpdateMouseCoords();
    }

    private void UpdateMouseCoords()
    {
        mouseClickStartX = Mouse.current.position.ReadValue().x;
        mouseClickStartY = Mouse.current.position.ReadValue().y;
    }

    private void CheckClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            clicked = true;
        UpdateMouseCoords();
        }else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clicked = false;
        UpdateMouseCoords();
        }

    }
    //=======================================================
}
