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
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        cam = Camera.main.GetComponent<Cam>();
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
                    rend[0].enabled = true;
                    rend[1].enabled = true;
                }
                break;
            case State.Normal:
                NormalThrow();
                RotateScythe();
                break;
            case State.GoBack:
                MoveToPlayer();
                Catch();
                RotateScythe();
                break;
        }
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
        rend[0].enabled = false;
        rend[1].enabled = false;
        transform.position = player.position;
        hurtbox.SetActive(false);
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, returnSpeed * Time.deltaTime);
    }

    void StartThrow()
    {
        goal = player.position + player.forward * range;
        curState = State.Normal;
        cam.SmallShake();
        hurtbox.SetActive(true);
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
            curState = State.Disabled;
            cam.SmallShake();
        }
    }
}
