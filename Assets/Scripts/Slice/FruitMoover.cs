using UnityEngine;

public class FruitMoover : MonoBehaviour
{
    public float speed = 5f;

    public Transform target;
    public Vector3 targetPosition;
    public bool isMoving = false;

    public Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = startPos;
        targetPosition = target.position;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    public void MoveTo(Vector2 target)
    {
        targetPosition = new Vector3(target.x, target.y, transform.position.z);
        isMoving = true;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
