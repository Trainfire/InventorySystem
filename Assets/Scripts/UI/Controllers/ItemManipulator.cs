using UnityEngine;

public class ItemManipulator : MonoBehaviour
{
    public GameObject Model;
    public float Speed;

    private bool mouseDown;

    public void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButtonDown(0))
            mouseDown = true;

        if (Input.GetMouseButtonUp(0))
            mouseDown = false;

        if (mouseDown)
            Model.transform.Rotate(new Vector3(y, x, 0) * Time.deltaTime * Speed);
    }

    public void Reset()
    {
        Model.transform.rotation = Quaternion.identity;
    }
}
