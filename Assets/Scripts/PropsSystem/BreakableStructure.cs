using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableStructure : MonoBehaviour
{

    [SerializeField]
    private List<WeakPoint> weakPoints;
    [SerializeField]
    private Breakable breakable;

    private int weakPointDestroyedCounter = 0;
    private void OnValidate()
    {
        GetComponentsInChildren(weakPoints);
        if (breakable == null)
            breakable = GetComponentInChildren<Breakable>();
    }
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
            breakable.StructureBroke();
            foreach (var weakPoint in weakPoints)
            {
                weakPoint.StructureBroke();
            }
        }
    }
}
