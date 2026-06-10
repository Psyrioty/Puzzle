using UnityEngine;
using UnityEngine.InputSystem;

public class Bar : MonoBehaviour
{
    private bool clicked = false;
    [SerializeField] private CameraController cameraController;
    //нажал
    void OnMouseDown()
    {
        clicked = true;
        cameraController.StopCamera();
    }

    void Update()
    {
        CheckClick();
    }


    //отпустил
    private void CheckClick()
    {
        if (!clicked)
        {
            return;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clicked = false;
            cameraController.ReturnCamera();
        }

    }
}
