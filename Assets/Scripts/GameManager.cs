using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> droppableObjects;
    public GameObject ball;
    public GameObject pot;
    public float forceToBall = 10f;
    public bool isBallKinematic = false;

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

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 targetPos = GetNearestTile(ball).transform.position;
        //    Arc arc = ball.GetComponent<Arc>();
        //    float travelDuration = 1f / forceToBall;
        //    StartCoroutine(arc.TravelArc(targetPos, travelDuration));
        //}
    }

    public GameObject GetNearestTile(GameObject source)
    {
        droppableObjects.Remove(source);
        float minDistance = float.MaxValue;
        GameObject target = null;
        foreach(GameObject go in droppableObjects)
        {
            float distance = Vector3.Distance(source.transform.position, go.transform.position);
            if (distance < minDistance)
            {
                target = go;
                minDistance = distance;
            }
        }
        return target;
    }
}
