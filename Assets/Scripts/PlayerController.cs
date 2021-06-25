using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rigidbody2D;

    [SerializeField] float m_walkSpeed;
    [SerializeField] float m_jumpSpeed;

    [SerializeField] Animator m_animator;
    [SerializeField] SpriteRenderer m_spriteRenderer;

    [SerializeField] Collider2D m_groundDetector;
    [SerializeField] LayerMask m_groundLayers;

    [SerializeField] TextMeshProUGUI m_starsText;

    [SerializeField] ParticleSystem m_blood;

    int m_starsCollected;

    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Star") {
            //collision.gameObject.SendMessage("OnCollect");
            if (collision.TryGetComponent<StarController>(out var star)) {
                if (!star.Collected) {
                    m_starsCollected++;
                    m_starsText.text = m_starsCollected.ToString();
                    star.OnCollect();
                }
            }
        }

        if (collision.tag == "Damage") {
            m_blood.Play();
        }
    }

    void Update()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        bool isAtGround = m_groundDetector.IsTouchingLayers(m_groundLayers);
        m_animator.SetBool("onGround", isAtGround);

        if (Mathf.Approximately(dx, 0.0f)) {
            m_animator.SetBool("walking", false);
        } else {
            m_animator.SetBool("walking", true);
            m_spriteRenderer.flipX = dx < 0.0f;
        }

        var velocity = m_rigidbody2D.velocity;

        velocity.x = dx * m_walkSpeed;
        if (dy > 0.0f && isAtGround)
            velocity.y = m_jumpSpeed;

        m_animator.SetFloat("verticalSpeed", velocity.y);

        m_rigidbody2D.velocity = velocity;
    }
}
