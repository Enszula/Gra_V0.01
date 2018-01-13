using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour
    {
        private bool facingRight = true; // For determining which way the player is currently facing.

        [SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
        [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.	
        [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
        [SerializeField] private bool airControll; // kontrolowanie postaci podczas skakania
        [SerializeField] private float fall_multiplayer = 2.5f; // mnoznik skoku
        [SerializeField] private float low_jump = 2f; // wartosc niskiego skoku

        private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool grounded = false; // Whether or not the player is grounded.
        private bool jump;
        Transform groundCheck; // A position marking where to check if the player is grounded.
        Animator anim; // Odwolanie do animatora postaci
        Rigidbody2D player_Rigidbody2D; // Odwolanie do Rb2D postaci
        Transform playerGraphics; // Odwolanie do grafiki postaci

        private void Awake()
        {
            anim = GetComponent<Animator>();
            player_Rigidbody2D = GetComponent<Rigidbody2D>();
            groundCheck = transform.Find("GroundCheck");
            playerGraphics = transform.Find("Graphics");
                // sprawdzanie czy zastosowano grafike dla glownej postaci
            if (playerGraphics == null)
            {
                Debug.LogError("Brak grafiki glownej postaci!");
            }
        }

        private void Update()
        {
            if (!jump)
                jump = Input.GetButtonDown("Jump");
                    // Rozna wysokosc skokow w zaleznosci od dlugosci przytrzymania przycisku skoku
                if (player_Rigidbody2D.velocity.y < 0)
                {
                    player_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fall_multiplayer - 1) * Time.deltaTime;
                }
                else if (player_Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
                {
                    player_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (low_jump - 1) * Time.deltaTime;
                }
        }


        private void FixedUpdate()
        {
                // Pozyskiwanie informacji o poruszaniu sie postaci
            float horizontal = Input.GetAxis("Horizontal");
            Move(horizontal, jump);
            jump = false;
                // jeżeli kolo u dolu postaci dotyka ziemi to postac uziemiona
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround); 
            anim.SetBool("Ground", grounded);
        }


        public void Move(float move, bool jump)
        {

            //only control the player if grounded or airControl is turned on
            if (grounded || airControll)
            {
                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                anim.SetFloat("Speed", Mathf.Abs(move));

                    // poruszanie postacia
                player_Rigidbody2D.velocity = new Vector2(move * maxSpeed, player_Rigidbody2D.velocity.y);

                    // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                    Flip();
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                    Flip();
            }
            // Skakanie postaci i ograniczenie do jednego skoku
            if (grounded && jump && anim.GetBool("Ground"))
            {
                // dodanie mocy skoku
                grounded = false;
                anim.SetBool("Ground", false);
                player_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }


        private void Flip()
        {
            // Zmiana strony w ktora obrocona jest grafika postaci.
            facingRight = !facingRight;

            // zmiana wektora x o -1.
            Vector3 theScale = playerGraphics.localScale;
            theScale.x *= -1;
            playerGraphics.localScale = theScale;
        }
    }
}