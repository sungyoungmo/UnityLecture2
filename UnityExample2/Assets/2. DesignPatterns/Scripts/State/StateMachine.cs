using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.State
{
    // �÷��̾�� ���� ������Ʈ�� �����鼭 �÷��̾��� ���¸� �������ִ� ������Ʈ
    [RequireComponent(typeof(Player))]
    public class StateMachine : MonoBehaviour
    {
        public MoveState moveState;
        public IdleState idleState;
        public Player player;

        public IState currentState;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Start()
        {
            moveState = new MoveState();
            idleState = new IdleState();

            moveState.Initialize(player);
            idleState.Initialize(player);

            currentState = idleState;
            idleState.Enter();
        }

        public void Transition(IState state)
        {
            if (currentState == state) return;

            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }

        private void Update()
        {
            currentState.Update();
        }

        // �ν����Ϳ��� reset �ϸ� ���⿡ �ִ� �� ��������
        private void Reset()
        {
            player = GetComponent<Player>();
        }
    }
}

