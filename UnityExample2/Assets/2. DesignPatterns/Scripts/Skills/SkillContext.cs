using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill 
{
    public class SkillContext : MonoBehaviour
    {
        public Player owner;

        // internal  : 같은 어셈블리 내에서만 접근 가능하므로 내가 만든 클래스끼리는 접근 가능하나, 유니티 엔진은 접근할 수 없으므로
        // inpector에서 수정이 안됨.
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

