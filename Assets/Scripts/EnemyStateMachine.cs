using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {

	private BattleStateMachine BSM;
	public BaseEnemy enemy;
	bool actionStarted;

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

		//wait a bit
		//do damage

		//animate back to start position

		//remove this performer from list in BSM
		
		//reset BSM -> Wait

		actionStarted = false;
		//reset this enemy
		cur_cooldown = 0.0f;
		currentState = TurnState.PROCESSING;
	
	}

}
