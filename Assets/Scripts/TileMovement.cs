using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float duration = 10f;
    private float startTime;
    private int direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if(startTime < duration)
        {
            startTime += Time.deltaTime;
            transform.position = new Vector3(transform.position.x + Time.deltaTime * direction, transform.position.y, transform.position.z);
        }
        else
        {
            direction = -direction;
            startTime = 0;
        }
    }
}
