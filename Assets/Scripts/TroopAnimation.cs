using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TroopAnimation : MonoBehaviour {
	
	[SerializeField] private SpriteRenderer spriteRenderer = null;
	[SerializeField] private List<Sprite> walkCicleSprites = new List<Sprite>();
	[SerializeField] private List<Sprite> attackCicleSprites = new List<Sprite>();
	[SerializeField] private Sprite hitSprite = null;
	[SerializeField] private List<Sprite> celebrationCicleSprites = new List<Sprite>();

	//keys for walking
	private bool walking = false;
	private bool stopWalking = false;

	//keys for attacking
	private bool attacking = false;
	private bool stopAttacking = false;

	//keys for hit
	private bool beenHit = false;

	//keys for celebration
	private bool celebrating = false;
	private bool stopCelebrating = false;

	private Dictionary<int, Vector3> troopSpriteLayer = new Dictionary<int, Vector3>(){
		{100,new Vector3(0,-0.05f,0)},
		{101,new Vector3(0,-0.2f,0)},
		{102,new Vector3(0,-0.35f,0)},
		{103,new Vector3(0,-0.5f,0)},
		{104,new Vector3(0,-0.65f,0)},
		{105,new Vector3(0,-0.8f,0)},
		{106,new Vector3(0,-0.95f,0)},
	};

	void Start () {

		int i = Mathf.FloorToInt(Random.value * troopSpriteLayer.Count);
		spriteRenderer.sortingOrder = troopSpriteLayer.ElementAt(i).Key;
		spriteRenderer.gameObject.transform.localPosition = troopSpriteLayer.ElementAt(i).Value;
	}	

	public void StartWalking() {
		if (walkCicleSprites.Count <= 0 || walking)return;
		walking = true;
		stopWalking = false;
		StartCoroutine (WalkAnim ());
	}

	void StopWalking() {
		stopWalking = true;
	}

	public void StartAttacking(System.Action animationEndCall) {
		if (attacking)return;

		StopWalking ();

		attacking = true;
		stopAttacking = false;

		StartCoroutine (AttackAnim (animationEndCall));
	}

	void StopAttacking() {
		stopAttacking = true;
	}

	public void Hit(System.Action animationEndCall) {
		if (beenHit)return;
		beenHit = true;
		StopWalking ();
		StopAttacking ();
		StartCoroutine (HitAnim (animationEndCall));
	}

	public void StartCelebrating() {
		if (celebrationCicleSprites.Count <= 0 || celebrating)return;
		celebrating = true;
		stopCelebrating = false;
		StopWalking ();
		StopAttacking ();
		StartCoroutine (CelebrateAnim ());
	}
	
	void StopCelebrating() {
		stopCelebrating = true;
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

			yield return new WaitForSeconds(0.1f * Time.timeScale);
		}
	}

	IEnumerator AttackAnim(System.Action animationEndCall) {
		int index = 0;
		while (true) {
			if(stopAttacking) {
				attacking = false;
				break;
			}

			spriteRenderer.sprite = attackCicleSprites[index];
			index++;

			if(index >= attackCicleSprites.Count) {
				yield return new WaitForSeconds(0.05f*Time.timeScale);
				if(!stopAttacking)spriteRenderer.sprite = walkCicleSprites[0];//put a default
				attacking = false;
				animationEndCall.Invoke();
				break;
			}

			yield return new WaitForSeconds(0.1f*Time.timeScale);
		}
	}

	IEnumerator HitAnim(System.Action animationEndCall) {
		spriteRenderer.sprite = hitSprite;
		for (int i = 0; i < 15; i++) {
			spriteRenderer.enabled = !spriteRenderer.enabled;
			yield return new WaitForSeconds(0.05f*Time.timeScale);
		}
		spriteRenderer.sprite = walkCicleSprites[0];
		animationEndCall.Invoke ();
		spriteRenderer.enabled = true;
		beenHit = false;
	}

	IEnumerator CelebrateAnim() {
		int index = 0;
		yield return new WaitForSeconds(Random.value);
		while (true) {
			
			if(stopCelebrating) {
				celebrating = false;
				break;
			}
			
			spriteRenderer.sprite = celebrationCicleSprites[index];
			index++;
			
			if(index >= celebrationCicleSprites.Count) {
				index = 0;
			}
			
			yield return new WaitForSeconds(0.2f);
		}
	}
}
