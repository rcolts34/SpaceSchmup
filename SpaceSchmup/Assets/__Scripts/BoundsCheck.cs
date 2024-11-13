using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If you type /// in Visual Studio, it will automatically expand to a <summary>
/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an othrographic Main Camera.
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset };

    [Header("Inscribed")]

    public eType boundsType = eType.center;

    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
// b
        camWidth = camHeight * Camera.main.aspect;
// c
    }
    private void LateUpdate()
    // d
    {
        float checkRadius = 0;
        if (boundsType == eType.inset) { checkRadius = -radius; }

        if (boundsType == eType.outset) { checkRadius = radius; }

        // LateUpdate is called after all other GameObjects have called Update()

        Vector3 pos = transform.position;
        isOnScreen = true;

        // Restrict the X position to camWidth

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            isOnScreen = false;
        }

        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            isOnScreen = false;
        }

        // Restrict the Y position to camHeight

        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            isOnScreen = false;
        }

        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
        }


    }

}
