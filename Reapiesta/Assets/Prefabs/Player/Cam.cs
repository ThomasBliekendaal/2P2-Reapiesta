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

    void Update()
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

    void StartShake(float time, float strength)
    {
        CancelInvoke("StopShake");
        isShaking = true;
        shakestr = strength;
        Invoke("StopShake", time);
    }

    public void SmallShake()
    {
        StartShake(0.1f, 0.2f);
    }

    public void MediumShake()
    {
        StartShake(0.2f, 0.5f);
    }

    public void HardShake()
    {
        StartShake(0.3f, 1f);
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
        angleGoal += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed;
        angleGoal.x -= 90;
        angleGoal = new Vector3(-Mathf.Clamp(-angleGoal.x, 40, 140), angleGoal.y, angleGoal.z);
        angleGoal.x += 90;
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
        helper.position = Vector3.Lerp(helper.position, player.position + transform.TransformDirection(offset), Time.deltaTime * speed * 2);
        //  helper.eulerAngles += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed;
        angleGoal += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.unscaledDeltaTime * rotSpeed;
        angleGoal.x -= 90;
        angleGoal = new Vector3(-Mathf.Clamp(-angleGoal.x, 40, 140), angleGoal.y, angleGoal.z);
        angleGoal.x += 90;
        helper.rotation = Quaternion.Lerp(helper.rotation, Quaternion.Euler(angleGoal), Time.unscaledDeltaTime * 20);

        transform.position = helper.position + helper.TransformDirection(0, 0, distance);
        transform.LookAt(helper.position);
        if (Mathf.Abs(Mathf.Abs(player.eulerAngles.y) - Mathf.Abs(transform.eulerAngles.y)) > 0)
        {
            angleGoal.y = Quaternion.Lerp(Quaternion.Euler(angleGoal), Quaternion.Euler(player.localEulerAngles.x, player.eulerAngles.y + 180, angleGoal.z), Time.deltaTime * rotSpeed / 10).eulerAngles.y;
        }
        //the z axis rotation effect
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, Input.GetAxis("Horizontal") * -5), Time.deltaTime * 1000);

    }
}
