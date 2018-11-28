using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public Transform player;
    public Transform helper;
    public float speed = 10;
    public float rotSpeed = 10;
    public float distance = 20;
    public Vector3 offset = Vector3.zero;
    PlayerFunctions playerMov;
    Vector3 angleGoal;

    bool isShaking = false;
    Vector3 lastPos;
    float shakestr = 0.5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMov = player.GetComponent<PlayerFunctions>();
        lastPos = transform.position;

    }

    void LateUpdate()
    {
        transform.position = lastPos;
        //Debug.Log(playerMov.grounded);
        if (playerMov.curState == PlayerFunctions.State.SkateBoard && playerMov.grounded == true)
        {
            SkateRotate();
        }
        else
        {
            NormalCam();
        }
        Zooming();

        lastPos = transform.position;
        if (isShaking == true)
        {
            Shake(shakestr);
        }
    }

    void Update()
    {
        // It does not actuarly work though :( if somebody wants to fix it, go ahead -Casper.
        angleGoal = new Vector3(Mathf.Clamp(angleGoal.x, -60, 10), angleGoal.y, angleGoal.z);
        transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -60, 10), transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void StartShake(float time, float strength)
    {
        CancelInvoke("StopShake");
        isShaking = true;
        shakestr = strength;
        Invoke("StopShake", time);
    }

    public void SmallShake(){
        StartShake(0.1f,0.2f);
    }

    public void MediumShake(){
        StartShake(0.2f,0.5f);
    }

    public void HardShake(){
         StartShake(0.3f,1f);
    }

    void StopShake()
    {
        isShaking = false;
    }

    void Shake(float str)
    {
        transform.position += new Vector3(Random.Range(-str, str), Random.Range(-str, str), Random.Range(-str, str));
    }

    void NormalCam()
    {
        helper.position = Vector3.Lerp(helper.position, player.position + transform.TransformDirection(offset), Time.deltaTime * speed);
        //  helper.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed;
        angleGoal += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed; ;
        helper.rotation = Quaternion.Lerp(helper.rotation, Quaternion.Euler(angleGoal), Time.unscaledDeltaTime * 20);
        transform.position = helper.position + helper.TransformDirection(0, 0, distance);
        transform.LookAt(helper.position);
    }

    void Zooming()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.position - transform.position), out hit, Vector3.Distance(transform.position, player.position), ~LayerMask.GetMask("IgnoreCam", "Ignore Raycast")))
        {
            transform.position = hit.point;
        }
        if (Physics.Raycast(player.position, (transform.position - player.position), out hit, Vector3.Distance(transform.position, player.position), ~LayerMask.GetMask("IgnoreCam", "Ignore Raycast")))
        {
            transform.position = hit.point;
        }
    }

    void SkateRotate()
    {
        helper.position = Vector3.Lerp(helper.position, player.position + transform.TransformDirection(offset), Time.deltaTime * speed);
        helper.rotation = Quaternion.Lerp(helper.rotation, Quaternion.Euler(helper.eulerAngles.x, player.eulerAngles.y + 180, helper.eulerAngles.z), Time.deltaTime * 3);
        helper.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed;
        transform.position = helper.position + helper.TransformDirection(0, 0, distance);
        transform.LookAt(helper.position);
        angleGoal = helper.eulerAngles;
    }
}
