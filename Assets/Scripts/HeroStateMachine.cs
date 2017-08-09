using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour {


	private BattleStateMachine BSM;
	public BaseHero hero;
	public Image ProgressBar;

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
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
		currentState = TurnState.PROCESSING;
	}

	void Update(){
		switch(currentState){
			case(TurnState.PROCESSING):
				UpgradeProgressBar();
			 	break;
			case(TurnState.ADDTOLIST):
				BSM.HerosToManage.Add(this.gameObject);
				currentState = TurnState.WAITING;
				break;
			case(TurnState.WAITING):
			//idle
				break;
			case(TurnState.ACTION):
				break;
			case(TurnState.DEAD):
				break;
		}
	}

	void UpgradeProgressBar(){
		cur_cooldown = cur_cooldown + Time.deltaTime;
		float calc_cooldown = cur_cooldown / max_cooldown;
		ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown,0,1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.x); 
		if(cur_cooldown >= max_cooldown){
			currentState = TurnState.ADDTOLIST;
		}
	}

}
