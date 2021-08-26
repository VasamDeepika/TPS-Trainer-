using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character;
    [SerializeField]
    private float playerspeed = 5;
    private float gravity = 9.8f;
    [SerializeField]
    private GameObject muzzlePrfeab;
    [SerializeField]
    private GameObject hitMarketPrefab;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //raycast from the centre of main camera
        if (Input.GetMouseButton(0))
        {
            muzzlePrfeab.SetActive(true);
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("raycast got hit" + hit.transform.name);
                GameObject temp = (GameObject)Instantiate(hitMarketPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(temp, 2.0f);
                audioSource.clip = audioClip[1];
                audioSource.Play();
                audioSource.loop = false;
            }
        }
        else
        {
            muzzlePrfeab.SetActive(false);
            audioSource.clip = audioClip[0];
            audioSource.Play();
            audioSource.loop = false;
        }

    }
    private void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * playerspeed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);
        character.Move(velocity * Time.deltaTime);
    }
}