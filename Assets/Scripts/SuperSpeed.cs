using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    #region Speed_variables
    [SerializeField]
    [Tooltip("the movespeed upgrade")]
    private int speedamount;
    #endregion

    #region Speed_functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().setSpeed(speedamount);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
