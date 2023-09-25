using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeringHandler : MonoBehaviour
{
    [SerializeField] private int sortingInt = 5000;
    [SerializeField] private int offset = 0;
    [SerializeField] private Renderer m_Renderer;

    private void Awake()
    {
        m_Renderer = gameObject.GetComponent<Renderer>();

    }

    private void LateUpdate()
    {
        m_Renderer.sortingOrder = (int)(sortingInt - transform.position.y - offset);
    }
}
