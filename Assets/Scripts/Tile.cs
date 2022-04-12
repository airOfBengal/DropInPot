using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ball collide with tile");
        GameObject target = GameManager.instance.GetNearestTile(gameObject);
        if(target == null)
        {
            target = GameManager.instance.pot;
        }

        gameObject.GetComponent<Renderer>().material.color = Color.green;

        Vector3 targetPos = target.transform.position;
        Arc arc = GameManager.instance.ball.GetComponent<Arc>();
        arc.gameObject.GetComponent<Rigidbody>().useGravity = false;
        float travelDuration = 1f / GameManager.instance.forceToBall;
        StartCoroutine(arc.TravelArc(targetPos, travelDuration));
    }
}
