using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 3.75f;
    public GameObject gameBackgroundOne;
    public Vector3 backgroundVectorOne;
    public float backgroundFloatTweakOne = 0.1f;
    public GameObject gameBackgroundTwo;
    public Vector3 backgroundVectorTwo;
    public float backgroundFloatTweakTwo = 9.0f;


    void Start()
    {
        // set Player object to field
        playerTransform = GameObject.Find("Player").transform;
        offset.y = 1.0f;
        offset.z = -7.5f;
        gameBackgroundOne = GameObject.Find("Background_One");
        // gameBackgroundTwo = GameObject.Find("Background_Two");
    }

    private void LateUpdate()
    {
        // camera effect
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        //
        // background layer one
        // speed up background relative to camera - adjust with backgroundFloatTweakOne
        Vector3 SmoothedPosition_BgOne = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * backgroundFloatTweakOne * Time.deltaTime);
        backgroundVectorOne.Set(SmoothedPosition_BgOne.x, SmoothedPosition_BgOne.y, 1.0f);
        gameBackgroundOne.transform.position = backgroundVectorOne;

        //
        // background layer two
        // speed up background relative to camera - adjust with backgroundFloatTweakTwo
    //     Vector3 SmoothedPosition_BgTwo = Vector3.Lerp(transform.position *2, desiredPosition - offset, -smoothSpeed * backgroundFloatTweakTwo * Time.deltaTime);
    //     backgroundVectorTwo.Set(SmoothedPosition_BgTwo.x, SmoothedPosition_BgTwo.y, 2.0f);
    //     gameBackgroundTwo.transform.position = backgroundVectorTwo;
    }
} 