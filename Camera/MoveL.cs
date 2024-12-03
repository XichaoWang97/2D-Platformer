using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveL : MonoBehaviour
{
    public static bool isMoveL;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoveL = true;
        }
    }
}
