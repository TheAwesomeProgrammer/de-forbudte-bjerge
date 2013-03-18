using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public float CloseDistance = 0.5f;
    public float DieDistance = 0.01f;
    public float Speed = 5;

    public int ScoreToGivePlayer = 1;

    private Vector3 mPlayer;

    private bool Visible = false;

	// Use this for initialization
	void Start () {
        Visible = false;
	}
	
	// Update is called once per frame
	void Update () {
       if(Visible)
       {
           MoveToPlayer();
       }
       
	}

    void OnTriggerEnter(Collider pCollider)
    {
        if(pCollider.tag == "Player")
        {
           // GameObject.Find("AudioManager").GetComponent<PlaySounds>().PlaySound("Coin");
            PermenentVariables.Score += ScoreToGivePlayer;
            Destroy(gameObject);
        }
    }

    void OnBecameVisible()
    {
        Visible = true;
    }
    void OnBecameInvisible()
    {
        Visible = false;
    }

    void MoveToPlayer()
    {
        Vector3 mPlayer = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(Vector3.Distance(mPlayer,transform.position) < CloseDistance)
        {
            mPlayer += new Vector3(0, 0.5f, 0);
            transform.LookAt(mPlayer);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        if (Vector3.Distance(mPlayer, transform.position) < DieDistance)
        {
           GameObject.Find("AudioManager").GetComponent<PlaySounds>().PlaySound("Coin");
            PermenentVariables.Score += ScoreToGivePlayer;
            Destroy(gameObject);
        }
       
    }
}
