using UnityEngine;

namespace Interaction
{
    public class Chest : MonoBehaviour , IInteractable
    {
        [SerializeField]
        private Animator _anim;
        
        private bool _canLoot = true;
        private static readonly int Open = Animator.StringToHash("Open");

        public void BeginInteract()
        {
            _canLoot = false;
            _anim.SetTrigger(Open);
        }

        public void EndInteract()
        {
        }

        public bool CanInteract()
        {
            return _canLoot;
        }

        public void EnterInRange()
        {
        }

        public void ExitRange()
        {
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}