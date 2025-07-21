using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player:")]
    public float speed = 3;
    public float limitPosX = 2.6f;
    public float limitPosY = 4.6f;
    Animator anim;
    BulletGenerator bullet;
    SpriteRenderer spriteRenderer;
    private GameObject gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<BulletGenerator>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet.StartFire();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            bullet.StopFire();
        }
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -limitPosX, limitPosX),
            Mathf.Clamp(transform.position.y, -limitPosY, limitPosY), 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameDirector.GetComponent<GameDirector>().DecreaseHP();
            StartCoroutine("HitAnimation");
        }

    }
    private IEnumerator HitAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
