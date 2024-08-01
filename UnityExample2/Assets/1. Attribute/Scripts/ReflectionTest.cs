using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ReflectionTest : MonoBehaviour
{
    // Reflection
    // System.Reflection 네임스페이스에 포함된 기능
    // 컴파일 타임에서 생성된 클래스, 메소드, 멤버 변수 등의 데이터를 취급하는 Class
    // Attribute는 특정 요소에 대한 메타데이터이므로, Refletion에 의해서 접근이 가능.

    AttributeTest attTest;
    

    private void Awake()
    {
        attTest = GetComponent<AttributeTest>();
    }


    private void Start()
    {
        //attTest의 Type을 확인

        Type attTestType = typeof(AttributeTest);

        // 이렇게도 가능하다. 박싱되어 있어도 원래 타입을 가져올 수 있음
        // Type attTestType = attTest.GetType();
        // 아래 방식처럼
        //MonoBehaviour attTestBoxing = attTest;
        //Type attributeTestType = attTestBoxing.GetType();
        //print(attributeTestType);

        // AttributeTest 라는 클래스의 데이터를 확인해보자
        BindingFlags bind = BindingFlags.Public | BindingFlags.Instance;
        FieldInfo[] fis =  attTestType.GetFields(bind); //필드(멤버 변수)


        //print(fis.Length);

        // 변수 가져오는 방법
        foreach (FieldInfo fi in fis)
        {
            if (fi.GetCustomAttribute<MyCustomAttribute>() == null)
            {
                // FieldInfo에 MyCustomAttribute 어트리뷰트가 부착되어 있지 않으면 건너뜀
                continue;
            }

            MyCustomAttribute customAtt = fi.GetCustomAttribute<MyCustomAttribute>();

           //print($" Name : {fi.Name}, Type : {fi.FieldType}, AttName : {customAtt.name}, AttValue : {customAtt.value}");
        }

        //TestMethod의 MethodInfo 또는 MemberInfo를 탐색해서 MethodMessageAttribute.msg를 출력해보세요




        // 가장 쉬운 방법
        MethodInfo testMethodInfo = attTestType.GetMethod("TestMethod"); // SendMessage랑 비슷한 용법


        bind = BindingFlags.NonPublic | BindingFlags.Instance;
        
        MethodInfo[] mis = attTestType.GetMethods(bind);

        // 메소드 가져오는 방법
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
