using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.MonsterState
{
	public class MonsterAttackState : MonsterStateBase
	{
        public MonsterAttackState()
        {

        }


        public override void Enter()
        {
            monster.ms = MonsterState.ATTACK;
            //Debug.Log("���� ���� ����");
        }

        public override void Update()
        {


        }

        public override void Exit()
        {
            //Debug.Log("���� ���� ����");
        }
    }
}
