using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Captain.Command;

namespace Captain.Command
{
    public class MoveCharacterRight : ScriptableObject, ICaptainCommand
    {
        private float speed = 5.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(this.speed, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
