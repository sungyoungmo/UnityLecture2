using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.State
{
    public class MoveState : BaseState
    {
        public override void Enter()
        {
           
        }

        public override void Update()
        {
            player.text.text = $"{GetType().Name} : {player.moveDistance:n1}";
            player.moveDistance += Time.deltaTime;
        }

        public override void Exit()
        {
            Debug.Log("이동 상태 종료");
        }
    }
}

