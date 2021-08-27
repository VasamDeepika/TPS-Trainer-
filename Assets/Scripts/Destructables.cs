using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructables : MonoBehaviour
{
    [SerializeField]
    private GameObject crateDestroyed;

    public void OnCrateDestroy()
    {
        Instantiate(crateDestroyed, transform.position, transform.rotation);
    }

}
