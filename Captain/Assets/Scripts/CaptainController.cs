using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Captain.Command;

public class CaptainController : MonoBehaviour
{
    private ICaptainCommand fire1;
    private ICaptainCommand fire2;
    private ICaptainCommand right;
    private ICaptainCommand left;
    public float speed = 5.0f;

    [SerializeField]
    private UnityEngine.UI.Text booty;
    private int mushrooms;
    private int skulls;
    private int gems;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.fire1 = gameObject.AddComponent<CaptainMotivateCommand>();
        this.fire2 = gameObject.AddComponent<CaptainBoostCommand>();
        this.right = ScriptableObject.CreateInstance<MoveCharacterRight>();
        this.left = ScriptableObject.CreateInstance<MoveCharacterLeft>();
        this.booty.text = "Booty";
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.fire1.Execute(this.gameObject);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            this.fire2.Execute(this.gameObject);
        }
        if(Input.GetAxis("Horizontal") > 0.01)
        {
            this.right.Execute(this.gameObject);
        }
        if(Input.GetAxis("Horizontal") < -0.01)
        {
            this.left.Execute(this.gameObject);
        }

        // Horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Update Animator and UI
        var animator = GetComponent<Animator>();
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x / 5.0f));
        this.booty.text = "x" + (this.mushrooms * 1 + this.gems * 3 + this.skulls * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            this.mushrooms++;
        }
        else if (collision.gameObject.tag == "Skull")
        {
            Destroy(collision.gameObject);
            this.skulls++;
        }
        else if(collision.gameObject.tag == "Gem")
        {
            Destroy(collision.gameObject);
            this.gems++;
        }
    }
}