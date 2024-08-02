using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Skill
{
    public class Fireball : SkillBehaviour
    {
        public FireballProjectile projectile;   // ����ü ������
        public float projectileSpeed;


        public override void Apply()
        {
            print("���̾ ����");
        }
        
        public override void Use()
        {
            Transform shotpoint = context.owner.shotPoint;

            var obj = Instantiate(projectile, shotpoint.position, shotpoint.rotation);

            obj.SetProjectile(projectileSpeed);

            Destroy(obj, 3);

            print("���̾ �߻�");
        }

        public override void Remove()
        {
            print("���̾ ����");
        }
    }
}
