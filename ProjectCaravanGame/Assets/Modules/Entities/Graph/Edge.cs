using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGeorge.Entities.Graph
{
    public abstract class Edge
    {
        public Vertex From
        {
            get;
            protected set;
        }

        public Vertex To
        {
            get;
            protected set;
        }

        public float Weight
        {
            get;
            protected set;
        }

        public Edge(Vertex f, Vertex t, float w)
        {
            From = f;
            To = t;
            Weight = w;
        }
    }
}