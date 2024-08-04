using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.Chest
{
	public class Chest : MonoBehaviour
	{

        private void Start()
        {
            GameManager.Instance.onDayNightChange += OnDayNightChangeMimic;

            GameManager.Instance.OnChestSpawn(this);
        }

        private void OnEnable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnChestSpawn(this);
            }
        }


        private void OnDisable()
        {
            GameManager.Instance.OnChestDespawn(this);
        }


        private void OnDestroy()
        {
            GameManager.Instance.onDayNightChange -= OnDayNightChangeMimic;
        }

        public void OnDayNightChangeMimic(bool isDay)
        {
            if (isDay)
            {
                gameObject.name = "chest";
            }
            else
            {
                gameObject.name = "mimic";
            }

        }

    }
}
