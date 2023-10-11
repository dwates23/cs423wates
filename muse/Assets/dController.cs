using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dController : MonoBehaviour
{
    public Material dissolveMaterial;
    public float dissolveSpeed = 60.0f;
    private float currentDissolveAmount = 0.0f;

    // Detect when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check for the "Dissolvable" tag.
        {
            StartCoroutine(DissolveObject());
        }
    }

    // Coroutine for gradual dissolution
    private IEnumerator DissolveObject()
    {
        while (currentDissolveAmount < 1.0f)
        {
            currentDissolveAmount += dissolveSpeed * Time.deltaTime;
            dissolveMaterial.SetFloat("_DissolveAmount", currentDissolveAmount);
            yield return null;
        }

        // The object has fully dissolved; you can destroy or hide it at this point.
        gameObject.SetActive(false);
    }
}