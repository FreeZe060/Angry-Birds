using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float damageVelocity = 3f;
    [SerializeField] Sprite injuredSprite;

    SpriteRenderer _spriteRenderer;
    int _health = 2;
    bool _isDead = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;

        float collisionForce = collision.relativeVelocity.magnitude;
        if (collisionForce >= damageVelocity)
        {
            _health -= 1;
        }
        if (collisionForce >= damageVelocity*2 || _health <= 0)
        {
            StartCoroutine(Die());
        }
        UpdateSprite();
    }

    void UpdateSprite(){
        if (_health == 1)
        {
            _spriteRenderer.sprite = injuredSprite;
        }
    }

    IEnumerator Die()
    {
        _isDead = true;
        _spriteRenderer.sprite = injuredSprite;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
