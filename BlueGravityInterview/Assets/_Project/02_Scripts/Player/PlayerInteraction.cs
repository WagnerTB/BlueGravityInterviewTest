using System;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField]
        private float _interactionRange;

        [SerializeField]
        private LayerMask _interactionMask;

        [SerializeField]
        private bool _showGizmos = false;

        private IInteractable _currentInteraction;

        private List<IInteractable> _interactionsNearby = new List<IInteractable>(3);
        private List<IInteractable> _lastInteractionsNearby = new List<IInteractable>(3);
        private float closestDistance = 99;
        public bool IsInteracting => _isInteracting;
        private bool _isInteracting;

        public void Interact()
        {
            if (_currentInteraction != null)
            {
                _currentInteraction.BeginInteract();
                _currentInteraction = null;
                _isInteracting = true;
            }
        }

        public void EndInteraction()
        {
            _isInteracting = false;
        }

        public void CheckInteractions()
        {
            closestDistance = 99;
            foreach (var interaction in _interactionsNearby)
            {
                _lastInteractionsNearby.Add(interaction);
            }

            _interactionsNearby.Clear();
            _currentInteraction = null;


            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _interactionRange, _interactionMask, 0);

            for (int i = 0; i < colliders.Length; i++)
            {
                IInteractable interaction = colliders[i].GetComponent<IInteractable>();
                if (interaction != null && interaction.CanInteract())
                {
                    if (!_lastInteractionsNearby.Contains(interaction))
                    {
                        interaction.EnterInRange();
                    }
                    else
                    {
                        _lastInteractionsNearby.Remove(interaction);
                    }

                    var dist = transform.position - interaction.GetTransform().position;
                    if (dist.sqrMagnitude < closestDistance * closestDistance)
                    {
                        closestDistance = dist.sqrMagnitude;
                        _currentInteraction = interaction;
                        _interactionsNearby.Insert(0, interaction);
                    }
                    else
                    {
                        _interactionsNearby.Add(interaction);
                    }
                }
            }

            foreach (var interaction in _lastInteractionsNearby)
            {
                interaction.ExitRange();
            }

            _lastInteractionsNearby.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            if (_showGizmos)
            {
               Gizmos.color = Color.red;
               Gizmos.DrawWireSphere(transform.position,_interactionRange);
            }
        }
    }
}