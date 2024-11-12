using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class TestMoveBarrier
{
    private GameObject MoveBarrier;

    [SetUp]
    public void Setup()
    {
        MoveBarrier = Object.Instantiate(Resources.Load<GameObject>("MoveBarrier"));
    }

    [UnityTest]
    public IEnumerator TestMove()
    {
        float initialYPosotion = MoveBarrier.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(MoveBarrier.transform.position.y, initialYPosotion);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(MoveBarrier);
    }
}
