using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private Animator anim;
    private bool isShopOpen = true;

    private bool isAnimatorActive = false;
    public void ToggleShop()
    {
        if(isAnimatorActive == false){
            anim.enabled = true;
            isAnimatorActive = true;
            isShopOpen = !isShopOpen;
            anim.SetBool("ShopOpen", isShopOpen);  
        } else {
            isShopOpen = !isShopOpen;
            anim.SetBool("ShopOpen", isShopOpen);  
        }
        
        
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.coins.ToString();
    }
}
