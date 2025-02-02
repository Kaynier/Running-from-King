
using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{   
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float adjustChangeMoveSpeedAmount= -2f;

    LevelGenerator levelGenerator;
    const string hitString = "Hit";
    float cooldownTimer = 0f;

    void Update(){
        cooldownTimer += Time.deltaTime;
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    void OnCollisionEnter(Collision other){
        if (cooldownTimer < collisionCooldown) return;
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        animator.SetTrigger("Hit");
        cooldownTimer = 0f;
    }
}
