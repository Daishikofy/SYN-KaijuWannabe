using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable
{
    [SerializeField]
    private int lifePoints = 1;
    [SerializeField]
    private Collectible[] itemsToSpawn;
    [SerializeField]
    private Animation noDamageAnimation;

    public int objectLevel { get; private set; }

    private Collider _collider;

    [Button("Damage", true)]
    public int damageMe;

    [Button("Restore")]
    public int restoreMe;

    #region DEBUG Functions
    public void Restore()
    {
        gameObject.SetActive(true);
    }
    #endregion

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
        objectLevel = (int) (_collider.bounds.size.x * _collider.bounds.size.y * _collider.bounds.size.z);
    }

    public void Attacked()
    {
        if (GameManager.instance.currentPlayerLevel >= objectLevel)
        {
            if (lifePoints <= 0)
            {
                Break();
            }
        }
        else
        {
            ReceivesNoDamages();
        }
    }

    public void ReceivesNoDamages()
    {
        //noDamageAnimation.Play();
        Debug.Log("Lol! Im fine");
    }

    private void Break()
    {
        foreach (var item in itemsToSpawn)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

    public void StructureBroke()
    {
        Break();
    }
}
