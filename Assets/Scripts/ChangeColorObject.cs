using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorObject : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        rend.material.color = Random.ColorHSV();
    }


}
