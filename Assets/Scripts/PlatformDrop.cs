using System.Collections;
using UnityEngine;

public class PlatformDrop : MonoBehaviour
{

    public float dropWindow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropWindow = 0.20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(FallTimer());
        }
    }

    IEnumerator FallTimer()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(dropWindow);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

}
