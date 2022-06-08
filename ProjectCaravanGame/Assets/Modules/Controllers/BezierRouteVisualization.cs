using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectGeorge.Entities;

namespace ProjectGeorge.Controllers
{
    /// <summary>
    /// Visualize a Bezier route of degree <= 3
    /// It is technically a controller, but does not inherit MonoBehaviour
    /// </summary>
    public class BezierRouteVisualization
    {
        private BezierRoute route;
        public BezierRoute Route
        {
            get
            {
                return route;
            }
            set
            {
                route = value;
                UpdateVisual();
            }
        }

        private GameObject tokenPrefab;
        private List<GameObject> tokens = new List<GameObject>();

        const int PRECISION = 10;

        /// <summary>
        /// Do we display this visualization?
        /// </summary>
        public bool Active;

        /// <summary>
        /// Empty constructor that sets Route to null
        /// </summary>
        public BezierRouteVisualization(GameObject token)
        {
            Route = null;
            tokenPrefab = token;
        }

        public BezierRouteVisualization(BezierRoute route, GameObject token)
        {
            Route = route;
            tokenPrefab = token;
        }

        /// <summary>
        /// Place PRECISION# of tokens on the Bezier curve
        /// </summary>
        private void UpdateVisual()
        {
            if(Route != null && Active)
            {
                ClearTokens();
                for (float step = 0; step <= PRECISION; step++)
                {
                    float t = step / PRECISION;
                    Vector3 pos = Route.EvaluateAt(t);
                    GameObject newToken = GameObject.Instantiate(tokenPrefab, pos, Quaternion.identity);
                    tokens.Add(newToken);
                }
            }
        }

        /// <summary>
        /// Delete all existing tokens
        /// </summary>
        private void ClearTokens()
        {
            foreach(GameObject token in tokens)
            {
                GameObject.Destroy(token);
            }
            tokens = new List<GameObject>(PRECISION + 1);
        }
    }
}