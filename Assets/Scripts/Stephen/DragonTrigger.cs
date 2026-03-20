using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DragonTrigger : MonoBehaviour
{
    public Transform platform;      // The object that moves
    public float moveDistance = 20f; // How far up/down
    public float moveSpeed = 2f;    // Movement speed

    private Vector3 startPos;
    private Vector3 targetPos;
    private Coroutine moveRoutine;

    void Start()
    {
        startPos = platform.position;
        targetPos = startPos + Vector3.up * moveDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartMove(targetPos);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StartMove(startPos);
    }

    void StartMove(Vector3 destination)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveTo(destination));
    }

    IEnumerator MoveTo(Vector3 destination)
    {
        while (Vector3.Distance(platform.position, destination) > 0.01f)
        {
            platform.position = Vector3.MoveTowards(
                platform.position,
                destination,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        platform.position = destination;
    }
}
