using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectGeorge.Entities;
using ProjectGeorge.Entities.Order;

namespace ProjectGeorge.Controllers
{
    public class Merchant : MonoBehaviour
    {
        private Queue<MerchantOrder> orderQ = new Queue<MerchantOrder>();
        private MerchantOrder currentOrder;
        private MerchantData data;

        public Text MerchantInfo;
        public Image cityPicture;

        /// <summary>
        /// Contains the material when unselected at 0, and material when selected at 1
        /// </summary>
        public Material[] SelectionMaterials;

        private bool selected;
        public bool IsSelected
        {
            get
            {
                return selected;
            }
            set
            {
                ApplySelectionMaterial(value);
                if (!value)
                {
                    ClearUI();
                }
                selected = value;
            }
        }

        /// <summary>
        /// Apply the material of the merchant according to whether it is selected
        /// </summary>
        private void ApplySelectionMaterial(bool s)
        {
            if (s)
            {
                gameObject.GetComponent<Renderer>().material = SelectionMaterials[1];
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = SelectionMaterials[0];
            }
        }

        private City docked;
        /// <summary>
        /// Currently docked city
        /// </summary>
        public City DockedCity
        {
            get
            {
                return docked;
            }
            set
            {
                if(value != null)
                {
                    IsDocked = true;
                    cityPicture.enabled = true;
                    cityPicture.sprite = value.cityPic;
                }
                else
                {
                    cityPicture.enabled = false;
                    IsDocked = false;
                }
                docked = value;
            }
        }

        public void CloseCityPicture()
        {
            cityPicture.enabled = false;
        }

        public bool IsDocked
        {
            get;
            private set;
        }

        // Start is called before the first frame update
        void Start()
        {
            data = new MerchantData();
            currentOrder = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsSelected)
            {
                UpdateUI();
            }
            if (currentOrder == null)
            {
                // Issue next order
                if (orderQ.Count != 0)
                {
                    currentOrder = orderQ.Dequeue();
                }
            }
            else
            {
                if (currentOrder.IsComplete == true)
                {
                    // Order complete, kill current order
                    currentOrder = null;
                }
                else
                {
                    // Continue execution
                    currentOrder.EvaluateUpdate();
                }
            }
        }

        private void UpdateUI()
        {
            MerchantInfo.text =
                "Name: " + data.name + "\n" +
                "Money: " + data.money + "\n" +
                "Slaves:" + data.slaves;
        }

        private void ClearUI()
        {
            MerchantInfo.text = "No merchant selected";
        }

        /// <summary>
        /// Add an order to the order queue
        /// </summary>
        public void EnqueueOrder(MerchantOrder order)
        {
            orderQ.Enqueue(order);
        }

        /// <summary>
        /// Discard the order queue and switch to a new order
        /// </summary>
        public void SwitchOrder(MerchantOrder order)
        {
            orderQ.Clear();
            currentOrder = order;
        }

        /// <summary>
        /// Discard the order queue and stop immediately
        /// </summary>
        public void Stop()
        {
            orderQ.Clear();
            currentOrder = null;
        }
    }
}