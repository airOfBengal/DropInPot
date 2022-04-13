using UnityEngine;

public class Pot : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.ShakeCamera();
        GameManager.instance.UpdateProgress();
        GameObject ball = collision.gameObject;
        ball.SetActive(false);
        Destroy(ball, 0.5f);
        Debug.Log("Ball is potted!");
    }
}
