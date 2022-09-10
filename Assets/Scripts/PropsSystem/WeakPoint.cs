using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeakPoint : MonoBehaviour, IBreakable
{
    [HideInInspector]
    public UnityEvent onBroken;

    [SerializeField]
    private int lifePoints = 1;

    private bool isBroken = false;

    public void Attacked()
    {
        if (--lifePoints <= 0 && !isBroken)
        {
            Break();
        }
    }

    private void Break()
    {
        isBroken = true;
        GetComponent<MeshRenderer>().material.color = Color.red;
        onBroken.Invoke();
    }

    public void StructureBroke()
    {
        gameObject.SetActive(false);
    }

}
