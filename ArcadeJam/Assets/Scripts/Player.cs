using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {
    /// <summary>
    /// how fast the player moves
    /// </summary>
    [SerializeField]
    float speed;
    /// <summary>
    /// how fast the camera turns
    /// </summary>
    [SerializeField]
    float sensitivity;
    /// <summary>
    /// Stuff for caluclating camera rotation
    /// </summary>
    float xRot;
    float yRot;
    float yaw;
    float pitch;
    /// <summary>
    /// What gun is equppied
    /// </summary>
    public Gun equippedGun;
    private int gunIndex;
    /// <summary>
    /// Invetory for guns
    /// </summary>
    [SerializeField]
    Gun[] inventory = new Gun[3];
    /// <summary>
    /// Player animatior
    /// </summary>
    public Animator anim;
    [SerializeField]
    Transform camPosition;
    [SerializeField]
    Text ammoCount;
    /// <summary>
    /// WHere the gun should be postion
    /// </summary>
    [SerializeField]
    Transform gunPivot;
    [SerializeField]
    Transform rightHand;
    [SerializeField]
    Transform leftHand;

    public Effects.PlayerHit OnPlayerHitEffect;

    private Health health;

    // Use this for initialization
    void Start () {
        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i].transform.gameObject.SetActive(false);
        }
        gunIndex = 0;
        SwitchGun();
        ammoCount.text = equippedGun.Ammo.ToString();
        Cursor.lockState = CursorLockMode.Locked;
       
	}
	
	// Update is called once per frame
	void Update () {

        
        anim.SetFloat("Speed", Input.GetAxis("Vertical"));
        anim.SetFloat("Strafe", Input.GetAxis("Horizontal"));


        xRot = Input.GetAxis("Mouse X") * sensitivity;
        yRot = Input.GetAxis("Mouse Y") * sensitivity;
        yaw += xRot;
        pitch += yRot;
        pitch = Mathf.Clamp(pitch, -30, 30);
        Camera.main.transform.localEulerAngles = new Vector3(-pitch, yaw, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0f);
        Camera.main.transform.position = camPosition.position;
        gunPivot.parent = Camera.main.transform;

        CheckPlayerInput();

    }
    /// <summary>
    /// Checks button inputs
    /// </summary>
    void CheckPlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveBackward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetMouseButton(0))
        {
            equippedGun.Fire();
            ammoCount.text = equippedGun.Ammo.ToString();
        }
        if(Input.GetMouseButtonUp(0))
        {
            equippedGun.CoolDown = 0;
        }
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Hello?");
            equippedGun.Reload();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            equippedGun.transform.gameObject.SetActive(false);
            SwitchGun();
        }

    }

    void MoveForward()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime; 
    }
    void MoveBackward()
    {
        transform.position = transform.position + -transform.forward * speed * Time.deltaTime;
    }
    void MoveRight()
    {
        transform.position = transform.position + transform.right * speed * Time.deltaTime;
    }
    void MoveLeft()
    {
        transform.position = transform.position + -transform.right * speed * Time.deltaTime;
    }
    /// <summary>
    /// Switches gun
    /// </summary>
    void SwitchGun()
    {
        inventory[gunIndex].transform.gameObject.SetActive(true);
        equippedGun = inventory[gunIndex];
        ammoCount.text = equippedGun.Ammo.ToString();
        gunPivot = equippedGun.transform;
        rightHand = equippedGun.rightHand;
        leftHand = equippedGun.leftHand;
        gunIndex++;
        if(gunIndex == inventory.Length)
        {
            gunIndex = 0;
        }
    }
    /// <summary>
    /// Stuff for hands only fworks for pistol can ignore
    /// </summary>
    void OnAnimatorIK()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
    }    

    public void OnGetHit(float damage)
    {
        OnPlayerHitEffect();
        health.ChangeHealth(damage);
        
    }

}

