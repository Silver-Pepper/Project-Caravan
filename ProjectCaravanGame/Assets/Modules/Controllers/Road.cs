using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ProjectGeorge.Entities;
using ProjectGeorge.Entities.Graph;

namespace ProjectGeorge.Controllers
{
    class Road : MonoBehaviour
    {
        private RoadData data;
        /// <summary>
        /// Road data entity
        /// </summary>
        public RoadData Data
        {
            get
            {
                return data;
            }
            set
            {
                
                segmentsVisual = new List<BezierRouteVisualization>();
                // Initialize visuals for each segment
                foreach(BezierRoute seg in value.Segments)
                {
                    segmentsVisual.Add(new BezierRouteVisualization(seg, DebugToken));
                }
                
                data = value;
            }
        }

        public GameObject DebugToken;

        /// <summary>
        /// Visual representations of road segments
        /// </summary>
        private List<BezierRouteVisualization> segmentsVisual = new List<BezierRouteVisualization>();

        /// <summary>
        /// Compute the position when at a certain progress on the road
        /// </summary>
        /// <param name="progress">A value between 0 and 1 indicating current progress</param>
        /// <returns>The position on the road</returns>
        public Vector3 EvaluateAt(float progress)
        {
            //TODO: take weight into consideration


            // If the parameter is wrong, crush loud and clear
            if(progress < 0 || progress > 1)
            {
                throw new Exception("Road.EvaluateAt() failed: Parameter out of bounds");
            }
            if(Data.Segments.Count == 0)
            {
                throw new Exception("Road.EvaluateAt() failed: No segment to travel on");
            }

            int onSegment;
            float t;
            onSegment = Mathf.FloorToInt(Data.Segments.Count * progress); // Determine on which segment
            t = (progress - onSegment / Data.Segments.Count) * Data.Segments.Count; // Compute t on that specific segment

            // Then use Bezier routes to compute position
            return Data.Segments[onSegment].EvaluateAt(t);
        }
    }
}
