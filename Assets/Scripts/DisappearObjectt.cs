using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearObjectt : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("DisappearObject script attached to: " + gameObject.name);
    }

    public void Disappear()
    {
        Debug.Log("Disappear method called on: " + gameObject.name);
        gameObject.SetActive(false);
    }

}
