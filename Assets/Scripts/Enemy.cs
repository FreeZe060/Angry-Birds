using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float damageVelocity = 3f;
    [SerializeField] Sprite injuredSprite;
    [SerializeField] ParticleSystem _particleSystem;

    GameManager _gameManager;
    SpriteRenderer _spriteRenderer;
    PolygonCollider2D _polygonCollider;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    int _health = 2;
    bool _isDead = false;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;
        float collisionForce = collision.relativeVelocity.magnitude;

        if (collisionForce >= damageVelocity)
        {
            _health -= 1;
            UpdateSprite();
        }
        if (collisionForce >= damageVelocity*2 || _health <= 0 || collision.gameObject.CompareTag("Bird"))
        {
            StartCoroutine(Die());
        }
    }

    void UpdateSprite(){
        if (_health == 1)
        {
            _spriteRenderer.sprite = injuredSprite;
            _animator.SetBool("isInjured", true);
        }
    }

    IEnumerator Die()
    {
        _isDead = true;
        _spriteRenderer.sprite = injuredSprite;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        _rigidbody2D.freezeRotation = true;
        _spriteRenderer.enabled = false;
        _rigidbody2D.simulated = false;
        _polygonCollider.enabled = false;
        _particleSystem.Play();
        
        _gameManager.EnemyEliminated();
    }
}
