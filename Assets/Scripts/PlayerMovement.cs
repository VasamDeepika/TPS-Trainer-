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
    [SerializeField] float timer = 1.0f;
    [SerializeField]
    private GameObject hitMarketPrefab;
    [SerializeField]
    private GameObject shootPrefab;
    public static PlayerMovement instance;
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool isReloading = false;
    public bool hasCoin = false;
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
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
                if (currentAmmo > 0)
                {
                    ShootGun();
                    audioSource.clip = audioClip[0];
                    audioSource.Play();
                    audioSource.loop = false;
                }
                else
                {
                    print("Not enough bullets");
                }
            }
            else
            {
                muzzlePrfeab.SetActive(false);

            }
        }
        if(Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    public void ShootGun()
    {
        muzzlePrfeab.SetActive(true);
        currentAmmo--;
        uiManager.UpdateAmmo(currentAmmo);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        ShootPool.instance.AddParticleEffect(shootPrefab);
        ShootPool.instance.Spawning();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log("raycast got hit" + hit.transform.name);
            ParticlePool.instance.AddParticleEffect(hitMarketPrefab);
            ParticlePool.instance.Spawning(hit);
            //GameObject temp1 = (GameObject)Instantiate(hitMarketPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(temp1, 2.0f);

            audioSource.clip = audioClip[1];
            audioSource.Play();
            audioSource.loop = false;

            //check if we had crate and then destroy
            Destructables crate = hit.transform.GetComponent<Destructables>();
            GunShop gunShop = GetComponent<GunShop>();
            if(crate!=null)
            {
                if (gunShop.hasGun == true)
                {
                    crate.OnCrateDestroy();
                }
            }

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
    //reload bullts
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        currentAmmo = maxAmmo;
        uiManager.UpdateAmmo(currentAmmo);
        isReloading = false;
    }

    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }
}