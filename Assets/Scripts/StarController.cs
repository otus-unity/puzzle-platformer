using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] GameObject m_collectEffect;

    public bool Collected { get; set; }

    public void OnCollect()
    {
        Collected = true;
        m_collectEffect.transform.SetParent(null);
        m_collectEffect.SetActive(true);
        Destroy(gameObject);
        Destroy(m_collectEffect, 1.0f);
    }
}
