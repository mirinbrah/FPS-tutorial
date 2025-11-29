using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public Transform objectToMove;
    public Vector3 offset = new Vector3(0, 3, 0);
    public float speed = 2f;
    public string playerTag = "Player";

    private bool isOpen = false;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        if (objectToMove != null)
        {
            startPos = objectToMove.position;
            endPos = startPos + offset;
        }
    }

    void Update()
    {
        Vector3 target;

        // Vector3 target = isOpen ? endPos : startPos;
        // objectToMove.position = Vector3.MoveTowards(objectToMove.position, target, speed * Time.deltaTime);

        if (isOpen)
        {
            target = endPos;
        }
        else
        {
            target = startPos;
        }

        if (objectToMove != null)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, target, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isOpen = !isOpen;
        }
    }
}