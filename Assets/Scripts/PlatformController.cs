using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] Transform m_top;
    [SerializeField] Transform m_bottom;
    [SerializeField] float m_speed;

    float m_current; // текущая позиция от 0 до 1
    float m_dir; // направление движения: либо 1, либо -1

    void Start()
    {
        m_current = 0.0f;
        m_dir = 1.0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    void Update()
    {
        m_current += m_dir * m_speed * Time.deltaTime;
        if (m_current > 1.0f) {
            m_current = 1.0f;
            m_dir = -1.0f;
        } else if (m_current < 0.0f) {
            m_current = 0.0f;
            m_dir = 1.0f;
        }

        transform.position = Vector3.Lerp(m_top.position, m_bottom.position, m_current);
    }
}
