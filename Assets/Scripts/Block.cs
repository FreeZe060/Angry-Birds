using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float damageVelocity = 7f;
    [SerializeField] Sprite injuredSprite;

    SpriteRenderer _spriteRenderer;
    int _health = 2;
    bool _isDestroyed = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDestroyed) return;

        float collisionForce = collision.relativeVelocity.magnitude;
        if (collisionForce >= damageVelocity)
        {
            _health -= 1;
        }
        if (collisionForce >= damageVelocity*2 || _health <= 0)
        {
            Die();
        }
        UpdateSprite();
    }

    void UpdateSprite(){
        if (_health == 1)
        {
            _spriteRenderer.sprite = injuredSprite;
        }
    }

    void Die()
    {
        _isDestroyed = true;
        Destroy(gameObject);
    }
}
