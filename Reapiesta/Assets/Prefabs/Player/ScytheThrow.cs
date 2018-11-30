using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheThrow : MonoBehaviour
{

    Transform player;
    [SerializeField] float speed = 10;
    [SerializeField] float returnSpeed = 10;
    Vector3 goal;
    [SerializeField] float range = 100;
    public enum State
    {
        Disabled,
        Normal,
        GoBack
    }
    public State curState = State.Disabled;
    [SerializeField] List<Renderer> rend;
    Cam cam;
    [SerializeField] GameObject hurtbox;
    Vector3 lastPos;
    [SerializeField] Vector3 offset = Vector3.zero;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        cam = Camera.main.GetComponent<Cam>();
        lastPos = transform.position;
    }

    void Update()
    {
        switch (curState)
        {
            case State.Disabled:
                DisabledStuff();
                if (Input.GetButtonDown("Throw") == true)
                {
                    StartThrow();
                }
                break;
            case State.Normal:
                NormalThrow();
                RotateScythe();
                RayCollider();
                break;
            case State.GoBack:
                MoveToPlayer();
                Catch();
                RotateScythe();
                break;
        }
    }

    void RayCollider()
    {
        RaycastHit hit;
        if (Physics.Raycast(lastPos, lastPos - transform.position, out hit, Vector3.Distance(transform.position, lastPos), ~LayerMask.GetMask("IgnoreCam", "Ignore Raycast")))
        {
            curState = State.GoBack;
            StaticFunctions.PlayAudio(9, false);
            cam.SmallShake();
        }
        else if (Physics.Raycast(transform.position, transform.position - lastPos, out hit, Vector3.Distance(transform.position, lastPos), ~LayerMask.GetMask("IgnoreCam", "Ignore Raycast")))
        {
            curState = State.GoBack;
            StaticFunctions.PlayAudio(9, false);
            cam.SmallShake();
        }
        // Debug.DrawRay(lastPos, lastPos - transform.position, Color.red, 1);
        lastPos = transform.position;
    }

    void RotateScythe()
    {
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].transform.Rotate(1000 * Time.deltaTime, 0, 0);
        }
    }

    void DisabledStuff()
    {
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].enabled = false;
        }
        transform.position = player.position;
        hurtbox.SetActive(false);
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, returnSpeed * Time.deltaTime);
    }

    void StartThrow()
    {
        lastPos = transform.position;
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].enabled = true;
        }
        curState = State.Normal;
        cam.SmallShake();
        hurtbox.SetActive(true);
        StaticFunctions.PlayAudio(4, false);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, 10, ~LayerMask.GetMask("IgnoreCam", "Ignore Raycast")))
        {
            float ySaveCam = cam.transform.eulerAngles.y;
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, 0, cam.transform.eulerAngles.z);
            goal = player.position + cam.transform.forward * range + cam.transform.TransformDirection(offset);
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, ySaveCam, cam.transform.eulerAngles.z);
            transform.position -= (transform.position - hit.point);
        }
        else
        {
            goal = player.position + cam.transform.forward * range + cam.transform.TransformDirection(offset);
        }
    }

    void NormalThrow()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, goal) < 10)
        {
            curState = State.GoBack;
        }
    }

    void Catch()
    {
        if (Vector3.Distance(transform.position, player.position) < 5)
        {
            StaticFunctions.PlayAudio(1, false);
            curState = State.Disabled;
            cam.SmallShake();
        }
    }
}
