//MonoBehaviour�� ������� ���� �Ϲ����� ��ü�� �̱��� ���� ���� ���
public class NonMonoBehaviourSingleton
{
    // ���� instance �ʵ�� private static �ʵ�� ����
    private static NonMonoBehaviourSingleton instance;  

    // �ܺ� ��ü�� instance�� �����ϱ� ���ؼ��� Getter �޼ҵ� �Ǵ� C#�� Get ���� property�� �б� �������� ������ �� �ֵ��� ��.

    public static NonMonoBehaviourSingleton Instance 
    {
        get 
        {
            if (instance == null)
            {
                instance = new NonMonoBehaviourSingleton();
            }
            return instance;
        }
    }

    // �ٸ� ��ü���� �����ڸ� ȣ������ ���ϵ��� �⺻ �������� ���� �����ڸ� private�� ��ȣ
    private NonMonoBehaviourSingleton() { }

    public static NonMonoBehaviourSingleton GetInstance()
    {
        if (instance == null)
        {
            instance = new NonMonoBehaviourSingleton();
        }
        return instance;
    }
}
