using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character;
    private AudioSource audioSource;
    [SerializeField]
    private float playerspeed = 5;
    private float gravity = 9.8f;
    [SerializeField]
    private GameObject muzzlePrfeab;
    [SerializeField]
    private AudioClip[] audioClip;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float timer;
    [SerializeField]
    private GameObject hitMarketPrefab;
    [SerializeField]
    private GameObject shootPrefab;
    public static PlayerMovement instance;

    public int bulletCount = 50;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //raycast from the centre of main camera
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            if (Input.GetMouseButton(0))
            {
                timer = 0f;
                if (bulletCount > 0)
                {
                    ShootGun();
                }
                else
                {
                    print("Not enough bullets");
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

    }

    public void ShootGun()
    {
        bulletCount--;
        muzzlePrfeab.SetActive(true);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        ShootPool.instance.AddParticleEffect(shootPrefab);
        ShootPool.instance.Spawning();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("raycast got hit" + hit.transform.name);
            ParticlePool.instance.AddParticleEffect(hitMarketPrefab);
            ParticlePool.instance.Spawning(hit);
            //GameObject temp1 = (GameObject)Instantiate(hitMarketPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(temp1, 2.0f);

            audioSource.clip = audioClip[1];
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