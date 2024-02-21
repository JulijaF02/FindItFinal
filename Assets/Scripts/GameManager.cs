using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public DisappearObjectt disappearObject;
    public MoveObject moveObject;
    public ChangeColorObject changeColorObject;
    public SwitchObject switchObject;
    public LampController lampController;

    public GameObject[] livingRoomObjects;
    public GameObject[] bedroomObjects;
    public GameObject[] kitchenObjects;
    private float eventInterval = 17f; // Time interval between events (in seconds)
    private float timeSinceLastEvent = 0f;
    public bool gameIsOver = false; // Flag to track if the game is over

    HashSet<GameObject> objectsWithEvents = new HashSet<GameObject>();


    void Update()
    {
        if (!gameIsOver)
        {
            timeSinceLastEvent += Time.deltaTime;

            if (timeSinceLastEvent >= eventInterval)
            {
                TriggerRandomEvent();
                timeSinceLastEvent = 0f;
            }
        }
    }

    void TriggerRandomEvent()
    {
        // Select a random room
        GameObject[] roomObjects = GetRandomRoomObjects();

        if (roomObjects != null && roomObjects.Length > 0)
        {
            // Filter out objects that have already had an event triggered on them
            List<GameObject> availableObjects = new List<GameObject>();
            foreach (GameObject obj in roomObjects)
            {
                if (!objectsWithEvents.Contains(obj))
                {
                    availableObjects.Add(obj);
                }
            }

            if (availableObjects.Count > 0)
            {
                // Select a random object from the available objects
                GameObject obj = availableObjects[Random.Range(0, availableObjects.Count)];
                Debug.Log("Selected object: " + obj.name);

                // Get the list of scripts attached to the object
                MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();

                if (scripts != null && scripts.Length > 0)
                {
                    // Select a random script from the list
                    MonoBehaviour script = scripts[Random.Range(0, scripts.Length)];

                    // Trigger the event based on the script attached to the object
                    if (script is DisappearObjectt)
                    {
                        ((DisappearObjectt)script).Disappear();
                    }
                    else if (script is MoveObject)
                    {
                        ((MoveObject)script).Move();
                    }
                    else if (script is ChangeColorObject)
                    {
                        ((ChangeColorObject)script).ChangeColor();
                    }
                    else if(script is SwitchObject)
                    {
                        ((SwitchObject)script).Swap();
                    }
                    
                }

                // Add the object to the set of objects with events triggered
                objectsWithEvents.Add(obj);

                // Update counters and UI
                UpdateCountersAndUI(obj);
            }
            else
            {
                Debug.Log("No available objects to trigger events on.");
                uiManager.showWinPanel();
            }
        }
    }

    GameObject[] GetRandomRoomObjects()
    {
        // Select a random room and return its objects
        int randomRoom = Random.Range(0, 3); // 0: Living Room, 1: Bedroom, 2: Kitchen
        switch (randomRoom)
        {
            case 0:
                return livingRoomObjects;
            case 1:
                return bedroomObjects;
            case 2:
                return kitchenObjects;
            default:
                return null;
        }
    }

    void UpdateCountersAndUI(GameObject obj)
    {
        if (IsInRoom(obj, livingRoomObjects))
        {
            uiManager.IncrementCounter("Living Room");
        }
        else if (IsInRoom(obj, bedroomObjects))
        {
            uiManager.IncrementCounter("Bedroom");
        }
        else if (IsInRoom(obj, kitchenObjects))
        {
            uiManager.IncrementCounter("Kitchen");
        }
    }

    bool IsInRoom(GameObject obj, GameObject[] roomObjects)
    {
        return System.Array.Exists(roomObjects, element => element == obj);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        gameIsOver = true;
    }


}
