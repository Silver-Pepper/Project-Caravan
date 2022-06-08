using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectGeorge.Entities
{
    /// <summary>
    /// Represents a Bezier route with degree <= 3
    /// The implementation follows closely with the recursive definition
    /// </summary>
    public class BezierRoute
    {
        private Vector3[] controlPoints;
        /// <summary>
        /// The degree refers to the degree of polynomial
        /// controlPoints.Length == degree + 1
        /// </summary>
        private int degree;

        public BezierRoute(Vector3[] controlPoints, int degree)
        {
            if(controlPoints.Length != degree + 1)
            {
                throw new Exception("Failed to initalize Bezier curve : control points array size doesn't match degree");
            }
            else
            {
                if(degree > 3)
                {
                    Debug.LogError("Current architecture only accept Bezier curve of degree 3 or less");
                    // In fact, our implementation works for arbitrary degree, so we may keep executing
                    // TODO: refactor this part to prevent unexpected behaviour OR go with arbitrary solution
                }
                this.controlPoints = controlPoints;
                this.degree = degree;
            }
        }

        /// <summary>
        /// Evaluate at a certain time value
        /// </summary>
        /// <returns>The position at time t</returns>
        public Vector3 EvaluateAt(float t)
        {
            // First check that 0 <= t <= 1
            if(t>=0 && t<= 1)
            {
                if (degree == 0)
                {
                    return controlPoints[0];
                }
                else
                {
                    // Set up recursion

                    Vector3[] b1Control = new Vector3[degree];
                    Array.Copy(controlPoints, b1Control, degree); // P0 to P(n-1)
                    Vector3[] b2Control = new Vector3[degree];
                    Array.Copy(controlPoints, 1, b2Control, 0, degree); // P1 to P(n)

                    BezierRoute b1 = new BezierRoute(b1Control, degree - 1);
                    BezierRoute b2 = new BezierRoute(b2Control, degree - 1);

                    Vector3 output = (1 - t) * b1.EvaluateAt(t) + t * b2.EvaluateAt(t);
                    return output;
                }
            }
            else
            {
                throw new Exception("Failed to evaluate Bezier curve: parameter out of bounds");
            }
        }

        /// <summary>
        /// Testing function, delete later
        /// </summary>
        public Vector3 CubicEvaluateAt(float t)
        {
            Vector3 output =
                Mathf.Pow(1 - t, 3) * controlPoints[0] + 
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] + 
                3 * Mathf.Pow(t, 2) * (1 - t) * controlPoints[3] + 
                Mathf.Pow(t,3) * controlPoints[3];
            return output;
        }
    }
}