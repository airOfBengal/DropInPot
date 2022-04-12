using UnityEngine;

public class Pot : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject ball = collision.gameObject;
        ball.SetActive(false);
        Destroy(ball, 0.5f);
        Debug.Log("Ball is potted!");
    }
}
