using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public Transform centerPoint;
    public float speed = 10.0f;
    public float radius = 10.0f;

    private float angle;

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float z = centerPoint.position.z + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
