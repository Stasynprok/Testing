using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Net.Sockets;
using System.Xml.Linq;

public class TestsRocket
{
    private Rocket RocketTest;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Rocket"));
        RocketTest = gameObject.GetComponent<Rocket>();
    }

    //Test move rocket
    [UnityTest]
    public IEnumerator RocketMoveUp()
    {
        float initialYPos = RocketTest.transform.position.y;
        float timer = 0;
        while (timer < 0.1f)
        {
            RocketTest.Launch();
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Assert.Less(initialYPos, RocketTest.transform.position.y);
    }

    [UnityTest]
    public IEnumerator RocketRotation()
    {
        float initialZRot = RocketTest.transform.rotation.z;
        float timer = 0;
        while (timer < 0.1f)
        {
            RocketTest.Launch();
            RocketTest.Rotation(true);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Assert.Less(initialZRot, RocketTest.transform.rotation.z);
    }
    
    [UnityTest]
    public IEnumerator RocketAddEnergy()
    {
        int currentEnergy = RocketTest.energyTotal;
        RocketTest.AddEnergy(10);
        yield return new WaitForFixedUpdate();
        Assert.Less(currentEnergy, RocketTest.energyTotal);
    }
    
    [UnityTest]
    public IEnumerator RocketLostEnergy()
    {
        int currentEnergy = RocketTest.energyTotal;
        RocketTest.Launch();
        yield return new WaitForFixedUpdate();
        Assert.Less(RocketTest.energyTotal, currentEnergy);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(RocketTest.gameObject);
    }
}
