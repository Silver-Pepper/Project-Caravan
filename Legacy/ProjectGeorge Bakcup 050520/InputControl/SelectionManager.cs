using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectGeorge.Controllers;
using ProjectGeorge.Entities.Order;
using ProjectGeorge.InputControl;

namespace ProjectGeorge.InputControl
{
    // TODO: factor out the movement dispatch part
    class SelectionManager
    {
        /// <summary>
        /// Current selection of merchants
        /// </summary>
        private List<Merchant> selection;

        /// <summary>
        /// Click manager bound to LMB by default
        /// to handle selection
        /// </summary>
        private ClickManager selectionClickManager;

        /// <summary>
        /// Click manager bound to RMB by default
        /// to dispatch movement
        /// </summary>
        private ClickManager movementDispatcher;

        /// <summary>
        /// Click manager bound to Shift + RMB by default
        /// to dispatch movement order enqueued to the order queue
        /// </summary>
        private ClickManager movementEnqueueDispatcher;

        private Vector3 _mouseStart;
        private Vector3 _mouseEnd;
        /// <summary>
        /// If dragging is active
        /// </summary>
        private bool _active;

        /// <summary>
        /// A list of all merchants
        /// </summary>
        public ICollection<Merchant> AllMerchants { get;}
            = new List<Merchant>();

        public void Awake()
        {
            selection = new List<Merchant>();
            selectionClickManager = new ClickManager(0, StartBoxSelection, OnSelectShortClick, EndDrag, UpdateBoxSelection);
            movementDispatcher = new ClickManager(
                1, NullMethod, DispatchMovementOrder, NullMethod, NullMethod
                );
            movementEnqueueDispatcher = new ClickManager(
                1, NullMethod, DispatchEnqueueMovementOrder, NullMethod, NullMethod, true
                );
        }

        public void Update()
        {
            selectionClickManager.Update();
            movementDispatcher.Update();
            movementEnqueueDispatcher.Update();
        }

        private void StartBoxSelection()
        {
            _mouseStart = Input.mousePosition;
            _active = false;
        }

        private void UpdateBoxSelection()
        {
            _mouseEnd = Input.mousePosition;
            UpdateSelection(false);
            _active = true;
        }

        private void EndDrag()
        {
            _active = false;
            UpdateSelection(true);
        }

        private void UnselectAll(List<Merchant> selection)
        {
            foreach(var merchant in selection)
            {
                merchant.IsSelected = false;
            }
            selection.Clear();
        }

        private void SetSelected(List<Merchant> selection)
        {
            foreach(Merchant merchant in selection)
            {
                merchant.IsSelected = true;
            }
        }

        private void OnSelectShortClick()
        {
            UnselectAll(selection);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Selectable"), QueryTriggerInteraction.Ignore))
            {
                GameObject go = hit.transform.gameObject;
                Merchant selectable = go.GetComponent<Merchant>();

                if (selectable != null)
                {
                    var selectedMerchant = selectable;
                    selectable.IsSelected = true;
                    selection.Add(selectedMerchant);
                }
            }
        }

        private void UpdateSelection(bool finalizeSelection)
        {
            List<Merchant> newSelection = AllMerchants.Where(x => IsInside(x)).ToList();

            // Obsolete, TODO: REMOVE
            if (!Input.GetKey(KeyCode.LeftShift) && selection != null && selection.Count != 0)
            {
                List<Merchant> old = selection.Except(newSelection).ToList();
                UnselectAll(old);
            }
            SetSelected(newSelection);
            selection = newSelection;
        }

        private bool IsInside(Merchant merchant)
        {
            bool inside = false;
            inside = IsInside(merchant.transform.position);
            
            return inside;
        }

        /// <summary>
        /// If a position vector is within the rectangle with vertices _mouseStart and _mouseEnd and sides parallel to axis
        /// </summary>
        private bool IsInside(Vector3 targetPosition)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
            bool insideX = (screenPosition.x - _mouseStart.x) * (screenPosition.x - _mouseEnd.x) < 0;
            bool insideY = (screenPosition.y - _mouseStart.y) * (screenPosition.y - _mouseEnd.y) < 0;
            return insideX && insideY;
        }

        public void RegisterMerchantBirth(Merchant merchant)
        {
            AllMerchants.Add(merchant);
        }

        public void RegisterMerchantDeath(Merchant merchant)
        {
            AllMerchants.Remove(merchant);
        }

        public void DispatchMovementOrder()
        {
            DispatchToHitPoint(false);
        }


        public void DispatchEnqueueMovementOrder()
        {
            DispatchToHitPoint(true);
        }

        private void DispatchToHitPoint(bool enqueueMode)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Terrain"), QueryTriggerInteraction.Ignore))
            {
                // Free movement on terrain
                DispatchMovementUponDestination(hit.point, enqueueMode);
            }
            else if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Cities"), QueryTriggerInteraction.Ignore))
            {
                // Move to a city
                DispatchMovementUponDestination(hit.collider.gameObject, enqueueMode);
            }
        }

        public void NullMethod()
        {
        }

        private void DispatchMovementUponDestination(Vector3 destination, bool enqueue)
        {
            foreach (Merchant merchant in selection)
            {
                if (enqueue)
                {
                    merchant.EnqueueOrder(new MovementOrder(merchant, destination, 5.0f));
                    Debug.Log("enq");
                }
                else
                {
                    merchant.SwitchOrder(new MovementOrder(merchant, destination, 5.0f));
                }
            }
        }

        private void DispatchMovementUponDestination(GameObject destination, bool enqueue)
        {
            foreach(Merchant merchant in selection)
            {
                if (enqueue)
                {
                    merchant.EnqueueOrder(new MovementOrder(merchant, destination, 5.0f));
                }
                else
                {
                    merchant.SwitchOrder(new MovementOrder(merchant, destination, 5.0f));
                }
            }
        }
    }
}