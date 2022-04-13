using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTimeTest : MonoBehaviour
{
    private static float lastFrameTime;

    // Start is called before the first frame update
    void Start()
    {
        lastFrameTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - lastFrameTime);
        Debug.Log("my delta time: " + t + " system delta time: " + Time.deltaTime);
        lastFrameTime = Time.time;
    }
}
