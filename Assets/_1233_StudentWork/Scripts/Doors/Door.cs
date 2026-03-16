using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 openOffset = new Vector3(0, 3, 0);
    [SerializeField] private float speed = 2f;

    private Vector3 _closedPos;
    private Vector3 _openPos;
    private bool _isOpen;

    private void Start()
    {
        _closedPos = transform.position;
        _openPos = _closedPos + openOffset;
    }

    private void Update()
    {
        Vector3 target = _isOpen ? _openPos : _closedPos;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
    }

    public void OpenDoor()
    {
        _isOpen = true;
    }

    public void CloseDoor()
    {
        _isOpen = false;
    }
}
