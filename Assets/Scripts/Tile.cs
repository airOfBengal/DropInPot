using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ball collide with tile");
        Vector3 targetPos = GameManager.instance.droppableObjects[GameManager.currentDropObject].transform.position;
        GameManager.currentDropObject++;
        Arc arc = GameManager.instance.ball.GetComponent<Arc>();
        float travelDuration = 1f / GameManager.instance.forceToBall;
        StartCoroutine(arc.TravelArc(targetPos, travelDuration));
    }
}
