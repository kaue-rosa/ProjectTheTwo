using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroopAnimation : MonoBehaviour {

	[SerializeField] private bool UseDebugKeys = false;
	[SerializeField] private bool playOnStart = true;
	[SerializeField] private List<Sprite> walkCicleSprites = new List<Sprite>();
	[SerializeField] private List<Sprite> attackCicleSprites = new List<Sprite>();
	[SerializeField] private Sprite hitSprite = null;

	private SpriteRenderer spriteRenderer = null;

	//keys for walking
	private bool walking = false;
	private bool stopWalking = false;

	//keys for attacking
	private bool attacking = false;
	private bool stopAttacking = false;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		if (playOnStart) {
			StartWalking();
		}
	}	

	void Update() {
		if(UseDebugKeys){
			if(Input.GetKeyDown(KeyCode.S))StopWalking();
			if(Input.GetKeyDown(KeyCode.A))StartAttacking();
			if(Input.GetKeyDown(KeyCode.W))StartWalking();
			if(Input.GetKeyDown(KeyCode.H))Hit();
		}
	}

	void StartWalking() {
		if (walkCicleSprites.Count <= 0 || walking)return;
		walking = true;
		stopWalking = false;
		StartCoroutine (WalkAnim ());
	}

	void StopWalking() {
		stopWalking = true;
	}

	void StartAttacking() {
		if (attacking)return;

		StopWalking ();

		attacking = true;
		stopAttacking = false;

		StartCoroutine (AttackAnim ());
	}

	void StopAttacking() {
		stopAttacking = true;
	}

	void Hit() {
		StopWalking ();
		StopAttacking ();
		StartCoroutine (HitAnim ());
	}


	IEnumerator WalkAnim() {
		int index = 0;
		while (true) {

			if(stopWalking) {
				walking = false;
				break;
			}

			spriteRenderer.sprite = walkCicleSprites[index];
			index++;

			if(index >= walkCicleSprites.Count) {
				index = 0;
			}


			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator AttackAnim() {
		int index = 0;
		while (true) {
			if(stopAttacking) {
				attacking = false;
				break;
			}

			spriteRenderer.sprite = attackCicleSprites[index];
			index++;

			if(index >= attackCicleSprites.Count) {
				yield return new WaitForSeconds(0.05f);
				spriteRenderer.sprite = walkCicleSprites[0];//put a default
				attacking = false;
				break;
			}


			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator HitAnim() {
		spriteRenderer.sprite = hitSprite;
		for (int i = 0; i < 15; i++) {
			spriteRenderer.enabled = !spriteRenderer.enabled;
			yield return new WaitForSeconds(0.05f);
		}
		spriteRenderer.enabled = true;
	}
}
