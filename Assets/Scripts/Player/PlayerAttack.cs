using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    private void OnValidate()
    {
        if (playerController == null)
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IBreakable>();
        if (hit != null)
        {
           hit.Attacked();
        }
        else
        {
            hit = GetComponentInParent<IBreakable>();
            if (hit != null)
            {
                hit.Attacked();
            }
        }
    }
}
