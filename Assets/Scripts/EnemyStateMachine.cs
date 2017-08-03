using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {

	public BaseEnemy enemy;

	public enum TurnState{
		PROCESSING,
		ADDTOLIST,
		WAITING,
		SELECTING,
		ACTION,
		DEAD
	}

	public TurnState currentState;
	//for progress bar
	private float cur_cooldown = 0.0f;
	private float max_cooldown = 5.0f;

	void Start(){
		currentState = TurnState.PROCESSING;
	}

	void UpgradeProgressBar(){
		cur_cooldown = cur_cooldown + Time.deltaTime;
		if(cur_cooldown >= max_cooldown){
			currentState = TurnState.ADDTOLIST;
		}
	}
}
