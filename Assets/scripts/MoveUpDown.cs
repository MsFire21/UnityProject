using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 3.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.PingPong(Time.time * speed, distance) - (distance / 2);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
