using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField]
    private float lifetime;

    private void Start()
    {
        StartCoroutine(DestroyAfterSeconds(lifetime));
    }

    // Destroys the gameObject that this is attached to after the given amount of seconds
    private IEnumerator DestroyAfterSeconds(float _lifetime)
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(this.gameObject);
    }
}
