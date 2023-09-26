using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;

    [SerializeField] private int force;

    [SerializeField] private Rigidbody2D rg;

    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Collider2D coll;

    [SerializeField] private Animator anim;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask EnemyLayer;

    [SerializeField] private int slideSpeed;

    [SerializeField] private int forceJumpWall;

    [SerializeField] private MovingPF pf;

    [SerializeField] private JumpPad pad;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private GameObject life1;
    [SerializeField] private GameObject life2;
    [SerializeField] private GameObject life3;
    [SerializeField] private Text CoinText;


    // [SerializeField] private TrailRenderer tr;

    private int coins = 0;
    private float dirX = 0f;

    private bool isGrounded;

    private bool doubleJump;

    public bool isWallR;
    public bool isWallL;
    private bool isWallU;
    private bool isWall;

    private bool EnemyR;
    private bool EnemyL;
    private bool EnemyUnder;
    private bool jumpAfterTrap;

    private bool canRun = true;

    private bool isSliding;
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;
    private bool isIdling;

    private bool isHitting;

    // public bool isHit = false;
    private bool canJump = true;
    private int countJumping = 0;
    public int hp = 3;
    public int heart = 3;

    private Vector2 checkPointPos;

    void Start()
    {
        checkPointPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        Flip();

        // Double Jump
        isWallU = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .1f, groundLayer);
        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        if (isWallU)
        {
            countJumping = 3;
        }

        if (canJump)
        {
            DoubleJump();
            if (isGrounded || isSliding)
            {
                countJumping = 0;
            }

            if (countJumping == 0 && isFalling == true)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    countJumping = 1;
                    rg.velocity = new Vector2(rg.velocity.x, 17);
                }
            }
        }

        // Wall Slide
        isWallR = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, groundLayer);
        isWallL = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, groundLayer);
        WallSlide();
        isWallCheck();

        //JumpWall
        if (rg.velocity.y == -slideSpeed)
        {
            isGrounded = true;
            DoubleJump();
        }

        //JumpAfterTrap
        if (jumpAfterTrap || pad.jumpAfterPad)
        {
            if (Input.GetButtonDown("Jump"))
            {
                countJumping = 1;
                isHitting = false;
                rg.velocity = new Vector2(rg.velocity.x, 17);
                jumpAfterTrap = false;
                pad.jumpAfterPad = false;
            }
        }

        if (pf.isPlatform)
        {
            isWallL = false;
            isWallR = false;
            isWallU = false;
        }

        UpdateAnim();
        healthBar.SetHealth(hp);
        Hearts();
    }

    void Hearts()
    {
        if (heart == 2)
        {
            life3.SetActive(false);
        }

        if (heart == 1)
        {
            life2.SetActive(false);
        }

        if (heart == 0)
        {
            life1.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (canRun)
        {
            rg.velocity = new Vector2(dirX * speed, rg.velocity.y);
        }
    }

    void Flip()
    {
        if (dirX > 0)
        {
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            sprite.flipX = true;
        }
    }

    void DoubleJump()
    {
        if (isGrounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // JumpSoundd();
            if (isGrounded || doubleJump)
            {
                rg.velocity = new Vector2(rg.velocity.x, force);
                doubleJump = !doubleJump;
                countJumping++;
            }
        }
    }

    void WallSlide()
    {
        if (isWallR && rg.velocity.y < 0 && dirX != 0 || isWallL && rg.velocity.y < 0 && dirX != 0)
        {
            // if (rg.velocity.y < slideSpeed)
            // {
            //     rg.velocity = new Vector2(rg.velocity.x, -slideSpeed);
            // }
            rg.velocity = new Vector2(rg.velocity.x, -slideSpeed);
        }
    }

    private void isWallCheck()
    {
        if (isWallL && dirX != 0 && !isWallU && !isGrounded && rg.velocity.y == -slideSpeed ||
            isWallR && dirX != 0 && !isWallU && !isGrounded && rg.velocity.y == -slideSpeed)
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }
    }

    private void UpdateAnim()
    {
        if (dirX != 0 && isGrounded && !isWallL && !isWall && canRun && !isWallU && !isHitting ||
            dirX != 0 && isGrounded && !isWallR && !isWall && canRun && !isWallU && !isHitting)
        {
            anim.Play("Run");
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (rg.velocity.y > 0 && !isGrounded && countJumping < 1 && !isHitting)
        {
            anim.Play("Jump");
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if (rg.velocity.y < 0 && !isGrounded && !isHitting)
        {
            anim.Play("Fall");
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        if (isWall && !isHitting)
        {
            anim.Play("WallSlide");
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        if (isGrounded && !isWall && !isRunning && !isHitting)
        {
            anim.Play("Idle");
        }

        if (countJumping > 0 && !isFalling && !isSliding && !isHitting)
        {
            anim.Play("DoubleJump");
        }

        if (isHitting && hp > 0)
        {
            anim.Play("Hit");
        }
        else if (isHitting && hp == 0)
        {
            anim.Play("Dead");
        }
    }

    void HP()
    {
        hp--;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyBase>())
        {
            countJumping = 0;
            if (isFalling && transform.position.y - other.transform.position.y > 1)
            {
                rg.velocity = new Vector2(rg.velocity.x, 10);
                other.gameObject.GetComponent<EnemyBase>().EnemyHurt();
                return;
            }

            if (transform.position.x < other.transform.position.x)
            {
                rg.velocity = new Vector2(-10, 10);
                isHitting = true;
            }
            else
            {
                rg.velocity = new Vector2(10, 10);
                isHitting = true;
            }

            canRun = false;
            canJump = false;
            StartCoroutine(WaitStun(0.5f));
            StartCoroutine(WaitStun2(0.3f));
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            // GetDmgSoundd();
            countJumping = 0;
            if (sprite.flipX == false)
            {
                rg.velocity = new Vector2(-7, 10);
                isHitting = true;
            }
            else
            {
                rg.velocity = new Vector2(7, 10);
                isHitting = true;
            }
            canRun = false;
            StartCoroutine(WaitStun(0.5f));
            StartCoroutine(WaitStun2(0.3f));
            jumpAfterTrap = true;
        }
        if (other.gameObject.CompareTag("Fruits"))
        {
            coins++;
            CoinText.text = "" + coins;
        }
    }

    private IEnumerator WaitStun(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canRun = true;
        canJump = true;
    }

    private IEnumerator WaitStun2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isHitting = false;
    }

    void Die()
    {
        if (hp == 0)
        {
            StartCoroutine(CheckPoint(0.5f));
            heart--;
        }

        if (heart == 0)
        {
            StartLevel();
        }
    }

    private void StartLevel()
    {
        StartCoroutine(Load(0.5f));
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    private IEnumerator CheckPoint(float waitTime)
    {
        rg.velocity = new Vector2(0, 0);
        rg.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(waitTime);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1, 1, 1);
        rg.simulated = true;
        hp = 3;
        healthBar.SetHealth(hp);
    }

    private IEnumerator Load(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}