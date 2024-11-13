using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotationsPerSecond = 0.1f;

    [Header("Dynamic")]
    public int levelshown = 0;

    // This non-public variable will not appear in the Inspector
    Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    void Update()
    {
        // Read the current shield level from the Hero Singleton
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);

        // If this is diffrent from levelShown
        if(levelshown != currLevel)
        {
            levelshown = currLevel;
            // Adjust the texture offest to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f*levelshown, 0);
        }
        // Rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationsPerSecond*Time.time*360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
