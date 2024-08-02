using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.MonsterState
{
	public class MonsterAttackState : MonsterStateBase
	{
        public override void Enter()
        {
            monster.ms = MonsterState.IDLE;
            Debug.Log("공격 상태 시작");
        }

        public override void Update()
        {


        }

        public override void Exit()
        {
            Debug.Log("공격 상태 종료");
        }
    }
}
