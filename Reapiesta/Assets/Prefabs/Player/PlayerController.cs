using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public CharacterController cc;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float jumpHeight = 10;

    Vector3 moveV3;
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
    [SerializeField]
    GameObject skateBoard;
    Vector3 latePos;
    [SerializeField]
    Transform skateAngleHelper;
    Cam cam;
    [Header("Skateboard Stats")]
    [SerializeField]
    float maxSkateSpeed;
    [SerializeField]
    float skateAcceleration;
    [SerializeField]
    float skateDeceleration;
    [SerializeField]
    float minSkateSpeed = 10;
    [SerializeField]
    float skateAngleSetSpeed = 10;
    bool justSkateGrounded = false;
    [SerializeField]
    float skateJumpHeight = 50;
    [HideInInspector]
    public bool grounded = false;
    [SerializeField]
    GameObject particleSkateChange;
    bool canSkateJump = true;
    [Header("Dashing")]
    State stateBeforeDash = State.Foot;
    [SerializeField]
    float dashSpeed = 10;
    [SerializeField]
    Renderer[] dashInvisible;
    [SerializeField]
    GameObject particleDash;
    [SerializeField]
    GameObject landingParticle;
    bool canDash = true;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        latePos = transform.position;
        cam = Camera.main.GetComponent<Cam>();
    }

    void LateUpdate()
    {
        latePos = transform.position;
    }

    void Update()
    {
        Controll();
    }

    void Controll()
    {
        switch (curState)
        {

            case State.Foot:
                grounded = true;
                skateBoard.SetActive(false);
                MoveForward();
                AngleY();
                Gravity();
                FinalMove();
                if (Input.GetButtonDown("Fire2"))
                {
                    StartSkateBoard();
                }
                if (Input.GetButtonDown("Dash"))
                {
                    StartDash();
                }
                break;
            case State.SkateBoard:
                skateBoard.SetActive(true);
                SkateForward();
                SkateAngleY();
                // Gravity();
                FinalMove();
                if (Input.GetButtonDown("Fire2"))
                {
                    curState = State.Foot;
                    transform.eulerAngles = Vector3.zero;
                    Instantiate(particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
                    cam.MediumShake();

                    if (canSkateJump == false)
                    {
                        moveV3 += new Vector3(0, jumpHeight, 0);
                    }
                }
                if (Input.GetButtonDown("Dash"))
                {
                    StartDash();
                }
                break;
            case State.Dash:
                DashStuff();
                FinalMove();
                break;
        }
    }

    void StartSkateBoard()
    {
        if (cc.isGrounded == false)
        {
            grounded = false;
            if (canSkateJump == true)
            {
                curState = State.SkateBoard;
                skateSpeed = 50;
                Instantiate(particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
                moveV3 = new Vector3(0, jumpHeight, 0);
                skateSpeed += 50;
                moveV3 += transform.TransformDirection(0, 0, minSkateSpeed / 5);
                transform.position += new Vector3(0, 4.1f, 0);
                transform.Rotate(0, 0, 180);
                canSkateJump = false;
                cam.MediumShake();
            }
        }
        else
        {
            grounded = true;
            curState = State.SkateBoard;
            skateSpeed = 50;
            Instantiate(particleSkateChange, transform.position, Quaternion.Euler(90, 0, 0), transform);
            cam.MediumShake();
        }
    }

    void StartDash()
    {
        if (canDash == true)
        {
            stateBeforeDash = curState;
            curState = State.Dash;
            cam.MediumShake();
            for (int i = 0; i < dashInvisible.Length; i++)
            {
                dashInvisible[i].enabled = false;
            }
            Instantiate(particleDash, transform.position, Quaternion.identity, transform);
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y, 0);
            moveV3 = transform.TransformDirection(0, 0, dashSpeed);
            if (grounded == false)
            {
                canDash = false;
            }
        }
    }

    void DashStuff()
    {
        moveV3 = Vector3.MoveTowards(moveV3, Vector3.zero, Time.deltaTime * dashSpeed);
        if (Vector3.Distance(moveV3, Vector3.zero) < dashSpeed / 4 * 3)
        {
            for (int i = 0; i < dashInvisible.Length; i++)
            {
                dashInvisible[i].enabled = true;
            }
            curState = stateBeforeDash;
            if(stateBeforeDash == State.SkateBoard){
                skateSpeed = minSkateSpeed / 2;
            }
            if (Physics.Raycast(transform.position, Vector3.down, 4) == false)
            {
                canDash = false;
                moveV3 = new Vector3(moveV3.x, jumpHeight, moveV3.z);
            }
        }
    }


    void SkateGravity()
    {

        if (cc.isGrounded == true)
        {
            canDash = true;
            canSkateJump = true;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.localEulerAngles.y, 0), Time.deltaTime * 3);
        if (moveV3.y > 0)
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, -1200, Time.deltaTime * 100);
        }
        else
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, -1200, Time.deltaTime * 200);
        }
        justSkateGrounded = false;

    }

    void SkateForward()
    {
        grounded = false;
        RaycastHit hit;
        float input = Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 4))
        {
            grounded = true;
            //sets the rotation
            skateAngleHelper.rotation = Quaternion.Lerp(skateAngleHelper.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * skateAngleHelper.rotation, Time.deltaTime * skateAngleSetSpeed);
            skateAngleHelper.localEulerAngles = new Vector3(skateAngleHelper.eulerAngles.x, transform.localEulerAngles.y, skateAngleHelper.eulerAngles.z);
            transform.rotation = skateAngleHelper.rotation;

            //sets the speed
            if (-transform.forward.y > 0)
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
                if (-transform.forward.y < -0.3f && skateSpeed < 30) //< 10f && skateSpeed < 20)
                {
                    if (-transform.right.y > 0)
                    {
                        skateSpeed = 0;
                        transform.Rotate(0, skateRotSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        skateSpeed = 0;
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
            if (justSkateGrounded == false)
            {
                if (moveV3.y > 40)
                {
                    Instantiate(landingParticle, transform.position, transform.rotation, transform);
                    cam.HardShake();
                }
                else
                {
                    cam.SmallShake();
                }
                skateSpeed /= 1.1f;
                transform.position = hit.point + new Vector3(0, 0.01f, 0);
                moveV3 = Vector3.zero;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                canDash = true;
                canSkateJump = true;
            }
            justSkateGrounded = true;

            //minimum speed
            // skateSpeed = Mathf.Lerp(skateSpeed, Mathf.Max(skateSpeed,minSkateSpeed * (Input.GetAxis("Vertical"))),skateAcceleration * Time.deltaTime);

        }
        else
        {
            SkateGravity();
        }

        if (justSkateGrounded == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                justSkateGrounded = true;
                moveV3 = transform.TransformDirection(0, skateJumpHeight, 0) + moveV3;
                transform.position += new Vector3(0, 4.1f, 0);
            }
        }

    }

    void SkateAngleY()
    {
        float goal = 0;
        if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
        {
            goal += Input.GetAxis("Horizontal") * skateRotSpeed;
        }
        transform.Rotate(0, goal * Time.deltaTime, 0);
    }

    void Gravity()
    {
        if (cc.isGrounded == true)
        {
            canDash = true;
            canSkateJump = true;
        }
        if (moveV3.y < 0)
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, -120f, Time.deltaTime * 500);
        }
        else
        {
            moveV3.y = Mathf.MoveTowards(moveV3.y, -120f, Time.deltaTime * 250);
        }

        if (Input.GetButtonDown("Jump") && cc.isGrounded == true)
        {
            moveV3.y = jumpHeight;
        }
    }

    void AngleY()
    {
        float goal = transform.eulerAngles.y;
        if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) != 0)
        {
            goal = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            goal += Camera.main.transform.eulerAngles.y;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, goal, transform.eulerAngles.z), Time.deltaTime * 20);
    }

    void MoveForward()
    {
        Vector3 goal;
        goal = transform.TransformDirection(0, 0, Mathf.Min(1, Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))))) * speed;
        moveV3.x = Mathf.Lerp(moveV3.x, goal.x, Time.deltaTime * 6);
        moveV3.z = Mathf.Lerp(moveV3.z, goal.z, Time.deltaTime * 6);
    }

    void FinalMove()
    {
        cc.Move(moveV3 * Time.deltaTime);
    }
}
