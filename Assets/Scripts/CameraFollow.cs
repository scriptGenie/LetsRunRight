using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 3.75f;
    public GameObject gameBackground;
    public Vector3 backgroundVector;


    void Start()
    {
        // set Player object to field
        playerTransform = GameObject.Find("Player").transform;
        offset.y = 1.0f;
        offset.z = -7.5f;
        gameBackground = GameObject.Find("Background");
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //speed up background relative to camera
        Vector3 fastSmoothedPosition = Vector3.Lerp(transform.position, desiredPosition, ((smoothSpeed * 0.1f) * Time.deltaTime));
        transform.position = smoothedPosition;

        // tweak this to adjust game background - TODO - have this bounce randomly in relation to camera
        backgroundVector.Set(fastSmoothedPosition.x, fastSmoothedPosition.y, 1.0f);
        gameBackground.transform.position = backgroundVector;
    }
} 