using System;
using DG.Tweening;
using TestTask.Attribute;
using TestTask.Core;
using TestTask.Helper;
using TestTask.Movement;
using UnityEngine;

namespace TestTask.Fight
{
    public class Attack : Attributes, IBehaviour
    {
        [Header("Defines the time between attack")]

        [SerializeField] float timeBetweenAttacks;

        [Header("Add area weapon here")]
        [SerializeField] GameObject areaWeapon;
        [Header("Add triple shot weapon here")]
        [SerializeField] GameObject tripleAttack;

        [Header("Point of launch of weapon")]
        [SerializeField] Transform pointOfLaunch;

        [Header("Add reward system from within the scene to assign playerskill class")]
        [SerializeField] RewardSystem rewardSystem;

       
        PlayerSkill playerSkill;
        PlayerBehaviour playerBehaviour;
        float tempTime;
        bool canAttack = false;
       [SerializeField] GameObject weapon;
        Attack attack;
        public new void Awake()
        {
            base.Awake();
            attack = GetComponent<Attack>();
            playerSkill = new PlayerSkill();
            rewardSystem.SetPlayerSkill(playerSkill);
            playerSkill.OnSkillUnLocked += PlayerSkill_OnSkillUnLocked;
        }


        private void PlayerSkill_OnSkillUnLocked(PlayerSkill.OnSkillUnlock e)
        {
            print(e.skillType);
            if (e.skillType == PlayerSkill.SkillType.Heal)
            {
                IncreaseHealth(25f);
                return;
            }
            if (e.skillType == PlayerSkill.SkillType.AttackRate)
            {
                ChangeTimeBetweenAttack(0.05f);
                return;
            }
           
            if (e.skillType == PlayerSkill.SkillType.TripleShot)
            {
                weapon = tripleAttack;
                return;
            }
            if (e.skillType == PlayerSkill.SkillType.IncreaseSpeed)
            {
                GetComponent<PlayerMovement>().speed = 7.5f;
                return;
            }
        }

      
        private void Start()
        {
            tempTime = timeBetweenAttacks;
            playerBehaviour = GetComponent<PlayerBehaviour>();
        }
        public void ChangeTimeBetweenAttack(float amount)
        {
            timeBetweenAttacks -= amount;
            if (timeBetweenAttacks < 0.5f)
            {
                timeBetweenAttacks = 0.5f;
            }
        }
        private void Update()
        {
            if (!canAttack) return;
            tempTime -= Time.deltaTime;
            if (tempTime <= 0)
            {
                AttackEnemy();
            }
        }

        ///<summary>
        ///Attack the enemy
        ///</summary>
        private void AttackEnemy()
        {
            LookTowardsEnemy();
            tempTime = timeBetweenAttacks;
            Instantiate(weapon, pointOfLaunch.position, pointOfLaunch.rotation);
        }


        ///<summary>
        ///Make the player looks towards the enemy during attack
        ///</summary>
        private void LookTowardsEnemy()
        {
            Transform enemy = GameHandler.instance.FindClosetEnemy(transform);
            if (enemy == null)
            {
                canAttack = false;
                return;
            }
            Vector3 position = enemy.position;
            transform.DOLookAt(position, 0.2f);
        }

        ///<summary>
        ///let player attack enemy
        ///</summary>
        public void SetCanAttack(Transform enemy)
        {
            LookTowardsEnemy();
            playerBehaviour.ChangeBehaviour(this);
            canAttack = true;
        }
        public void CancelBehavior()
        {
            canAttack = false;
        }
        protected override void IsDeath(float hp)
        {
            if (hp <= 0)
            {
                GameHandler.instance.GameOver();
            }
        }
    }
}