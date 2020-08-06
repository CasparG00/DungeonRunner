using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveStep;
    public float wallDistance;

    private Transform _tf;
    public bool isInteracting;
    private EventManager _em;

    private void Start()
    {
        _tf = transform;
        _em = GetComponent<EventManager>();
    }

    private void Update()
    {
        if (isInteracting) return;
        if (MoveDir(KeyCode.W, KeyCode.UpArrow, Vector2.up))
        {
            Movement(Vector3.up);
            _em.lastMove = Vector3.down;
        }

        if (MoveDir(KeyCode.S, KeyCode.DownArrow, Vector2.down))
        {
            Movement(Vector3.down);
            _em.lastMove = Vector3.up;
        }

        if (MoveDir(KeyCode.D, KeyCode.RightArrow, Vector2.right))
        {
            Movement(Vector3.right);
            _em.lastMove = Vector3.left;
        }

        if (MoveDir(KeyCode.A, KeyCode.LeftArrow, Vector2.left))
        {
            Movement(Vector3.left);
            _em.lastMove = Vector3.right;
        }
    }

    private bool MoveDir(KeyCode moveKey, KeyCode altMoveKey, Vector2 dir)
    {
        var position = _tf.position;
        var hit = Physics2D.Raycast(new Vector2(position.x, position.y), dir, wallDistance);
        return Input.GetKeyDown(moveKey) ^ Input.GetKeyDown(altMoveKey) && hit.collider == null;
    }

    public void Movement(Vector3 dir) => _tf.position += dir * moveStep;
}