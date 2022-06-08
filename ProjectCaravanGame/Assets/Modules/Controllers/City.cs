using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectGeorge.Entities;
using ProjectGeorge.Entities.Graph;

namespace ProjectGeorge.Controllers
{
    public class City : MonoBehaviour
    {
        public CityData Data
        {
            get;
            private set;
        }

        [SerializeField]
        public Sprite cityPic;
    }
}
