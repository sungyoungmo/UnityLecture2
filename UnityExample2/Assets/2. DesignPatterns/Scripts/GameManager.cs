using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;
using System;

public class GameManager : MonoBehaviour
{

    // 어떤 걸 싱글톤 패턴으로 만들면 좋은가?
    // 한가지만 고민해보면 된다. : 단일 책임 원칙에 부합하는 녀석인가?
    public static GameManager Instance { get; private set; }
    public new Light light;


    public float dayLength = 5;
    public bool isDay = true;


    public GameObject playerObj;

    // 옵저버 패턴 : 특정 임무를 수행하는 객체에게 상태 변화 또는 특정 이벤트의 호출 조건이 발생할 시
    // 해당 이벤트 호출이 필요한 객체들이 "나도 상태 변하면 알려주세요." 라고 등록(Subscribe)해 놓는 형태의 디자인 패턴. 


    private List<MyProject.MonsterState.Monster> monsters = new(); // 구독자들
    private List<MyProject.Chest.Chest> chests = new();

    // C#의 event는 옵저버 패턴 구현에 최적화된 구조로 만들어져 있으므로 event를 활용하는 것만으로도 옵저버 패턴을 적용했다고 볼 수 있음
    public event Action<bool> onDayNightChange;

    Coroutine EarthquakeCoroutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private float dayTemp;

    private void Update()
    {
        if (Time.time - dayTemp > dayLength)
        {
            dayTemp = Time.time;
            isDay = !isDay;
            light.gameObject.SetActive(isDay);

            //foreach (Monster monster in monsters)
            //{
            //    monster.OnDayNightChange(isDay);
            //}


            onDayNightChange?.Invoke(isDay);

        }


        if (EarthquakeCoroutine == null && UnityEngine.Random.Range(1, 1000) == 14 && !isDay)
        {
            EarthquakeCoroutine = StartCoroutine(Earthquake());
        }
    }

    public void OnMonsterSpawn(MyProject.MonsterState.Monster monster)
    {
        monsters.Add(monster);
        monster.OnDayNightChange(isDay);
    }

    public void OnMonsterDespawn(MyProject.MonsterState.Monster monster)
    {
        monsters.Remove(monster);
    }

    public void OnChestSpawn(MyProject.Chest.Chest chest)
    {
        chests.Add(chest);
        chest.OnDayNightChangeMimic(isDay);
    }

    public void OnChestDespawn(MyProject.Chest.Chest chest)
    {
        chests.Remove(chest);
    }

    IEnumerator Earthquake()
    {
        print("Earthquake");

        var chestsList = new List<MyProject.Chest.Chest>(chests);

        foreach (var c in chestsList)
        {
            c.gameObject.SetActive(false);
            chests.Remove(c);
        }

        yield return new WaitForSeconds(5.0f);

        EarthquakeCoroutine = null;
    }

}