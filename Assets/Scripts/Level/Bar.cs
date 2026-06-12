using UnityEngine;
using UnityEngine.InputSystem;

public class Bar : MonoBehaviour
{
    private bool clicked = false;
    [SerializeField] private CameraController cameraController;
    //нажал
    public void Click()
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

        if (Pointer.current.press.wasReleasedThisFrame)
        {
            clicked = false;
            cameraController.ReturnCamera();
        }

    }
}
