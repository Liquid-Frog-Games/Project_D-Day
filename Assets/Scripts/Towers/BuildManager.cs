using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;
    [SerializeField] public UnityEvent towerSelect;

    private int selectedTower;

    private void Awake()
    {
        main = this;
        selectedTower = -1;
    }
  
    public Tower GetSelectedTower()
    {
        if(selectedTower == -1)
        {
            Debug.Log("Not selected");
            return null;
        }
        return towers[selectedTower]; 
    }

    public void SetSelectedTower(int _selectedTower)
    {
       selectedTower = _selectedTower;
    }
}
