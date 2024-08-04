using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.State
{
    public class IdleState : BaseState
    {

        public override void Enter()
        {
            player.stateStay = 0;
        }

        public override void Update()
        {
            player.text.text = $"{GetType().Name} : {player.stateStay:n0}";
            player.stateStay += Time.deltaTime;
        }

        public override void Exit()
        {
            //Debug.Log("��� ���� ����");
            
        }
    }
}