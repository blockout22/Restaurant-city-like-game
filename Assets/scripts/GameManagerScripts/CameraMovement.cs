using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    public int zoomIncrements = 5;
    //used for camera zooming
    private Vector3 lastPos;
    //used for camera movement
    private Vector3 cursorStartPos;

    Dictionary<string, int> d = new Dictionary<string, int>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Camera.main.transform.position = new Vector3(10.5f, 5.42f, -2.51f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            lastPos = Camera.main.transform.position;
            //Camera.main.transform.LookAt(Vector3.forward);
            Camera.main.transform.Translate(0, 0, scroll * zoomIncrements, Space.Self);

            if (Physics.CheckSphere(Camera.main.transform.position, 1))
            {
                moveCameraBack();
            }

            if (Camera.main.transform.position.y > 15)
            {
                moveCameraBack();
            }
        }

        //if (Tool.getSelected() == Tool.selection.CURSOR)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cursorStartPos = GetWorldPosition(0);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = cursorStartPos - GetWorldPosition(0);
                Camera.main.transform.position += direction;
            }
        }
    }

    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    private void moveCameraBack()
    {
        Camera.main.transform.position = lastPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Camera.main.transform.position, 1f);
    }
}
