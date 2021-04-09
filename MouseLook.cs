using UnityEngine;

/// <summary>
/// Mouse look script for editor
/// </summary>
public class MouseLook : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_STANDALONE
	//Speeds the camera rotates
	[SerializeField]
    private float horizSpeed;
    [SerializeField]
    private float vertSpeed;
    [SerializeField]
    private Transform playerCamera;

    //Rotation amounts
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Vector3 followMouseRotation;

    //Lock the cursor to the center of the screen
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        followMouseRotation = new Vector3(yaw, pitch, 0.0f);
    }

    //Get the mouse movements and rotate the camera
    private void Update ()
    {
        pitch += horizSpeed * Input.GetAxis("Mouse X");
        yaw -= vertSpeed * Input.GetAxis("Mouse Y");

        followMouseRotation.x = yaw;
        followMouseRotation.y = pitch;

        playerCamera.eulerAngles = followMouseRotation;
	}
#endif
}