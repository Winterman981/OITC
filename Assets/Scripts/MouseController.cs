using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject targetCamera;
    public float cameraHorizontalSpeed = 1.0f;
    public float cameraVerticalSpeed = 1.0f;

    public float cameraVerticalMax = 80f;
    public float cameraVerticalMin = -10f;

    float horizontalToCam = 0f;
    float verticalToCam = 0f;

    bool onFirstCalculate = true;

    Vector3 cameraOriginalPosition = new Vector3(0, 0, 0);
    Vector3 cameraOriginalRotation = new Vector3(0, 0, 0);
    Vector3 cameraLocationOffset = new Vector3(0, 0, 0);
    Vector3 cameraRotationOffset = new Vector3(0, 0, 0);

    void Start()
    {
        Vector3 playerLocation = gameObject.transform.position;
        Vector3 playerLocationNoY = new Vector3(playerLocation.x, 0, playerLocation.z);

        Vector3 camLocation = targetCamera.transform.position;
        Vector3 camLocationNoY = new Vector3(camLocation.x, 0, camLocation.z);

        horizontalToCam = Vector3.Distance(playerLocationNoY, camLocationNoY);

        Vector3 playerLocationNoX = new Vector3(0, playerLocation.y, playerLocation.z);
        Vector3 camLocationNoX = new Vector3(0, camLocation.y, camLocation.z);

        verticalToCam = Vector3.Distance(playerLocationNoX, camLocationNoX);

        Cursor.visible = false;
    }

    void Update()
    {
        HorizontalUpdate();
        VerticalUpdate();

        CameraOffset();
        CameraCollision();
    }

    void HorizontalUpdate()
    {
        float mouseAxisUpdate_X = Input.GetAxis("Mouse X");
        mouseAxisUpdate_X = mouseAxisUpdate_X * cameraHorizontalSpeed;
        Vector3 currentCameraAngle = targetCamera.transform.eulerAngles;
        float nextCameraAngle = currentCameraAngle.y + mouseAxisUpdate_X;

        targetCamera.transform.eulerAngles = new Vector3(currentCameraAngle.x, nextCameraAngle, currentCameraAngle.z);

        float nextCameraRadian = nextCameraAngle * Mathf.PI / 180f;
        float nextCameraPosX = gameObject.transform.position.x + (horizontalToCam * Mathf.Sin(nextCameraRadian));
        float nextCameraPosZ = gameObject.transform.position.z + (horizontalToCam * Mathf.Cos(nextCameraRadian));

        targetCamera.transform.position = new Vector3(nextCameraPosX, targetCamera.transform.position.y, nextCameraPosZ);
    }

    void VerticalUpdate()
    {
        float mouseAxisUpdate_Y = Input.GetAxis("Mouse Y");
        mouseAxisUpdate_Y = mouseAxisUpdate_Y * cameraHorizontalSpeed;

        Vector3 currentCameraAngle = targetCamera.transform.eulerAngles;
        float nextCameraAngle = currentCameraAngle.x - mouseAxisUpdate_Y;

        if (nextCameraAngle > 90)
        {
            nextCameraAngle = nextCameraAngle - 360f;
        }

        if (nextCameraAngle > cameraVerticalMax)
        {
            nextCameraAngle = cameraVerticalMax;
        }

        if (nextCameraAngle < cameraVerticalMin)
        {
            nextCameraAngle = cameraVerticalMin;
        }

        targetCamera.transform.eulerAngles = new Vector3(nextCameraAngle, currentCameraAngle.y, currentCameraAngle.z);

        float nextCameraRadian = (nextCameraAngle + 180f) * Mathf.PI / 180f;
        float nextCameraPosY = gameObject.transform.position.y + (verticalToCam * Mathf.Sin(nextCameraRadian));
        float nextCameraPosZ = verticalToCam * Mathf.Cos(nextCameraRadian);

        nextCameraPosZ += verticalToCam;

        Vector3 currentCameraLocation = targetCamera.transform.position;
        Vector3 cameraForwardNoY = targetCamera.transform.forward;

        cameraForwardNoY = new Vector3(cameraForwardNoY.x, 0, cameraForwardNoY.z);// No Y because it will forward point down.
        cameraForwardNoY = cameraForwardNoY.normalized;         // normalized to get only direction not size.
        currentCameraLocation += cameraForwardNoY * nextCameraPosZ;

        targetCamera.transform.position = new Vector3(currentCameraLocation.x, nextCameraPosY, currentCameraLocation.z);
    }

    void CameraOffset()
    {
        if (onFirstCalculate == true)
        {
            onFirstCalculate = false;
            cameraLocationOffset = cameraOriginalPosition - targetCamera.transform.position;
            cameraRotationOffset = cameraOriginalRotation - targetCamera.transform.eulerAngles;
        }

        Vector3 rightDirection = new Vector3(targetCamera.transform.right.x, 0, targetCamera.transform.right.z);
        Vector3 upDirection = new Vector3(0, targetCamera.transform.up.y, 0);
        Vector3 forwardDirection = new Vector3(targetCamera.transform.forward.x, 0, targetCamera.transform.forward.z);

        Vector3 offsetToRight = rightDirection * cameraLocationOffset.x;
        Vector3 offsetToUp = upDirection * cameraLocationOffset.y;
        Vector3 offsetToForward = forwardDirection * cameraLocationOffset.z;

        targetCamera.transform.position += offsetToRight + offsetToUp + offsetToForward;
        targetCamera.transform.eulerAngles += cameraRotationOffset;
    }

    void CameraCollision()
    {
        RaycastHit hitInfo;     // This one will get Collision Information the the RayCase will get.

        // PlayerLocation with some offset to point ray from top of the head of player character.
        Vector3 playerLocation = gameObject.transform.position + new Vector3(0, 1.5f, 0);
        Vector3 cameraLocation = targetCamera.transform.position;
        Vector3 directionPlayerToCamera = (cameraLocation - playerLocation).normalized;
        float distancePlayerToCamera = Vector3.Distance(playerLocation, cameraLocation);

        //Debug.DrawRay( StartLocation, Direction * RayDistance , Color );
        Debug.DrawRay(playerLocation, directionPlayerToCamera * distancePlayerToCamera, Color.red);

        //UnityEngine.Physics.Raycast(StartLocation, Direction, out , RayDistance);
        if (Physics.Raycast(playerLocation, directionPlayerToCamera, out hitInfo, distancePlayerToCamera))
        {
            // Wall Collision Check
            Vector3 hitLocation_Wall = hitInfo.point;
            Vector3 hitLocation_WallNoY = new Vector3(hitLocation_Wall.x, 0, hitLocation_Wall.z);
            Vector3 playerLocationNoY = new Vector3(playerLocation.x, 0, playerLocation.z);
            Vector3 cameraLocationNoY = new Vector3(cameraLocation.x, 0, cameraLocation.z);

            float distancePlayerToHitPointNoY = Vector3.Distance(playerLocationNoY, hitLocation_WallNoY);
            float distancePlayerToCameraNoY = Vector3.Distance(playerLocationNoY, cameraLocationNoY);
            float distanceDifferentNoY = distancePlayerToCameraNoY - distancePlayerToHitPointNoY;
            // Right here we now know how far we should move the camera to not block by the wall.

            Vector3 cameraForwardNoY = targetCamera.transform.forward;
            cameraForwardNoY = new Vector3(cameraForwardNoY.x, 0, cameraForwardNoY.z);
            cameraForwardNoY = cameraForwardNoY.normalized;         // Get only Direction. Remove size and length.

            // Ceiling Collision Check
            float hitLocation_Ceiling = hitInfo.point.y;
            float cameraLocation_Y = cameraLocation.y;
            float distanceDifferent_Y = cameraLocation_Y - hitLocation_Ceiling; // How far we have to move down from ceiling.
            Vector3 cameraMoveDownVector = new Vector3(0, -distanceDifferent_Y, 0);

            // Move it forward to the front of the Wall.
            targetCamera.transform.position += (cameraForwardNoY * distanceDifferentNoY) + cameraMoveDownVector;
        }

    }
}
