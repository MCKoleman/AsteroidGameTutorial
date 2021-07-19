using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;
    [SerializeField]
    private float distanceMod;

    private const float SPEED_MOD = 0.1f;

    void Update()
    {
        // Parallax background
        foreach(Transform child in this.transform)
        {
            child.GetComponent<Renderer>().material.mainTextureOffset 
                += new Vector2(
                    Time.deltaTime * SPEED_MOD * speed.x / (1 + (Mathf.Pow(child.position.z, 2) * distanceMod)),
                    Time.deltaTime * SPEED_MOD * speed.y / (1 + (Mathf.Pow(child.position.z, 2) * distanceMod))
            );
        }
    }
}
