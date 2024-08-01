using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTest : MonoBehaviour
{
    // Attribute (속성, 특성)
    // C#에서의 Attribue의 정확한 의미 : 필드, 메소드등 멤버에 대한 메타 데이터를 생성할 수 있는 [클래스]다.
    // Attribute를 직접 작성하기 위해서는 System.Attribute 클래스를 상속하고, 클래스 명 뒤에 Attribute를 붙인다.
    // Attribute 클래스를 활용하기 위해선느 특정 멤버(클래스, 변수, 함수(Propertie 포함))등의 선언 앞에 [Attribute 이름에서 Attribute를 뺀 이름]

    private static int myStaticInt;

    // Attribute 생략 가능
    [MyCustomAttribute(name = "MyIntager", value = 1)]
    public int myInt;   // "멤버" 변수
    
    [MyCustom] // MyCustomAttribute의 기본 생성자를 호출하여 Attribute(메타 데이터) 생성
    public int myInt2;

    public string myString; // TextArea Attribute가 부착되지 않은 string 멤버 변수
    
    [TextArea(minLines: 1, 10)]
    public string myTextArea; // TextArea Attribute가 부착된 string 멤버 변수


    [Space(300)]
    public int anotherInt;

    [MethodMessage("이건 Private 메소드입니다.")]
    private void TestMethod()
    {
        print("비밀스러운 Test중.");
    }
}

public class MyCustomAttribute : System.Attribute
{
    public string name;
    public float value;

    public MyCustomAttribute()
    {
        name = "No Name";
        value = -1;
    }



}

[System.AttributeUsageAttribute(System.AttributeTargets.Method)] // Attribute에 제약 조건을 설정할 때 클래스 앞에 부착할 Attribute
public class MethodMessageAttribute : System.Attribute // Method에 붙일 Attribute
{
    public string msg;

    public MethodMessageAttribute(string msg)
    {
        this.msg = msg;
    }
}