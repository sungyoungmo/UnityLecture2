using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MyProject.Skill;
using MyProject.State;


// 상태 패턴 구현을 위해, 현재 움직이는 중인지 아니면 멈춰있는지 판단하고 싶음
public class Player : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Move = 1,
        // Jump = 2,
        // Attack = 3
    }

    CharacterController cc;
    public float moveSpeed= 1;
    public TextMeshPro text;
    public State currentState; // 0이면 멈춰있는 상태, 1이면 움직이는 상태, 추후 확장이 되면 2면 공격, 3이면 점프 등등... 설정
    
    public float stateStay; // 현재 상태에 머문 시간.
    public float moveDistance; // 총 이동 시간


    public Transform shotPoint;

    private SkillContext skillContext;
    private StateMachine stateMachine;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        stateMachine = GetComponent<StateMachine>();
        skillContext = GetComponentInChildren<SkillContext>();
        SkillBehaviour[] skills = skillContext.GetComponentsInChildren<SkillBehaviour>();

        foreach (SkillBehaviour sk in skills)
        {
            skillContext.AddSkill(sk);
        }

        skillContext.SetCurrentSkill(0);
    }

    private void Start()
    {
        //currentState = State.Idle;
    }


    private void Update()
    {
        Move();
        //StateUpdate();

        if (Input.GetButtonDown("Fire1"))
        {
            skillContext.UseSkill();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skillContext.SetCurrentSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skillContext.SetCurrentSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skillContext.SetCurrentSkill(2);
        }


    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(x, 0, y);
        
        cc.Move(moveDir * moveSpeed * Time.deltaTime);

        if (moveDir.magnitude < 0.1f)
        {
            stateMachine.Transition(stateMachine.idleState);
        }
        else
        {
            stateMachine.Transition(stateMachine.moveState);
        }

        /*magnitude : vector의 길이*/
            //if (moveDir.magnitude < 0.1f) // 상태 전이를 결정하는 조건 (condition)
            //{
            //    ChangeState(State.Idle);
            //}
            //else
            //{
            //    ChangeState(State.Move);
            //}

    }

    public void ChangeState/*Transition이라고 쓰기도 함*/(State nextState)
    {

        
        if (currentState != nextState)
        {
            // exit
            switch (currentState)
            {
                case State.Idle:
                    print("대기 상태 종료");
                    break;
                case State.Move:
                    print("이동 상태 종료");
                    break;
            }


            switch (nextState)
            {
                // enter
                case State.Idle:
                    print("대기 상태 시작");
                    break;
                case State.Move:
                    print("이동 상태 시작");
                    break;
            }

            currentState = nextState;
            stateStay = 0;
        }

        
        
    }


    public void StateUpdate()
    {
        // 현재 상태에 따른 행동 정의
        switch (currentState)
        {
            case State.Idle:
                // 현재 상태가 Idle일 때 할 것
                //text.text = $"{State.Idle} state : {stateStay:n0}";
                // 위에 처럼 단순화 가능
                text.text = $"{State.Idle} state : {stateStay.ToString("n0")}";
                break;
            case State.Move:
                // 현재 상태가 Move일 때 할 것
                text.text = $"{State.Move} state : {stateStay.ToString("n0")}";
                break;
        }

        stateStay += Time.deltaTime;
    }
}
