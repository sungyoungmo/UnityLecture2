using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.MonsterState
{
	public class MonsterStateMachine : MonoBehaviour
	{

		public MonsterIdleState monsterIdle;
		public MonsterChaseState monsterChase;
		public MonsterAttackState monsterAttack;

		public MonsterStateBase currentState;

		public Monster monster;

        private void Awake()
        {
			monster = GetComponent<Monster>();
        }


        private void Start()
        {
			monsterIdle = new MonsterIdleState();
			monsterChase = new MonsterChaseState();
			monsterAttack = new MonsterAttackState();

			monsterIdle.Initialize(monster);
			monsterChase.Initialize(monster);
			monsterAttack.Initialize(monster);		
			
			currentState = monsterIdle;
			monsterIdle.Enter();

			
		}

		public void Transition(MonsterStateBase state)
		{
			if (currentState == state)
            {
				return;
            }
				

			currentState.Exit();
			currentState = state;
			currentState.Enter();
		}
		private void Update()
		{
			currentState.Update();
		}


	}
}
