using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Animator))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Transform[] _targets;
    [SerializeField] private float _speed;

    private SpriteRenderer _sprite;

    private Animator _animator;

    private int _currentTarget;

    private const string _animationParameter = "AnimState";

    private int _currentState = 2;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        _animator.SetInteger(_animationParameter, _currentState);
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, _targets[_currentTarget].position, _speed * Time.deltaTime);

        if (transform.position == _targets[_currentTarget].position)
        {
            _currentTarget = (++_currentTarget) % _targets.Length;
        }

        if (transform.position.x < _targets[_currentTarget].position.x)
            _sprite.flipX = true;
        else
            _sprite.flipX = false;
    }
}
