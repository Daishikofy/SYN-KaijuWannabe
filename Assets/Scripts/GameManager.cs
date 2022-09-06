using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    [SerializeField]
    private int scoreBeforeCameraMovement = 10;

    private int currentScore;

    public int currentPlayerLevel { get; private set;  } = 0;
    public UnityEvent<int> onPlayerLevelChanged;
    private CameraController cameraController;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void AddToScore(int value)
    {
        currentScore += value;
        if (currentScore >= scoreBeforeCameraMovement)
        {
            cameraController.MoveBackward();
            scoreBeforeCameraMovement += scoreBeforeCameraMovement + 5;
        }
        //Update UI
    }

    public void UpdatePlayerLevel(int newLevel)
    {
        currentPlayerLevel = newLevel;
        onPlayerLevelChanged.Invoke(currentPlayerLevel);
    }
}
