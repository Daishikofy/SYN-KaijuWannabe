using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider _collider;

    private bool wasEaten = false;

    private void OnValidate()
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
    }

    // Start is called before the first frame update
    void Start() 
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
            float angle = Random.value * Mathf.PI;
        Vector3 force = new Vector3(Mathf.Cos(angle) * 0.5f, 3, Mathf.Sin(angle) * 0.5f);
        rb.AddForce(force , ForceMode.Impulse);
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            rb.isKinematic = true;
            _collider.isTrigger = true;
            GetComponent<Animation>().Play();
        }
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
