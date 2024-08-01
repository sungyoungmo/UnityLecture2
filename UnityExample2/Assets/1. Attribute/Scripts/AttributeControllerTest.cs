using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeControllerTest : MonoBehaviour
{

    //public new Renderer renderer;

    // ���⼭ ���� ǥ�ð� �ߴµ� ���� ���� �ϸ� ����� �ٵ� �ִ����� �������
    [Color(0,1,0,1), Size(2,3,2, MODIFYINGTYPE.MULTIPLY)]
    public Renderer renderer;


    [SerializeField,Color(r:1,b:0.5f), Size(200,120,100, rect = RECTSCALEORSIZE.RECTSIZE)]
    private Graphic graphic;


    [Color]
    public float notRendererOrGraphic;
}