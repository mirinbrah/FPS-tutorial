using UnityEngine;
using System.Collections.Generic;

public class TriggerMover : MonoBehaviour
{
    [System.Serializable]
    public class MovingObject
    {
        public Transform transform;
        public Vector3 offset;
        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public Vector3 endPos;
    }

    public List<MovingObject> objectsToMove;
    public float speed = 2f;
    public float toggleCooldown = 1.0f;
    public string playerTag = "Player";

    private bool isOpen = false;
    private float nextToggleTime = 0f;

    void Start()
    {
        foreach (var obj in objectsToMove)
        {
            if (obj.transform != null)
            {
                obj.startPos = obj.transform.position;
                obj.endPos = obj.transform.position + obj.offset;
            }
        }
    }

    void Update()
    {
        foreach (var obj in objectsToMove)
        {
            if (obj.transform != null)
            {
                Vector3 target = isOpen ? obj.endPos : obj.startPos;
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && Time.time >= nextToggleTime)
        {
            isOpen = !isOpen;
            nextToggleTime = Time.time + toggleCooldown;
        }
    }
}