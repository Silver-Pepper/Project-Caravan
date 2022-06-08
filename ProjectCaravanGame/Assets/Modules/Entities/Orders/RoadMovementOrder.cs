using System;
using System.Collections.Generic;
using System.Linq;
using ProjectGeorge.Controllers;
using ProjectGeorge.Entities;
using UnityEngine;

namespace ProjectGeorge.Entities.Order
{
    class RoadMovementOrder : MerchantOrder
    {
        private City destinationCity;
        private Road road;
        private float progress;
        private float speed;

        /// <summary>
        /// Move merchant from currently docked city to the destination city on a road
        /// </summary>
        /// <param name="destinationCity"></param>
        /// <param name="road"></param>
        public RoadMovementOrder(Merchant merchant, City destinationCity, Road road, float speed) : base(merchant)
        {
            if(destinationCity != null && road != null)
            {
                this.destinationCity = destinationCity;
                this.road = road;
                progress = 0.0f;
                this.speed = speed;
                // For simplicity, the order can only be issued when the merchant is already docked in a city now
                // TODO: pick up from where we left and continue moving
                if (!merchant.IsDocked)
                {
                    throw new Exception("Failed to issue road movement order: merchant not docked to any city");
                }

                if(road.Data.From == merchant.DockedCity.Data && road.Data.To == destinationCity.Data)
                {
                    // We are clear
                }
                else
                {
                    Debug.LogError("The road does not connect docked city and destination city");
                    IsComplete = true;
                }
            }
            else
            {
                throw new Exception("RoadMovementOrder must apply to a valid city and a valid road");
            }
        }

        public override void EvaluateUpdate()
        {
            float step = speed * Time.deltaTime;
            progress += step;
            Mathf.Clamp(progress, 0.0f, 1.0f);
            if(progress >= 1.0f)
            {
                // Dock at destination, complete order
                merchant.DockedCity = destinationCity;
                IsComplete = true;
            }
            else
            {
                merchant.DockedCity = null;
                merchant.transform.position = road.EvaluateAt(progress);
            }
        }
    }
}
