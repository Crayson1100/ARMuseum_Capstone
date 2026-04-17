using UnityEngine;

public class RoamingRobot : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;
    public float rotateSpeed = 5f;

    private int index = 0;
    private int direction = 1;

    void Update()
    {
        if (points.Length == 0) return;

        Transform target = points[index];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        Vector3 directionToTarget = (target.position - transform.position).normalized;

        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotateSpeed * Time.deltaTime
            );
        }

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            index += direction;

            if (index == points.Length - 1 || index == 0)
            {
                direction *= -1;
            }
        }
    }
}


