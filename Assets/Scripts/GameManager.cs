using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] droppableObjects;
    public static int currentDropObject;
    public GameObject ball;
    public float forceToBall = 10f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDropObject = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPos = droppableObjects[currentDropObject].transform.position;
            currentDropObject++;
            Arc arc = ball.GetComponent<Arc>();
            float travelDuration = 1f / forceToBall;
            StartCoroutine(arc.TravelArc(targetPos, travelDuration));
        }
    }
}
