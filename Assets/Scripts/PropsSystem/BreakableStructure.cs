using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableStructure : MonoBehaviour
{
    [SerializeField]
    private Collectible[] itemsToSpawn;
    [SerializeField]
    private List<WeakPoint> weakPoints;

    private int weakPointDestroyedCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var weakPoint in weakPoints)
        {
            weakPoint.onBroken.AddListener(OnWeakPointDestroyed);
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
}
