using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 700;
    [SerializeField] float _maxDragDistance = 1.5f;
    [SerializeField] ParticleSystem _particleSystem;

    [SerializeField] Sprite spriteNormal;
    [SerializeField] Sprite spriteFly;
    [SerializeField] Sprite spriteAngry;
    [SerializeField] Sprite spriteDead;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    PolygonCollider2D _polygonCollider;
    SpriteRenderer _spriteRenderer;
    bool _hasDied;
    // Animator _animator;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        // _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void Update()
    {
        if (_rigidbody2D.velocity.magnitude >= 5f)
        {
            float angle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;
        _spriteRenderer.sprite = spriteAngry;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
    
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);

        _spriteRenderer.color = Color.white;
        _spriteRenderer.sprite = spriteFly;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }else{
            _launchForce = 500 * distance;
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidbody2D.position = desiredPosition;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (!_hasDied)
        {
            _rigidbody2D.freezeRotation = false;
            _hasDied = true;
            // _animator.SetBool("Dead",true);
            _spriteRenderer.sprite = spriteDead;
            StartCoroutine(ResetAfterDelay());
        }
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(2);
        _rigidbody2D.simulated = false;
        _particleSystem.Play();
        _spriteRenderer.enabled = false;
        _rigidbody2D.freezeRotation = true;
        _rigidbody2D.isKinematic = true;
        _polygonCollider.enabled = false;
        yield return new WaitForSeconds(2);
        _rigidbody2D.simulated = true;
        // _animator.SetBool("Dead", false);
        _polygonCollider.enabled = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.rotation = 0;
        _rigidbody2D.position = _startPosition;
        yield return new WaitForSeconds(1);
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = spriteNormal;
        _hasDied = false;
    }
}
