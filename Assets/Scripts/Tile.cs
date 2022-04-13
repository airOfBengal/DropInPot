using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject nextTileGO = null;

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.ShakeCamera();

        Debug.Log("Ball collide with tile");
        GameObject target = nextTileGO;
        if(target == null)
        {
            target = GameManager.instance.pot;
        }

        gameObject.GetComponent<Renderer>().material.color = Color.green;

        GameManager.instance.UpdateProgress();

        Vector3 targetPos = target.transform.position;
        Arc arc = GameManager.instance.ball.GetComponent<Arc>();
        arc.gameObject.GetComponent<Rigidbody>().useGravity = false;
        float travelDuration = 1f / GameManager.instance.forceToBall;
        StartCoroutine(arc.TravelArc(targetPos, travelDuration));
    }
}
