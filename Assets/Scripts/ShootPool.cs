using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPool : MonoBehaviour
{
    public Stack<GameObject> shootPool = new Stack<GameObject>();
    public GameObject shootEffectPrefab;
    public GameObject currentPrefab;
    public GameObject firePoint;
    public static ShootPool instance;

    private void Awake()
    {
        instance = this;
    }

    public void CreatePool()
    {
        //print("Pool created ");
        shootPool.Push(shootEffectPrefab);
        shootPool.Peek().SetActive(false);
        shootPool.Peek().tag = "ShootEffect";
    }

    public void AddParticleEffect(GameObject effectTemp)
    {
        //print("Added shoot effect to pool");
        shootPool.Push(effectTemp);
        shootPool.Peek().SetActive(false);
    }

    public void Spawning()
    {
        //print("spawning shoot effect");
        if (shootPool.Count <= 1)
        {
            CreatePool();
        }
        GameObject temp = shootPool.Pop();
        if (temp.tag == "ShootEffect")
        {
            temp.SetActive(true);
            temp.transform.position = firePoint.transform.position;
            currentPrefab = temp;
        }
    }
}
