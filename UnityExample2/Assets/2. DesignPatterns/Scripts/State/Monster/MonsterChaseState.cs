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
            //Debug.Log("�߰� ���� ����");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            //Debug.Log("�߰� ���� ����");
        }
    }
}
