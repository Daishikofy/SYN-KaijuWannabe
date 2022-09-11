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

    private bool isBroken = false;

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
        objectLevel = KaijuUtils.GetLevel(_collider.bounds);
    }

    public void Attacked()
    {
        if (isBroken)
            return;
        if (GameManager.instance.currentPlayerLevel >= objectLevel)
        {
            lifePoints--;
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
        isBroken = true;
    }

    public void StructureBroke()
    {
        Break();
    }
}
