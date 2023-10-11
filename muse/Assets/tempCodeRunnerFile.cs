using System.Collections;
using System.Collections.Generic;
// Attach this script to your pyramid object
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Material dissolveMaterial;
    public float dissolveSpeed = 0.5f;
    private float currentDissolveAmount = 2.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // You can use any interaction trigger.
        {
            currentDissolveAmount += dissolveSpeed * Time.deltaTime;
            dissolveMaterial.SetFloat("_DissolveAmount", currentDissolveAmount);

            if (currentDissolveAmount >= 1.0f)
            {
                // The object has fully dissolved; you can destroy or hide it at this point.
                gameObject.SetActive(false);
            }
        }
    }
}
