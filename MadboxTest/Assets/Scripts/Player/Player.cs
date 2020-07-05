using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerNumber { PLAYER_1, PLAYER_2, PLAYER_3}
    public delegate void PlayerFinishedEvent(Player p);
    public event PlayerFinishedEvent OnPlayerFinished;

    public float Speed;
    public PlayerNumber NumberPlayer;
    public Animator PlayerAnimator;
    public bool HasFinished = false;
    public float Points = 0;

    private Vector3 startPosition;
    private bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move(NumberPlayer);
    }

    private void Move(PlayerNumber number)
    {
        if (HasFinished || disabled)
            return;

        float z = 0;

        switch (number)
        {
            case PlayerNumber.PLAYER_1:

                if (Input.GetKey(KeyCode.W))
                {
                    z += 1 * Speed;
                }

                break;

            case PlayerNumber.PLAYER_2:

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    z += 1 * Speed;
                }
                break;
        }

        if (z > 0)
            PlayerAnimator.SetBool("Run", true);
        else
            PlayerAnimator.SetBool("Run", false);


        //transform.position += new Vector3(0, 0, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ResetPlayer();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            PlayerFinished();
        }
    }

    private void ResetPlayer()
    {
        StartCoroutine(DieCoroutine());
    }

    private void PlayerFinished()
    {
        PlayerAnimator.SetBool("Run", false);
        HasFinished = true;
        OnPlayerFinished?.Invoke(this);
    }

    IEnumerator DieCoroutine()
    {
        disabled = true;
        PlayerAnimator.SetBool("Die", true);

        while (PlayerAnimator.GetBool("Die"))
        {
            if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                PlayerAnimator.SetBool("Die", false);
            yield return null;
        }

        transform.position = startPosition;
        disabled = false;
    }
}
