using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public float moveSpeed = 2.0f; // 這裡將前進速度設定為移動速度
    private Rigidbody2D rb2;
    public float airResistanceFactor = 0.1f;
    private AudioSource audioSource;


    void Start()
    {
        // 獲取角色的Rigidbody2D組件
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 在 Update 中检测是否按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // 在FixedUpdate中檢測向左和向右移動按鍵（A和D鍵）
        float horizontalInput = Input.GetAxis("Horizontal");

        // 計算水平運動方向
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb2.velocity.y);

        // 將水平運動應用於角色
        rb2.velocity = movement;

        // 添加空气阻力
        Vector2 airResistance = new Vector2(-rb2.velocity.x * airResistanceFactor, -rb2.velocity.y * airResistanceFactor);
        rb2.AddForce(airResistance, ForceMode2D.Force);
    }

    // 播放音乐
    public void PlayMusic()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collision with coin");
            audioSource = GetComponent<AudioSource>();
            PlayMusic();
            Destroy(other.gameObject);
            GameObject.Find("GameManager").GetComponent<GameManager>().CollectCoin();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("Collision with Spikes");
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

}
