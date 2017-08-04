using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {

	private BattleStateMachine BSM;
	public BaseEnemy enemy;
	bool actionStarted;
	public GameObject HeroToAttack;
	private float animSpeed = 5.0f;

	public enum TurnState{
		PROCESSING,
		CHOOSEACTION,
		WAITING,
		ACTION,
		DEAD
	}

	public TurnState currentState;
	//for progress bar
	private float cur_cooldown = 0.0f;
	private float max_cooldown = 5.0f;
	//this game object
	private Vector3 startposition;

	void Start(){
		currentState = TurnState.PROCESSING;
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
		startposition = transform.position;
		//IEnumerator Stuff timeforaction stuff
		actionStarted = false;
		animSpeed = 5.0f;

	}

	void Update(){
		switch(currentState){
			case(TurnState.PROCESSING):
				UpgradeProgressBar();
			 	break;
			case(TurnState.CHOOSEACTION):
				ChooseAction();
				currentState = TurnState.WAITING;
				break;
			case(TurnState.WAITING):
				//idle state
				break;
			case(TurnState.ACTION):
				StartCoroutine(TimeForAction());
				break;
			case(TurnState.DEAD):
				break;
		}

	}

	void UpgradeProgressBar(){
		cur_cooldown = cur_cooldown + Time.deltaTime;
		if(cur_cooldown >= max_cooldown){
			currentState = TurnState.CHOOSEACTION;
		}
	}

	void ChooseAction(){
		HandleTurn myAttack = new HandleTurn();
		myAttack.Attacker = enemy.name;
		myAttack.Type = "Enemy";
		myAttack.AttackersGameObject = this.gameObject;
		myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0,BSM.HerosInBattle.Count)];
		BSM.CollectActions(myAttack);
	}

	private IEnumerator TimeForAction(){
		if(actionStarted){
			yield break;
		}
		actionStarted = true;

		//animate the enemy near the hero to attack
		Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x-1.5f,HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
		while(MoveTowardsEnemy(heroPosition)){
			yield return null;
		}

		//wait a bit
		yield return new WaitForSeconds(0.5f);
		//do damage

		//animate back to start position
		Vector3 firstPosition = startposition;
		while(MoveTowardsStart(firstPosition)){
			yield return null;
		}

		//remove this performer from list in BSM
		BSM.PerformList.RemoveAt(0);
		
		//reset BSM -> Wait
		BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

		actionStarted = false;
		//reset this enemy
		cur_cooldown = 0.0f;
		currentState = TurnState.PROCESSING;
	
	}
	private bool  MoveTowardsEnemy(Vector3 target){
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}
	private bool  MoveTowardsStart(Vector3 target){
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}
}
