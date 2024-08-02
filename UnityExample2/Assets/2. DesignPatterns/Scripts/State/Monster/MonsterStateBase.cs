using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.MonsterState
{
	public class MonsterStateBase : MonoBehaviour
	{
		public Monster monster;


		public virtual void Enter()
        {

        }

		public virtual void Update()
		{

		}

		public virtual void Exit()
		{

		}

		public void Initialize(Monster monster)
        {
			this.monster = monster;
        }
	}
}
