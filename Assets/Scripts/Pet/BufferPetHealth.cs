using UnityEngine;

namespace Nightmare
{
    public class BufferPetHealth : EnemyPetHealth
    {
        BufferPetBehaviour bufferPetBehaviour;

        override protected void Awake()
        {
            base.Awake();
            bufferPetBehaviour = GetComponent<BufferPetBehaviour>();
        }

        public override void Death()
        {
            Debug.Log("BufferPetHealth Death");
            base.Death();
            bufferPetBehaviour.StopBuff();
        }
    }
}