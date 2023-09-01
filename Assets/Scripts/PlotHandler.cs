using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    void Start()
    {
        startColor = sr.color;
    }

    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        Debug.Log("Build Tower Here, " + name);
    }
}
