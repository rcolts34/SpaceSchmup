using JetBrains.Annotations;
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
    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0, // 0000 in binary
        offRight = 1, // 0001 in binary
        offLeft = 2, // 0010 in binary
        offUp = 4, // 0100 in binary
        offDown = 8 // 1000 in binary
    }
    public enum eType { center, inset, outset };

    [Header("Inscribed")]

    public eType boundsType = eType.center;

    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    //public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        // b
        camWidth = camHeight * Camera.main.aspect;
        // c
    }
   void LateUpdate()
    // d
    {
        float checkRadius = 0;
        if (boundsType == eType.inset) { checkRadius = -radius; }

        if (boundsType == eType.outset) { checkRadius = radius; }

        // LateUpdate is called after all other GameObjects have called Update()

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;
        //isOnScreen = true;

        // Restrict the X position to camWidth

        if (pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            screenLocs |= eScreenLocs.offRight;
            //isOnScreen = false;
        }

        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            screenLocs |= eScreenLocs.offLeft;
            //isOnScreen = false;
        }

        // Restrict the Y position to camHeight

        if (pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            screenLocs |= eScreenLocs.offUp;
            //isOnScreen = false;
        }

        if (pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            screenLocs |= eScreenLocs.offDown;
            //isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;
            //isOnScreen = true;
        }
    }
    public bool isOnScreen
    {
        get { return (screenLocs == eScreenLocs.onScreen); }
    }

    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkLoc) == checkLoc);
    }
}   
