using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGeorge.Entities.Graph
{
    public abstract class Vertex
    {
        public string ID
        {
            get;
            protected set;
        }

        public Vertex(string id)
        {
            ID = id;
        }
    }
}