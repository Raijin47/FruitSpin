using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    public Transform pathObject;
    public float speed = 1f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float journeyLength;
    private float startTime;

    void Start()
    {
        float objectSize = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        startPosition = pathObject.position - pathObject.right * (pathObject.localScale.x + objectSize);
        endPosition = pathObject.position + pathObject.right * (pathObject.localScale.x + objectSize);


        journeyLength = Vector3.Distance(startPosition, endPosition);

        startTime = Time.time;

        transform.position = startPosition;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        if (fractionOfJourney >= 1f)
        {
            startTime = Time.time;
            transform.position = startPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        }
    }
}
