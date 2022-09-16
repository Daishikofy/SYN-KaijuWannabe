using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableStructure : MonoBehaviour
{
    [SerializeField]
    private Collectible[] itemsToSpawn;
    [SerializeField]
    private List<WeakPoint> weakPoints;
    [SerializeField]
    private int objectLevel = 0;

    [SerializeField]
    private Collider _collider;

    private int weakPointDestroyedCounter = 0;
    private bool wasEaten = false;

    #region EDITOR SETUP

    [Button("SetAutomaticLevel")]
    public int setMyLevel;
    public void SetAutomaticLevel()
    {
        objectLevel = 0;
        foreach (var weakPoint in weakPoints)
        {
            objectLevel += weakPoint.objectLevel;
        }
    }
    
    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (var weakPoint in weakPoints)
        {
            weakPoint.onBroken.AddListener(OnWeakPointDestroyed);
        }
        GameManager.instance.onPlayerLevelChanged.AddListener(OnPlayerLevelChanged);

        if (objectLevel == 0)
        {
            SetStructureToTrigger();
        }
    }

    public void OnWeakPointDestroyed()
    {
        weakPointDestroyedCounter++;
        if (weakPointDestroyedCounter >= weakPoints.Count)
        {
            foreach (var weakPoint in weakPoints)
            {
                weakPoint.StructureBroke();
            }
            DestroyStructure();
        }
    }

    public void DestroyStructure()
    {
        foreach (var item in itemsToSpawn)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

// Functions related to eatable phase

    private void OnPlayerLevelChanged(int newLevel)
    {
        if (newLevel > objectLevel * 2)
        {
            SetStructureToTrigger();
            GameManager.instance.onPlayerLevelChanged.RemoveListener(OnPlayerLevelChanged);
        }
    }

    private void SetStructureToTrigger()
    {
        foreach (var weakPoint in weakPoints)
        {
            weakPoint._collider.enabled = false;
        }
        _collider.isTrigger = true;
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!wasEaten && other.CompareTag("Player"))
        {
            var hit = other.GetComponentInParent<PlayerController>();
            if (hit != null)
            {
                hit.Eat();
                wasEaten = true;
                Destroy(this.gameObject);
            }
        }
    }
}
