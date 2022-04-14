using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<GameObject> droppableObjects;
    public GameObject ball;
    public GameObject pot;
    public float forceToBall = 10f;
    private int droppableObjectCount;
    private int objectsDropped;
    public UIManager uiManager;
    public LevelManager levelManager;
    public CameraShake cameraShake;
    public float cameraShakeDuration = 0.25f;
    public float cameraShakeMagnitude = 0.4f;
    public GameObject hitParticleGO;
    public GameObject sparkShowerParticleGO;
    public Transform sparkShowerParent;
    public float progress;

    private void Awake()
    {
        Init();
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        pot = GameObject.FindGameObjectWithTag("Pot");
        droppableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        for(int i = 1; i < droppableObjects.Count; i++)
        {
            Tile tile = droppableObjects[i - 1].GetComponent<Tile>();
            tile.nextTileGO = droppableObjects[i];
        }
        droppableObjectCount = droppableObjects.Count + 1; // 1 is for pot
        objectsDropped = 0;

        uiManager.leftLabelText.text = levelManager.GetCurrentLevelNo().ToString();
        uiManager.rightLabelText.text = levelManager.GetNextLevelNo().ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.ExitApp();
        }
    }

    public void UpdateProgress()
    {
        objectsDropped++;
        progress = objectsDropped / (float)droppableObjectCount;
        Debug.Log("progress: " + progress.ToString("0.00"));
        uiManager.progressImage.fillAmount = progress;
    }

    public void ShakeCamera()
    {
        StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude));
    }

    public void PlayHitParticleEffect(Transform parent)
    {
        GameObject hitParticle = Instantiate(hitParticleGO, parent);
        Destroy(hitParticle, 0.5f);
    }

    public void PlaySparkShowerParticleEffect()
    {
        GameObject spark = Instantiate(sparkShowerParticleGO, sparkShowerParent);
        //Destroy(spark, 0.6f);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    IEnumerator LoadNextLevelCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        levelManager.LoadNextLevel();
    }
}
