using System.Collections.Generic;
using UnityEngine;

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
            Application.Quit();
    }

    public void UpdateProgress()
    {
        objectsDropped++;
        float progress = objectsDropped / (float)droppableObjectCount;
        //Debug.Log("progress: " + progress.ToString("0.00"));
        uiManager.progressImage.fillAmount = progress;
    }
}
