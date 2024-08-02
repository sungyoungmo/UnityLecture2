using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.MonsterState
{
    public enum MonsterState
    {
        IDLE,
        CHASE,
        ATTACK
    }

    public class Monster : MonoBehaviour
    {
        public MonsterState ms;

        MonsterStateMachine msm;

        public Renderer[] bodyRenderers;
        public Renderer[] eyeRenderers;

        public Color bodyDayColor;
        public Color eyeDayColor;

        public Color bodyNightColor;
        public Color eyeNightColor;


        private void Awake()
        {
            msm = GetComponent<MonsterStateMachine>();
        }

        private void Start()
        {
            //GameManager.Instance.OnMonsterSpawn(this);

            GameManager.Instance.onDayNightChange += OnDayNightChange;
            OnDayNightChange(GameManager.Instance.isDay);
        }

        private void Update()
        {
            ChangeMonsterState();
        }

        private void OnDestroy()
        {
            //GameManager.Instance.OnMonsterDespawn(this);

            GameManager.Instance.onDayNightChange -= OnDayNightChange;
        }


        public void ChangeMonsterState()
        {
            Vector3 distance = new Vector3
                (
                    transform.position.x - GameManager.Instance.playerObj.transform.position.x,
                    0,
                    transform.position.z - GameManager.Instance.playerObj.transform.position.z
                );

            if (Vector3.Magnitude(distance) < 1)
            {
                msm.Transition(msm.monsterAttack);
            }
            if (Vector3.Magnitude(distance) < 3 && Vector3.Magnitude(distance) > 1)
            {
                msm.Transition(msm.monsterChase);
            }
            if (Vector3.Magnitude(distance) > 3)
            {
                msm.Transition(msm.monsterIdle);
            }

            print(Vector3.Magnitude(distance));
        }


        public void OnDayNightChange(bool isDay)
        {
            if (isDay)
            {
                DayColor();
            }
            else
            {
                NightColor();
            }
        }

        public void DayColor()
        {
            foreach (Renderer r in bodyRenderers)
            {
                r.material.color = bodyDayColor;
            }

            foreach (Renderer r in eyeRenderers)
            {
                r.material.color = eyeDayColor;
            }
        }


        public void NightColor()
        {
            foreach (Renderer r in bodyRenderers)
            {
                r.material.color = bodyNightColor;
            }

            foreach (Renderer r in eyeRenderers)
            {
                r.material.color = eyeNightColor;
            }
        }
    }

}


