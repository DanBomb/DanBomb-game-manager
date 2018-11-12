using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text questmastertext;
	public Text questtext;

	private Rigidbody rb;
	SimpleGameManager SGM;

	void Start () {	
		rb = GetComponent<Rigidbody>();
		questmastertext.text = SGM.GetNextQuest ("KeyQuest");
		questtext.text = "";
	}
	void Awake () {
		SGM = SimpleGameManager.Instance;
	}
	
	
	void FixedUpdate () {
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 ( moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);

	}

	void OnTriggerEnter (Collider other) {
			
		questmastertext.text = "";
		questtext.text = SGM.GetQuestDescription (other.tag);
		SGM.AdvancedFinishQuest (other.tag);
		if (other.CompareTag ("KeyQuest")) {
			questmastertext.text = SGM.GetNextQuest ("DoorQuest");
		} else if (other.CompareTag ("DoorQuest")) {
			questmastertext.text = SGM.GetNextQuest ("BossQuest");
		} else if (other.CompareTag ("BossQuest")) {
			questmastertext.text = "You won!";
		}

	}
}
