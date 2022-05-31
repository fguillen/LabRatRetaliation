using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ScreenController
{
    List<MapObjectController> mapObjects;
    public int ScreenIndex { get; private set; }

    public ScreenController(int screenIndex, List<MapObjectController> mapObjects)
    {
        Debug.Log($"XXX: Creating ScreenController. index: {screenIndex}, numObjects: {mapObjects.Count()}");
        this.mapObjects = mapObjects;
        this.ScreenIndex = screenIndex;
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
    [SerializeField] Transform playerTransform;

    int numScreens = 8;
    int screenUnits = 12;
    int actualScreenIndex;

    List<ScreenController> screenControllers = new List<ScreenController>();

    void Awake()
    {
        BuildScreenControllers();
        actualScreenIndex = -1;
    }

    void Update()
    {
        CheckForScreenChange();
    }

    void CheckForScreenChange()
    {
        int playerScreenIndex = Mathf.FloorToInt(playerTransform.position.x / screenUnits);
        if(playerScreenIndex != actualScreenIndex)
        {
            Debug.Log($"XXX: newScreen: {playerScreenIndex}");
            var playerSide = PlayerScreenOppositeSide(playerScreenIndex, playerTransform.position.x);
            ActivateObjectsInScreenByIndex(playerSide, playerScreenIndex);
            actualScreenIndex = playerScreenIndex;
        }
    }

    string PlayerScreenOppositeSide(int screenIndex, float playerPositionX)
    {
        var screenCenterPosition = ((screenIndex * screenUnits) + (screenUnits / 2));
        if(playerTransform.transform.position.x < screenCenterPosition)
            return "right";
        else
            return "left";
    }

    void BuildScreenControllers()
    {
        GameObject[] mapObjectsInArray = GameObject.FindGameObjectsWithTag("MapObject");

        List<MapObjectController> mapObjects = new List<MapObjectController>();
        foreach (var item in mapObjectsInArray)
        {
            mapObjects.Add(item.GetComponent<MapObjectController>());
        }

        Debug.Log($"XXX: Num of MapObjects: {mapObjects.Count()}");

        for (int i = 0; i < numScreens; i++)
        {
            var mapObjectsInTheScreen = MapObjectsInTheScreenByIndex(mapObjects, i);
            var screenController = new ScreenController(i, mapObjectsInTheScreen);
            screenControllers.Add(screenController);
        }
    }

    List<MapObjectController> MapObjectsInTheScreenByIndex(List<MapObjectController> mapObjects, int screenIndex)
    {
        float minX = screenIndex * screenUnits;
        float maxX = minX + screenUnits;

        Debug.Log($"XXX: index: {screenIndex}, X:({minX},{maxX})");

        return mapObjects.Where(e => e.transform.position.x > minX && e.transform.position.x < maxX).ToList();
    }

    public void ActivateObjectsInScreenByIndex(string side, int screenIndex)
    {
        screenControllers.ForEach(e => e.StopMapObjects());
        screenControllers[screenIndex].StartMapObjects(side);
    }
}
