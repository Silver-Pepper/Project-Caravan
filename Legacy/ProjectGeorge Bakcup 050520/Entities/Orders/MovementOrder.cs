using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ProjectGeorge.Controllers;


namespace ProjectGeorge.Entities.Order
{
    class MovementOrder : MerchantOrder
    {
        private Vector3 destinationPos;
        private float speed;

        public MovementOrder(Merchant merchant, Vector3 destination, float speed) : base(merchant)
        {
            destinationPos = destination;
            this.speed = speed;
        }

        public MovementOrder(Merchant merchant, GameObject destination, float speed) : base(merchant)
        {
            destinationPos = destination.transform.position;
            this.speed = speed;
        }

        public override void EvaluateUpdate()
        {
            // For now we move the merchant directly towards destination
            float step = speed * Time.deltaTime;
            merchant.transform.position = Vector3.MoveTowards(merchant.transform.position, destinationPos, step);
            if (merchant.transform.position == destinationPos)
            {
                IsComplete = true;
            }
        }
    }
}