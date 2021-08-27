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
                    Debug.Log("Collected coin");
                }
            }
        }
    }
}
