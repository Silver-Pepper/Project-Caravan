using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ProjectGeorge.Controllers;

namespace ProjectGeorge.InputControl
{
    public class InputManager : MonoBehaviour
    {
        private SelectionManager _selectionManager;
        
        public void Awake()
        {
            _selectionManager = new SelectionManager();
            _selectionManager.Awake();
        }

        // Update is called once per frame
        void Update()
        {
            _selectionManager.Update();
        }

        public void RegisterMerchantBirth(Merchant merchant)
        {
            _selectionManager.RegisterMerchantBirth(merchant);
        }

        public void RegisterMerchantDeath(Merchant merchant)
        {
            _selectionManager.RegisterMerchantDeath(merchant);
        }
    }
}
