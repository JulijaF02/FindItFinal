using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    public GameObject otherObject;

    public void Swap()
    {
        Vector3 temp = transform.position;
        transform.position = otherObject.transform.position;
        otherObject.transform.position = temp;
    }


}
