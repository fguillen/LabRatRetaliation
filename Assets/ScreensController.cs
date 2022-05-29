using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenController
{
    List<MapObjectController> mapObjects;


    public ScreenController(int index, GameObject[] _mapObjects)
    {
        foreach (var mapObject in _mapObjects)
        {
            mapObjects.Add(mapObject.GetComponent<MapObjectController>());
        }
    }

    public void StartMapObjects(string position)
    {
        mapObjects.ForEach(e => e.Spawn(position));
    }

    public void StopMapObjects()
    {
        mapObjects.ForEach(e => e.Stop());
    }
}

public class ScreensController : MonoBehaviour
{
    int numScreens = 8;
    GameObject[] mapObjects;
    List<ScreenController> screenControllers;


    void Awake()
    {
        mapObjects = GameObject.FindGameObjectsWithTag("MapObject");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void BuildScreenControllers(GameObject[] mapObjects)
    {
        for (int i = 0; i < numScreens; i++)
        {

        }
    }
}
