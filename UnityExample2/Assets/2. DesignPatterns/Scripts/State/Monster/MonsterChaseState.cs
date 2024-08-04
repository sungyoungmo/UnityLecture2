using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.MonsterState
{
	public class MonsterChaseState : MonsterStateBase
	{
        public MonsterChaseState()
        {

        }

        public override void Enter()
        {
            monster.ms = MonsterState.CHASE;
            //Debug.Log("추격 상태 시작");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            //Debug.Log("추격 상태 종료");
        }
    }
}
