using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //ontriggerstay, if its palyer he can collect
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if(player!=null)
                {
                    player.hasCoin = true;
                    Destroy(this.gameObject);
                    UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                    if(uiManager!=null)
                    {
                        uiManager.CollectedCoins();
                    }
                    Debug.Log("Collected coin");
                    //Destroy(this.gameObject);
                }
            }
        }
    }
}
