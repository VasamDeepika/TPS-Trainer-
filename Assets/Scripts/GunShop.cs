using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShop : MonoBehaviour
{
    //if collision is player and have coin inventory needs to be updated

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if(player!=null)
                {
                    if(player.hasCoin==true)
                    {
                        Debug.Log("Collected Gun");
                        player.hasCoin = false;
                        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                        if(uiManager!=null)
                        {
                            uiManager.RemovedCoin();
                        }
                        player.EnableWeapon();
                    }
                }
            }
        }
    }

}
