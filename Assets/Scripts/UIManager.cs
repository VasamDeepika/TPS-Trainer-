using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private GameObject coins;
    public void UpdateAmmo(int count)
    {
        ammoText.text = "Ammo: " + count;
    }

    public void CollectedCoins()
    {
        coins.SetActive(true);
    }

    public void RemovedCoin()
    {
        coins.SetActive(false);
    }
}
