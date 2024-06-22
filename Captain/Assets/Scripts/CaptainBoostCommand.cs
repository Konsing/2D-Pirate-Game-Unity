using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Captain.Command
{
    public class CaptainBoostCommand : MonoBehaviour, ICaptainCommand
    {
        private float originalSpeed;
        private float boostDuration = 5.0f;
        private float boostCooldown = 10.0f;
        private float speedMultiplier = 2.0f;
        private bool isBoostActive = false;
        private float boostTimer = 0.0f;

        public void Execute(GameObject captain)
        {
            if (isBoostActive || boostTimer > 0)
                return;

            var captainController = captain.GetComponent<CaptainController>();
            if (captainController != null)
            {
                originalSpeed = captainController.speed;
                captainController.speed *= speedMultiplier;

                isBoostActive = true;
                boostTimer = boostCooldown;
                StartCoroutine(EndBoost(captainController));
            }
        }

        private IEnumerator EndBoost(CaptainController captainController)
        {
            yield return new WaitForSeconds(boostDuration);
            captainController.speed = originalSpeed;
            isBoostActive = false;
            StartCoroutine(CooldownTimer());
        }

        private IEnumerator CooldownTimer()
        {
            while (boostTimer > 0)
            {
                boostTimer -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
