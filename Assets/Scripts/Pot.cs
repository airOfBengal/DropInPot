using UnityEngine;
using UnityEngine.SceneManagement;

public class Pot : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.ShakeCamera();
        GameManager.instance.UpdateProgress();
        GameObject ball = collision.gameObject;
        ball.SetActive(false);
        Destroy(ball, 0.5f);

        if (GameManager.instance.progress < 1.0f)
        {
            GameManager.instance.SetGameOverPanelActive();
            return;
        }

        GameManager.instance.PlaySparkShowerParticleEffect();
        GameManager.instance.LoadNextLevel();
    }
}
