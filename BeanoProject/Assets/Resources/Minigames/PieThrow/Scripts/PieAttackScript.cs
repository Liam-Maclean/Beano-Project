using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAttackScript : MonoBehaviour {


	private GameObject m_pieSpawner;
	private PieScript m_pieScript;

	private GameObject m_pedSpawner;
	private PieThrowManagerScript m_gameManagerScript;

	private SpriteRenderer m_sr;
	private PieSpriteChanger m_pieSpriteManager;
    private AudioSource m_pieSplatSound;

	public Sprite pieSplat;


	private bool m_isHit;
	private float m_hitScore;

    // Use this for initialization
    void Start ()
    {
		FloatingTextManager.Initialise ();
        //find the two spawners to acces scripts
        m_pieSpawner = GameObject.FindGameObjectWithTag("PieSpawner");
        m_pedSpawner = GameObject.FindGameObjectWithTag ("PedSpawner");

        m_pieSplatSound = this.GetComponent<AudioSource>();

        //get the script and component references
        m_pieScript = m_pieSpawner.GetComponent<PieScript>();
        m_gameManagerScript = m_pedSpawner.GetComponent<PieThrowManagerScript>();
        m_sr = gameObject.GetComponent<SpriteRenderer>();
        m_pieSpriteManager = gameObject.GetComponent<PieSpriteChanger> ();

        m_isHit = false;
   }
	
	// Update is called once per frame
	void Update ()
    {
		if(!m_isHit)
		{
			if (m_pieScript.GetLaunched () == true) 
			{
            	//cast a ray out of all the objects and store them in an array
				RaycastHit[] hit = Physics.RaycastAll (gameObject.transform.position, new Vector3 (0.0f, 0.0f, 1.0f), Mathf.Infinity);
            

				for (int i = 0; i < hit.Length; i++)
				{

                    m_isHit = true;
                    m_pieScript.SetHit(m_isHit);
                    PedScript pedScript;

					//get the ped script of the object that the pie has collided with
                    pedScript = hit[i].collider.gameObject.GetComponent<PedScript>();

					//get the animator component to transition the animation states
					Animator pedAnimator = hit [i].collider.gameObject.GetComponent<Animator> ();
                    AudioSource pedSound = hit[i].collider.gameObject.GetComponent<AudioSource>();


                    //play impact sounds
                    pedSound.Play();
                    m_pieSplatSound.Play();

					pedAnimator.Play ("Impact");
					//stop the move speed to allow the animation to play
					pedScript.SetMoveSpeed (0.0f);
					//add a delay to the destruction of the enemy to allow for the animation to play
					Destroy (hit [i].collider.gameObject, 1.0f);


                    //get the unique score of the collided object
                    m_hitScore = pedScript.GetScore ();
                    //add the score to the player's score
                    m_gameManagerScript.AddScore (m_hitScore);
					FloatingTextManager.CreateFloatingText (m_hitScore.ToString (), hit [i].collider.transform, Color.red);

                    //respawn the pie
                    m_pieScript.Respawn ();
                    m_pieScript.Destroy();
                    //stop the velocity of the pie for animation purposes
                    m_pieScript.SetDistance (new Vector3 (0.0f, 0.0f, 0.0f));
					break;
				}      
			}

            m_pieScript.SetHit(m_isHit);
		}
	}
}


