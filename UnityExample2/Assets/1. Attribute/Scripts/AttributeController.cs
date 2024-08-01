using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class AttributeController : MonoBehaviour
{
    private void Start()
    {
        // ColorAttribue�� ���� �ʵ带 ã�´�.
        // BindingFlags : public �̰ų� private ������� static�� �ƴ� ���� �Ҵ� ����� Ž��.

        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);


        foreach (var monoBehaviour in monoBehaviours)
        {
            Type type = monoBehaviour.GetType();

            // �ݷ���(�迭, ����Ʈ ��)���� Ư�� ���ǿ� �����ϴ� ��Ҹ� �������� �� ���,
            // foreach �Ǵ� List.Find �� �ټ� ������ ������ ���ľ� ��
            //List<FieldInfo> fields = new List<FieldInfo>(type.GetFields(bind));
            //fields.FindAll(null);


            ////Linq ������ Ȱ���Ͽ� �̸� ����ȭ�� �� ����.
            ////1.Linq�� ���ǵ� Ȯ�� �޼��� �̿��ϴ� ���
            //IEnumerable<FieldInfo> colorAttachedFields = type.GetFields(bind).Where(field => field.GetCustomAttribute<ColorAttribute>() != null);

            ////2.Linq�� ���� �������� ����� ������ Ȱ���ϴ� ���

            //colorAttachedFields = from field in type.GetFields(bind)
            //                      where field.GetCustomAttribute<ColorAttribute>() != null
            //                      select field;

            ////�̷��� hasattribute�� ����ϱ� ����
            //colorAttachedFields = from field in type.GetFields(bind)
            //                      where field.HasAttribute<ColorAttribute>()
            //                      select field;


            //foreach (FieldInfo field in colorAttachedFields)
            //{
            //    ColorAttribute att = field.GetCustomAttribute<ColorAttribute>();
            //    object value = field.GetValue(monoBehaviour);

            //    print(value);


            //    if (value is Renderer rend)
            //    {
            //        rend.material.color = att.color;

            //    }
            //    else if (value is UnityEngine.UI.Graphic graph)
            //    {
            //        graph.color = att.color;
            //    }
            //    else
            //    {
            //        Debug.LogError("����, Color Attribute�� �߸� ���̼̳׿� ����");
            //        //  �� �� ����
            //        throw new Exception("����, Color Attribute�� �߸� ���̼̳׿� ����");
            //    }
            //}

            IEnumerable<FieldInfo> sizeAttachedFields = type.GetFields(bind).Where(field => field.GetCustomAttribute<SizeAttribute>() != null);

            foreach (FieldInfo field in sizeAttachedFields)
            {
                SizeAttribute att = field.GetCustomAttribute<SizeAttribute>();
                object value = field.GetValue(monoBehaviour);


                if (value is Renderer rend)
                {
                    if (att.mod == MODIFYINGTYPE.SET)
                    {
                        rend.transform.localScale = att.scale;
                    }
                    else
                    {
                        Vector3 vec = rend.transform.localScale;
                        vec.x = att.scale.x * vec.x;
                        vec.y = att.scale.y * vec.y;
                        vec.z = att.scale.z * vec.z;

                        rend.transform.localScale = vec;
                    }

                }
                else if (value is UnityEngine.UI.Graphic graph)
                {
                    if (att.rect == RECTSCALEORSIZE.RECTSCALE)
                    {
                        if (att.mod == MODIFYINGTYPE.SET)
                        {
                            graph.transform.localScale = att.scale;
                        }
                        else
                        {
                            Vector3 vec = graph.transform.localScale;
                            vec.x = att.scale.x * vec.x;
                            vec.y = att.scale.y * vec.y;
                            vec.z = att.scale.z * vec.z;

                            graph.transform.localScale = vec;
                        }
                    }
                    else
                    {
                        if (att.mod == MODIFYINGTYPE.SET)
                        {
                            graph.rectTransform.sizeDelta = att.scale;
                        }
                        else
                        {
                            Vector3 vec = graph.transform.localScale;
                            vec.x = att.scale.x * vec.x;
                            vec.y = att.scale.y * vec.y;
                            vec.z = att.scale.z * vec.z;

                            graph.rectTransform.sizeDelta = vec;
                        }
                    }



                    
                }
                else
                {
                    Debug.LogError("����, Color Attribute�� �߸� ���̼̳׿� ����");
                    //  �� �� ����
                    throw new Exception("����, Color Attribute�� �߸� ���̼̳׿� ����");
                }

            }

        }

    }


}

// Color�� ������ �� �ִ� ������Ʈ �Ǵ� ������Ʈ�� [Color]��� ��Ʈ����Ʈ�� �ٿ��� ���� �����ϰ� ����
// allowMultiple ������ �� �� �ִ���, inherited ��ӹ޾� �������� 
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ColorAttribute : Attribute
{
    public Color color;

    //public ColorAttribute(Color color)// Attribute�� �����ڿ����� ���ͷ� Ÿ���� �Ű������� �Ҵ� ����
    //{
        
    //}

    public ColorAttribute(float r = 1, float g = 1, float b = 1, float a = 1)
    {
        color = new Color(r, g, b, a);
    }

    public ColorAttribute()
    {
        color = Color.black;
    }

}

[AttributeUsage(AttributeTargets.Field)]
public class SizeAttribute : Attribute
{
    public Vector3 scale;
    public MODIFYINGTYPE mod;
    public RECTSCALEORSIZE rect;
    

    public SizeAttribute(float x = 1, float y= 1, float z= 1, MODIFYINGTYPE modify = MODIFYINGTYPE.SET, RECTSCALEORSIZE rectScaleOrSize = RECTSCALEORSIZE.RECTSCALE)
    {
        scale = new Vector3(x, y, z);
        mod = modify;
        rect = rectScaleOrSize;
    }
    public SizeAttribute()
    {
        scale = Vector3.one;
        mod = MODIFYINGTYPE.SET;
        rect = RECTSCALEORSIZE.RECTSCALE;
    }
}


public static class AttributeHelper
{
    // Ư�� ��Ʈ����Ʈ�� ������ �ִ��� ���θ� Ȯ���ϰ� ���� �� �� Ȯ�� �޼���

    public static bool HasAttribute<T>(this MemberInfo info) where T : Attribute
    {
        return info.GetCustomAttributes(typeof(T), true).Length > 0;
    }
    
}

public enum MODIFYINGTYPE
{
    SET,
    MULTIPLY
}

public enum RECTSCALEORSIZE
{
    RECTSCALE,
    RECTSIZE
}