using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Net.Sockets;
using System.Xml.Linq;
using static Rocket;


public class TestsGameStatus
{
    private Rocket RocketTest;

    //Test finish status
    [UnityTest]
    public IEnumerator GameStatusFinish()
    {
        GameObject gameObjectFinish = Object.Instantiate(Resources.Load<GameObject>("Finish"));
        Vector3 finisPosition = -Vector3.up;
        gameObjectFinish.transform.position = finisPosition;

        GameObject gameObjectRocket = Object.Instantiate(Resources.Load<GameObject>("Rocket"));
        RocketTest = gameObjectRocket.GetComponent<Rocket>();
        Vector3 rocketPosition = Vector3.zero;
        gameObjectRocket.transform.position = rocketPosition;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(RocketTest.GetStatus(), GameState.NextLevel);
        Object.Destroy(RocketTest.gameObject);
        Object.Destroy(gameObjectFinish);
    }
    
    //Test dead status
    [UnityTest]
    public IEnumerator GameStatusDead()
    {
        GameObject gameObjectRocket = Object.Instantiate(Resources.Load<GameObject>("Rocket"));
        RocketTest = gameObjectRocket.GetComponent<Rocket>();
        Vector3 rocketPosition = Vector3.zero;
        gameObjectRocket.transform.position = rocketPosition;

        GameObject gameObjectDead = Object.Instantiate(Resources.Load<GameObject>("Barrier"));
        Vector3 deadPosition = Vector3.up * 5;
        gameObjectDead.transform.position = deadPosition;

        float timer = 0;
        while (timer < 0.1f)
        {
            RocketTest.Launch();
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Assert.AreEqual(RocketTest.GetStatus(), GameState.Dead);

        Object.Destroy(RocketTest.gameObject);
        Object.Destroy(gameObjectDead);
    }
}
