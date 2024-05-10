using UnityEngine;

namespace Nightmare
{
    public class HealerPetHealth : PetHealth
    {
        private HealerPetBehaviour healerPetBehaviour;
        protected override void Awake ()
        {
            base.Awake ();
            healerPetBehaviour = GetComponent<HealerPetBehaviour>();
        }

        public override void Death()
        {
            base.Death();
            healerPetBehaviour.StopHeal();
        }
    }
}