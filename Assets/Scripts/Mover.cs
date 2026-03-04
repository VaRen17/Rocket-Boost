using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    [SerializeField] float delay;
    Vector3 startPosition;
    Vector3 endPosition;
    bool canMove = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
        Invoke("StartMove", delay);
    }

    void Update()
    {
        if(canMove)
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        if (transform.position == endPosition)
        {
            transform.position = startPosition;
        }
    }

    private void StartMove()
    {
        canMove = true;
    }
}
