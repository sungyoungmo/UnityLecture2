using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill 
{
    public class SkillContext : MonoBehaviour
    {
        public Player owner;

        // internal  : ���� ����� �������� ���� �����ϹǷ� ���� ���� Ŭ���������� ���� �����ϳ�, ����Ƽ ������ ������ �� �����Ƿ�
        // inpector���� ������ �ȵ�.
        internal List<SkillBehaviour> skills = new();

        public SkillBehaviour currentSkill;

        public void AddSkill(SkillBehaviour skill)
        {
            skill.context = this;
            skills.Add(skill);
        }

        public void SetCurrentSkill(int index)
        {
            if (index >= skills.Count) return;

            currentSkill?.Remove();
            currentSkill = skills[index];
            currentSkill?.Apply();
        }

        public void UseSkill()
        {
            currentSkill.Use();
        }
    }
}

