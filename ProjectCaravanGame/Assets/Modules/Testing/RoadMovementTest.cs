using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectGeorge.Entities;
using ProjectGeorge.Controllers;
using ProjectGeorge.Entities.Order;

class RoadMovementTest : MonoBehaviour
{
    public City city1;
    public City city2;
    public City city3;
    public Road road;
    public Road road2;
    public Merchant merchant;

    private void Start()
    {
        InitializeTest();
        merchant.EnqueueOrder(new RoadMovementOrder(merchant, city2, road, 0.5f));
        merchant.EnqueueOrder(new RoadMovementOrder(merchant, city3, road2, 0.5f));
    }

    private void InitializeTest()
    {
        merchant.DockedCity = city1;

        // Prepare the road data
        Vector3[] ctpts = { new Vector3(0, 0, 0), new Vector3(2, 0, 2), new Vector3(4, 0, 0) };
        BezierRoute r = new BezierRoute(ctpts, 2);
        List<BezierRoute> segments = new List<BezierRoute>();
        segments.Add(r);

        Vector3[] ctpts2 = { new Vector3(4, 0, 0), new Vector3(0, 0, -4), new Vector3(10, 0, 2) };
        BezierRoute r2 = new BezierRoute(ctpts2, 2);
        List<BezierRoute> segments2 = new List<BezierRoute>();
        segments2.Add(r2);
        RoadData testdata = new RoadData(city1.Data, city2.Data, 1.0f, "from Burgerland to George's sex dungeon",segments);
        RoadData testdata2 = new RoadData(city2.Data, city3.Data, 1.0f, "  ", segments2);

        road.Data = testdata;
        road2.Data = testdata2;
    }
}
