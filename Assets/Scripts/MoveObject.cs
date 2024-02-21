using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject target;

    public void Move()
    {
        if (gameObject != null && target != null)
        {
            gameObject.SetActive(false);
            target.SetActive(true);
        }
        else
        {
            Debug.LogError("MoveObject script is missing either the source or target GameObject reference.");
        }
    }


}
