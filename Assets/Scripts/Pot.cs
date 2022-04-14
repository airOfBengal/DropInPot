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
            GameManager.instance.uiManager.gameOverPanel.SetActive(true);
            return;
        }

        GameManager.instance.PlaySparkShowerParticleEffect();

        if (GameManager.instance.levelManager.GetCurrentLevelNo() == GameManager.instance.levelManager.totalLevel)
        {
            GameManager.instance.uiManager.gameOverPanel.SetActive(true);
            GameManager.instance.uiManager.congratsGO.SetActive(true);
            return;
        }

        GameManager.instance.LoadNextLevel();
    }
}
