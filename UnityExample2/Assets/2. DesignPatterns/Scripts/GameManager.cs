using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;
using System;

public class GameManager : MonoBehaviour
{

    // � �� �̱��� �������� ����� ������?
    // �Ѱ����� ����غ��� �ȴ�. : ���� å�� ��Ģ�� �����ϴ� �༮�ΰ�?
    public static GameManager Instance { get; private set; }
    public new Light light;


    public float dayLength = 5;
    public bool isDay = true;


    public GameObject playerObj;

    // ������ ���� : Ư�� �ӹ��� �����ϴ� ��ü���� ���� ��ȭ �Ǵ� Ư�� �̺�Ʈ�� ȣ�� ������ �߻��� ��
    // �ش� �̺�Ʈ ȣ���� �ʿ��� ��ü���� "���� ���� ���ϸ� �˷��ּ���." ��� ���(Subscribe)�� ���� ������ ������ ����. 


    private List<MyProject.MonsterState.Monster> monsters = new(); // �����ڵ�
    private List<MyProject.Chest.Chest> chests = new();

    // C#�� event�� ������ ���� ������ ����ȭ�� ������ ������� �����Ƿ� event�� Ȱ���ϴ� �͸����ε� ������ ������ �����ߴٰ� �� �� ����
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