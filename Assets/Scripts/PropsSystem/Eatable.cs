using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;

    public int objectLevel = 0;

    private bool wasEaten = false;

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
        objectLevel = KaijuUtils.GetLevel(_collider.bounds);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.Log("WTF");
            return;
        }
        GameManager.instance.onPlayerLevelChanged.AddListener(OnPlayerLevelChanged);
        if (GameManager.instance.currentPlayerLevel >= objectLevel)
        {
            _collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasEaten) return;
        if (other.CompareTag("Player"))
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

    private void OnPlayerLevelChanged(int newLevel)
    {
        if (newLevel >= objectLevel)
        {
            _collider.isTrigger = true;
        }
    }
}
