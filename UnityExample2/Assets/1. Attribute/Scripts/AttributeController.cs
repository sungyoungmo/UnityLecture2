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
        // ColorAttribue를 가진 필드를 찾는다.
        // BindingFlags : public 이거나 private 상관없이 static이 아닌 동적 할당 멤버만 탐색.

        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);


        foreach (var monoBehaviour in monoBehaviours)
        {
            Type type = monoBehaviour.GetType();

            // 콜렉션(배열, 리스트 등)에서 특정 조건에 부합하는 요소만 가져오려 할 경우,
            // foreach 또는 List.Find 등 다소 복잡한 절차를 거쳐야 함
            //List<FieldInfo> fields = new List<FieldInfo>(type.GetFields(bind));
            //fields.FindAll(null);


            ////Linq 문법을 활용하여 이를 간소화할 수 있음.
            ////1.Linq에 정의된 확장 메서드 이용하는 방법
            //IEnumerable<FieldInfo> colorAttachedFields = type.GetFields(bind).Where(field => field.GetCustomAttribute<ColorAttribute>() != null);

            ////2.Linq를 통해 쿼리문과 비슷한 문법을 활용하는 방법

            //colorAttachedFields = from field in type.GetFields(bind)
            //                      where field.GetCustomAttribute<ColorAttribute>() != null
            //                      select field;

            ////이렇게 hasattribute를 사용하기 가능
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
            //        Debug.LogError("저런, Color Attribute를 잘못 붙이셨네요 ㅎㅎ");
            //        //  둘 다 같음
            //        throw new Exception("저런, Color Attribute를 잘못 붙이셨네요 ㅎㅎ");
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
                    Debug.LogError("저런, Color Attribute를 잘못 붙이셨네요 ㅎㅎ");
                    //  둘 다 같음
                    throw new Exception("저런, Color Attribute를 잘못 붙이셨네요 ㅎㅎ");
                }

            }

        }

    }


}

// Color를 조절할 수 있는 컴포넌트 또는 오브젝트에 [Color]라는 어트리뷰트를 붙여서 색을 설정하고 싶음
// allowMultiple 여러개 쓸 수 있는지, inherited 상속받아 가능한지 
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ColorAttribute : Attribute
{
    public Color color;

    //public ColorAttribute(Color color)// Attribute의 생성자에서는 리터럴 타입의 매개변수만 할당 가능
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
    // 특정 어트리뷰트를 가지고 있는지 여부만 확인하고 싶을 때 쓸 확장 메서드

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