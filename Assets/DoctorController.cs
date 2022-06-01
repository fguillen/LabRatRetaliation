using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoctorController : MonoBehaviour
{
    List<GameObject> stairs;
    GameObject closestStair;
    Rigidbody2D rb;
    [SerializeField] float speed;
    CharacterOnTheStairsController characterOnTheStairsController;

    void Awake()
    {
        stairs = GameObject.FindGameObjectsWithTag("Stairs").ToList();

        rb = GetComponent<Rigidbody2D>();
        characterOnTheStairsController = GetComponent<CharacterOnTheStairsController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveToClosestStair();

        characterOnTheStairsController.onTouchingStairs.AddListener(TouchingStairs);
        characterOnTheStairsController.onEndOfStairs.AddListener(AtEndOfStairs);
    }

    void MoveToClosestStair()
    {
        closestStair = ClosestStair();
        Debug.Log($"XXX: closestStair: {closestStair.transform.position}");
        rb.velocity = new Vector2(closestStair.transform.position.x - transform.position.x, 0).normalized * speed;
    }

    GameObject ClosestStair()
    {
        List<GameObject> stairsInMyFloor = stairs.Where(e => Mathf.Abs(e.transform.position.y - transform.position.y) < 1).ToList();
        return stairsInMyFloor.OrderBy(e => Mathf.Abs(e.transform.position.x - transform.position.x)).First();
    }

    void TouchingStairs(string side)
    {
        if(side == "down" && !characterOnTheStairsController.onStairsWalking)
            WalkTheStairs();
    }

    void WalkTheStairs()
    {
        characterOnTheStairsController.WalkTheStairs();
    }

    void AtEndOfStairs()
    {
        MoveToClosestStair();
    }
}
