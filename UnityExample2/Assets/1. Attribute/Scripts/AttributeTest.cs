using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTest : MonoBehaviour
{
    // Attribute (�Ӽ�, Ư��)
    // C#������ Attribue�� ��Ȯ�� �ǹ� : �ʵ�, �޼ҵ�� ����� ���� ��Ÿ �����͸� ������ �� �ִ� [Ŭ����]��.
    // Attribute�� ���� �ۼ��ϱ� ���ؼ��� System.Attribute Ŭ������ ����ϰ�, Ŭ���� �� �ڿ� Attribute�� ���δ�.
    // Attribute Ŭ������ Ȱ���ϱ� ���ؼ��� Ư�� ���(Ŭ����, ����, �Լ�(Propertie ����))���� ���� �տ� [Attribute �̸����� Attribute�� �� �̸�]

    private static int myStaticInt;

    // Attribute ���� ����
    [MyCustomAttribute(name = "MyIntager", value = 1)]
    public int myInt;   // "���" ����
    
    [MyCustom] // MyCustomAttribute�� �⺻ �����ڸ� ȣ���Ͽ� Attribute(��Ÿ ������) ����
    public int myInt2;

    public string myString; // TextArea Attribute�� �������� ���� string ��� ����
    
    [TextArea(minLines: 1, 10)]
    public string myTextArea; // TextArea Attribute�� ������ string ��� ����


    [Space(300)]
    public int anotherInt;

    [MethodMessage("�̰� Private �޼ҵ��Դϴ�.")]
    private void TestMethod()
    {
        print("��н����� Test��.");
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

[System.AttributeUsageAttribute(System.AttributeTargets.Method)] // Attribute�� ���� ������ ������ �� Ŭ���� �տ� ������ Attribute
public class MethodMessageAttribute : System.Attribute // Method�� ���� Attribute
{
    public string msg;

    public MethodMessageAttribute(string msg)
    {
        this.msg = msg;
    }
}