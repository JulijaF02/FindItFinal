using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoomIndicator : MonoBehaviour
{
    public TextMeshProUGUI roomText;
    public GameObject livingRoomCamera;
    public GameObject bedroomCamera;
    public GameObject kitchenCamera;

    void Update()
    {
        if (IsInRoom(livingRoomCamera))
        {
            roomText.text = "camera_livingroom";
        }
        else if (IsInRoom(bedroomCamera))
        {
            roomText.text = "camera_bedroom";
        }
        else if (IsInRoom(kitchenCamera))
        {
            roomText.text = "camera_kitchen";
        }
        else
        {
            roomText.text = "Unknown Room";
        }
    }

    bool IsInRoom(GameObject roomCamera)
    {
        return roomCamera.activeSelf;
    }
}
