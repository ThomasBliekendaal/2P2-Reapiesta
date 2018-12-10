using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFunctions : MonoBehaviour
{
    [HideInInspector]
    public CharacterController cc;
    [SerializeField]
    float speed = 10;
    [SerializeField] float accelerationSpeed = 3;
    [SerializeField] float decelerationSpeed = 6;
    public ParticleSystem dustParticles;
    public float jumpHeight = 10;
    [SerializeField] float gravityStrength = -12;
    [SerializeField] float fallDeceleration = 50;

    [HideInInspector] public Vector3 moveV3;
    float skateSpeed = 40;
    [SerializeField]
    float skateRotSpeed = 20;
    public enum State
    {
        Foot,
        SkateBoard,
        Dash
    }
    public State curState = State.Foot;
    public GameObject skateBoard;
    Vector3 latePos;
    [SerializeField]
    Transform skateAngleHelper;
    [HideInInspector] public Cam cam;
    [Header("Skateboard Stats")]
    [SerializeField] float maxSkateSpeed;
    [SerializeField] float skateAcceleration;
    [SerializeField] float skateDeceleration;
    [SerializeField] float minSkateSpeed = 10;
    [SerializeField] float skateAngleSetSpeed = 10;
    bool justSkateGrounded = false;
    [SerializeField] float skateJumpHeight = 50;
    [HideInInspector] public bool grounded = false;
    public GameObject particleSkateChange;
    [HideInInspector] public bool canSkateJump = true;
    State stateBeforeDash = State.Foot;
    [Header("Dashing")]
    [SerializeField] GameObject[] dashEffects;
    [SerializeField] float dashSpeed = 10;
    [SerializeField] Renderer[] dashInvisible;
    [SerializeField] GameObject particleDash;
    [SerializeField] GameObject landingParticle;
    bool canDash = true;
    [HideInInspector] public float stamina = 100;
    public UIPercentBar staminaBar;
    bool antiBounce = false;

    public void Start()
    {
        cc = GetComponent<CharacterController>();
        latePos = transform.position;
        cam = Camera.main.GetComponent<Cam>();
    }

    public void LateUpdate()
    {
        latePos = transform.position;
    }

    public void StartSkateBoard()
    {
        if (cc.isGrounded == false)
        {
            grounded = false;
            if (canSkateJump == true)
            {
                StaticFunctions.PlayAudio(1, false);
                curState = State.SkateBoard;
                skateSpeed = 25;
                Instantiate(particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
                moveV3 = new Vector3(0, jumpHeight, 0);
                skateSpeed += 25;
                moveV3 += transform.TransformDirection(0, 0, minSkateSpeed / 5);
                transform.position += new Vector3(0, 2.1f, 0);
                transform.Rotate(0, 0, 180);
                canSkateJump = false;
                cam.MediumShake();
            }
        }
        else
        {
            StaticFunctions.PlayAudio(1, false);
            grounded = true;
            curState = State.SkateBoard;
            skateSpeed = 50;
            Instantiate(particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
            cam.MediumShake();
        }
    }

    public void StartDash()
    {
        if (canDash == true)
        {
            StaticFunctions.PlayAudio(13, false);
            stateBeforeDash = curState;
            curState = State.Dash;
            cam.MediumShake();
            for (int i = 0; i < dashInvisible.Length; i++)
            {
                dashInvisible[i].enabled = false;
            }
            Instantiate(particleDash, transform.position, Quaternion.identity);
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y, 0);
            moveV3 = transform.TransformDirection(0, 0, dashSpeed);
            if (grounded == false)
            {
                canDash = false;
            }
        }
    }

    public void DashStuff()
    {
        moveV3 = Vector3.MoveTowards(moveV3, Vector3.zero, Time.deltaTime * dashSpeed);
        if (Vector3.Distance(moveV3, Vector3.zero) < dashSpeed / 4 * 3)
        {
            Instantiate(particleDash, transform.position, Quaternion.identity);
            for (int i = 0; i < dashInvisible.Length; i++)
            {
                dashInvisible[i].enabled = true;
            }
            curState = stateBeforeDash;
            if (stateBeforeDash == State.SkateBoard)
            {
                skateSpeed = minSkateSpeed / 1.5f;
            }
            if (Physics.Raycast(transform.position, Vector3.down, 2) == false)
            {
                canDash = false;
                moveV3 = new Vector3(moveV3.x, jumpHeight, moveV3.z);
            }
        }
    }


    public void SkateGravity()
    {

        if (cc.isGrounded == true)
        {
            canDash = true;
            canSkateJump = true;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.localEulerAngles.y, 0), Time.deltaTime * 3);
        if (moveV3.y > 0)
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, -687, Time.deltaTime * 67);
        }
        else
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, -687, Time.deltaTime * 124);
        }
        justSkateGrounded = false;

    }

    public void SkateForward()
    {
        grounded = false;
        RaycastHit hit;
        float input = Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 2))
        {
            //            Debug.Log(hit.transform.name);
            grounded = true;
            //sets the rotation
            skateAngleHelper.rotation = Quaternion.Lerp(skateAngleHelper.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * skateAngleHelper.rotation, Time.deltaTime * skateAngleSetSpeed);
            skateAngleHelper.localEulerAngles = new Vector3(skateAngleHelper.eulerAngles.x, transform.localEulerAngles.y, skateAngleHelper.eulerAngles.z);
            transform.rotation = skateAngleHelper.rotation;

            //sets the speed
            if (-transform.forward.y > -0.1f)
            {
                //accelerate
                if (skateSpeed < -transform.forward.y * maxSkateSpeed)
                {
                    skateSpeed = Mathf.Lerp(skateSpeed, -transform.forward.y * maxSkateSpeed, Time.deltaTime * skateAcceleration);
                }
                else//decelerate
                {
                    skateSpeed = Mathf.Lerp(skateSpeed, -transform.forward.y * maxSkateSpeed, Time.deltaTime * skateDeceleration);
                }
                skateSpeed = Mathf.Max(0, skateSpeed);
            }
            else
            {
                if (-transform.forward.y < -0.3f)
                {
                    //  Debug.Log("yes");
                    skateSpeed = Mathf.MoveTowards(skateSpeed, 0, Time.deltaTime * transform.forward.y * skateDeceleration * 50);
                }
                else
                {
                    skateSpeed = Mathf.MoveTowards(skateSpeed, minSkateSpeed, Time.deltaTime * skateDeceleration);
                }
                // Rotator for no speed
                if (-transform.forward.y < 0.6f && skateSpeed < 30) //< 10f && skateSpeed < 20)
                {
                    if (-transform.right.y > 0)
                    {
                        skateSpeed = minSkateSpeed / 100;
                        transform.Rotate(0, skateRotSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        skateSpeed = minSkateSpeed / 100;
                        transform.Rotate(0, -skateRotSpeed * Time.deltaTime, 0);
                    }
                }
            }

            //set the actual vector
            moveV3 = transform.TransformDirection(0, 0, 1) * skateSpeed;
            //and move down
            if (-transform.forward.y > -0.01f)
            {
                moveV3 -= Vector3.up * 30;
            }
            else
            {
                moveV3 -= Vector3.up * 5;
            }

            //landing
            SkateLand(hit);
            justSkateGrounded = true;

            //minimum speed
            // skateSpeed = Mathf.Lerp(skateSpeed, Mathf.Max(skateSpeed,minSkateSpeed * (Input.GetAxis("Vertical"))),skateAcceleration * Time.deltaTime);

        }
        else
        {
            SkateGravity();
        }

        if (justSkateGrounded == true && Input.GetButtonDown("Jump") == true && Physics.Raycast(transform.position, Vector3.up, 2) == false)
        {
            justSkateGrounded = true;
            moveV3 += transform.TransformDirection(0, jumpHeight, 0);
            moveV3.y = skateJumpHeight;
            transform.position += new Vector3(0, 2.1f, 0);
        }

    }

    public void AntiBounceCancel()
    {
        antiBounce = false;
    }

    void SkateLand(RaycastHit hit)
    {
        if (justSkateGrounded == false)
        {
            if (antiBounce == false)
            {
                antiBounce = true;
                Invoke("AntiBounceCancel", 0.3f);
            }
            if (moveV3.y > 40)
            {
                Instantiate(landingParticle, transform.position, transform.rotation, transform);
                cam.HardShake();
                StaticFunctions.PlayAudio(2, false);
            }
            else
            {
                cam.SmallShake();
                StaticFunctions.PlayAudio(1, false);
            }
            skateSpeed /= 1.1f;
            transform.position = hit.point + new Vector3(0, 0.1f, 0);
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            canDash = true;
            canSkateJump = true;
            cc.Move(transform.TransformDirection(0, -1000 * Time.deltaTime, 0));
            moveV3.y = -1000 * Time.deltaTime;
        }
    }

    public void SkateBoost(bool shake)
    {
        skateSpeed = minSkateSpeed / 1.4f;
        if (shake == true)
        {
            cam.SmallShake();
        }
        for (int i = 0; i < dashEffects.Length; i++)
        {
            dashEffects[i].SetActive(true);
        }
    }

    public void StopSkateBoost()
    {
        for (int i = 0; i < dashEffects.Length; i++)
        {
            dashEffects[i].SetActive(false);
        }
        //   skateSpeed = minSkateSpeed / 3;
    }

    void SetTimeBack()
    {
        if (StaticFunctions.paused == false)
        {
            Time.timeScale = 1;
        }
    }

    public void SkateAngleY()
    {
        float goal = 0;
        if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
        {
            goal += Input.GetAxis("Horizontal") * skateRotSpeed;
        }
        transform.Rotate(0, goal * Time.deltaTime, 0);
    }

    public void Gravity()
    {
        if (cc.isGrounded == true)
        {
            canDash = true;
            canSkateJump = true;
        }
        if (moveV3.y < 0)
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, gravityStrength, Time.deltaTime * fallDeceleration * 2);
        }
        else
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, gravityStrength, Time.deltaTime * fallDeceleration);
        }

        if (Input.GetButtonDown("Jump") && cc.isGrounded == true)
        {
            moveV3.y = jumpHeight;
            CancelInvoke("AntiBounceCancel");
            AntiBounceCancel();
        }
    }

    public void AngleY()
    {
        float goal = transform.eulerAngles.y;
        if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
        {
            goal = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            goal += cam.transform.eulerAngles.y;
        }
        if (cc.isGrounded == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, goal, transform.eulerAngles.z), Time.deltaTime * 20);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, goal, transform.eulerAngles.z), Time.deltaTime * 5);
        }
    }

    public void MoveForward()
    {
        Vector3 goal;
        goal = transform.TransformDirection(0, 0, Mathf.Min(1, Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))))) * speed;
        if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
        {
            moveV3.x = Mathf.Lerp(moveV3.x, goal.x, Time.deltaTime * accelerationSpeed);
            moveV3.z = Mathf.Lerp(moveV3.z, goal.z, Time.deltaTime * accelerationSpeed);
        }
        else
        {
            moveV3.x = Mathf.Lerp(moveV3.x, goal.x, Time.deltaTime * decelerationSpeed);
            moveV3.z = Mathf.Lerp(moveV3.z, goal.z, Time.deltaTime * decelerationSpeed);
            if (cc.isGrounded == true && Physics.Raycast(transform.position, Vector3.down, 2, ~LayerMask.GetMask("Ignore Raycast")) == false)
            {//this makes him stop at a ledge when decelerating
                moveV3.x = 0;
                moveV3.z = 0;
            }
        }

        //this just feels better, don't question it. It stops you when the angle difference it bigger then 100
        if (cc.isGrounded == true)
        {
            float newGoal = transform.eulerAngles.y;
            if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
            {
                newGoal = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
                newGoal += cam.transform.eulerAngles.y;
            }
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, newGoal, transform.eulerAngles.z)) > 100)
            {
                moveV3.x = 0;
                moveV3.z = 0;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, newGoal, transform.eulerAngles.z);
            }
        }
    }

    public void FinalMove()
    {
        if (antiBounce == true)
        {
            if (Input.GetAxis("Jump") == 0)
            {
                //This fixes the most obnoxious bug ever. Bouncing..
                if (Physics.Raycast(transform.position, Vector3.down, 100, LayerMask.GetMask("Ignore Raycast", "IgnoreCam")))
                {
                    moveV3.y = -1000;
                }
            }
            else
            {
                antiBounce = false;
                CancelInvoke("AntiBounceCancel");
            }
        }
        cc.Move(moveV3 * Time.deltaTime);
    }
}