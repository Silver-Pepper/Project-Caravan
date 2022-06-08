using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectGeorge.Controllers;

namespace ProjectGeorge.Entities.Order
{
    /// <summary>
    /// Represents a generic order issued to a merchant
    /// Order applies to the merchant once it is evaluated
    /// </summary>
    public abstract class MerchantOrder
    {
        protected Merchant merchant;
        public bool IsComplete
        {
            get;
            protected set;
        }

        public MerchantOrder(Merchant merchant)
        {
            if (merchant != null)
            {
                this.merchant = merchant;
                IsComplete = false;
            }
            else
            {
                throw new System.Exception("Merchant order must be applied to a valid merchant");
            }
        }

        /// <summary>
        /// The EvaluateUpdate is called once per frame until the order is complete
        /// </summary>
        public abstract void EvaluateUpdate();
    }
}