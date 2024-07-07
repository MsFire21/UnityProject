using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 5.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newX = startPosition.x + Mathf.PingPong(Time.time * speed, distance) - (distance / 2);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
