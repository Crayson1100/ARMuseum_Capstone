using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    public float speed = 1f;
    public float width = 5f;
    public float height = 3f;
    public float forwardOffset = 0f;

    private float t = 0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime * speed;

        float x = width * Mathf.Sin(t);
        float z = height * Mathf.Sin(t) * Mathf.Cos(t);

        Vector3 targetPos = startPos + new Vector3(x, 0, z + forwardOffset);
        transform.position = targetPos;

        Vector3 nextPos = startPos + new Vector3(
            width * Mathf.Sin(t + 0.1f),
            0,
            height * Mathf.Sin(t + 0.1f) * Mathf.Cos(t + 0.1f)
        );

        Vector3 direction = transform.position - nextPos;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}