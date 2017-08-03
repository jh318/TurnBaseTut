using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class BattleStateMachine : MonoBehaviour {

	public enum PerformAction{
		WAIT,
		TAKEACTION,
		PERFORMACTION
	}

	public PerformAction battleStates;

	public List<HandleTurn> PerformList = new List<HandleTurn>();

	public List<GameObject> HerosInGame = new List<GameObject>();
	public List<GameObject> EnemysInBattle = new List<GameObject>();


	void Start () {
		battleStates = PerformAction.WAIT;
		EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
	}
	
	void Update () {
		switch(battleStates){
			case(PerformAction.WAIT):
				break;
			case(PerformAction.TAKEACTION):
				break;
			case(PerformAction.PERFORMACTION):
				break;
		}
	}
}
