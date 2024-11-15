using UnityEngine;

public interface ICollisionHandler
{
    void HandleCollision(Collision2D collision, GameObject enemyObject);
}