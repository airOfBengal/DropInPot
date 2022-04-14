using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("collide with plane");
            Destroy(gameObject, 0.2f);
            GameManager.instance.SetGameOverPanelActive();
        }
    }
}
