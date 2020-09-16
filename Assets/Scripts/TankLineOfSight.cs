using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankLineOfSight : MonoBehaviour
{
    //called when something enters the trigger collider
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponentInParent<TankEnemy>().player = col.transform;
            Debug.Log("SEE PLAYER RUN AT PLAYER");
        }
    }
}
