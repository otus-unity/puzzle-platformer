using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D m_rigidBody2D;

    [SerializeField] Collider2D m_leftCollider;
    [SerializeField] Collider2D m_rightCollider;
    [SerializeField] LayerMask m_groundLayers;

    [SerializeField] SpriteRenderer m_sprite;

    [SerializeField] float m_speed;

    float m_dir;

    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_dir = 1.0f;
    }

    void Update()
    {
        if (m_dir > 0.0f && !m_rightCollider.IsTouchingLayers(m_groundLayers))
            m_dir = -1.0f;
        else if (m_dir < 0.0f && !m_leftCollider.IsTouchingLayers(m_groundLayers))
            m_dir = 1.0f;

        m_sprite.flipX = m_dir < 0.0f;

        var velocity = m_rigidBody2D.velocity;
        velocity.x = m_dir * m_speed;
        m_rigidBody2D.velocity = velocity;
    }
}
