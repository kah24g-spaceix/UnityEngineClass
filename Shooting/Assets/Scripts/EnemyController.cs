using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject gameDirector;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -this.speed * Time.deltaTime, 0);
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameDirector.GetComponent<GameDirector>().AddScore();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            gameDirector.GetComponent<GameDirector>().AddScore();
            Destroy(gameObject);
        }
    }
}
