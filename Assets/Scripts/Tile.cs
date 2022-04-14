using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject nextTileGO = null;
    private bool collided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            collided = true;
            GameManager.instance.PlayHitParticleEffect(transform);
            GameManager.instance.ShakeCamera();

            Debug.Log("Ball collide with tile " + gameObject.name);
            GameObject target = nextTileGO;
            if (target == null)
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
}
