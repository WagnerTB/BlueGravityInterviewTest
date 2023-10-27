using System;
using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        public void BeginInteract();
        public void EndInteract();
        public bool CanInteract();

        public void EnterInRange();
        public void ExitRange();

        public Transform GetTransform();
    }
}