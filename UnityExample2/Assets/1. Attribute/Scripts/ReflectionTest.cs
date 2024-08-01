using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ReflectionTest : MonoBehaviour
{
    // Reflection
    // System.Reflection ���ӽ����̽��� ���Ե� ���
    // ������ Ÿ�ӿ��� ������ Ŭ����, �޼ҵ�, ��� ���� ���� �����͸� ����ϴ� Class
    // Attribute�� Ư�� ��ҿ� ���� ��Ÿ�������̹Ƿ�, Refletion�� ���ؼ� ������ ����.

    AttributeTest attTest;
    

    private void Awake()
    {
        attTest = GetComponent<AttributeTest>();
    }


    private void Start()
    {
        //attTest�� Type�� Ȯ��

        Type attTestType = typeof(AttributeTest);

        // �̷��Ե� �����ϴ�. �ڽ̵Ǿ� �־ ���� Ÿ���� ������ �� ����
        // Type attTestType = attTest.GetType();
        // �Ʒ� ���ó��
        //MonoBehaviour attTestBoxing = attTest;
        //Type attributeTestType = attTestBoxing.GetType();
        //print(attributeTestType);

        // AttributeTest ��� Ŭ������ �����͸� Ȯ���غ���
        BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
        FieldInfo[] fis =  attTestType.GetFields(bind); //�ʵ�(��� ����)


        //print(fis.Length);

        // ���� �������� ���
        foreach (FieldInfo fi in fis)
        {
            if (fi.GetCustomAttribute<MyCustomAttribute>() == null)
            {
                // FieldInfo�� MyCustomAttribute ��Ʈ����Ʈ�� �����Ǿ� ���� ������ �ǳʶ�
                continue;
            }

            MyCustomAttribute customAtt = fi.GetCustomAttribute<MyCustomAttribute>();

           //print($" Name : {fi.Name}, Type : {fi.FieldType}, AttName : {customAtt.name}, AttValue : {customAtt.value}");
        }

        //TestMethod�� MethodInfo �Ǵ� MemberInfo�� Ž���ؼ� MethodMessageAttribute.msg�� ����غ�����




        // ���� ���� ���
        MethodInfo testMethodInfo = attTestType.GetMethod("TestMethod"); // SendMessage�� ����� ���


        bind = BindingFlags.NonPublic | BindingFlags.Instance;
        
        MethodInfo[] mis = attTestType.GetMethods(bind);

        // �޼ҵ� �������� ���
        foreach (MethodInfo mi in mis)
        {
            if (mi.GetCustomAttribute<MethodMessageAttribute>() == null)
            {
                continue;
            }


            var msgAtt = mi.GetCustomAttribute<MethodMessageAttribute>();

            print(msgAtt.msg);

            mi.Invoke(attTest, null);
        }

    }

}
