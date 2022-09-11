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

    [SerializeField]
    public int objectLevel = 0;
    [SerializeField]
    private Bounds colliderBounds;

    
    public Collider _collider; 

    private bool isBroken = false;

    [Button("SetAutomaticLevel")]
    public int setMyLevel;
    public void SetAutomaticLevel()
    {
        objectLevel = KaijuUtils.GetLevel(_collider.bounds);
    }

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
            colliderBounds = _collider.bounds;
        }
    }

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
