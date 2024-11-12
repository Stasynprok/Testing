using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAudioRocket
{
    private Rocket RocketTest;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Rocket"));
        RocketTest = gameObject.GetComponent<Rocket>();
    }

    //Test audio play
    [UnityTest]
    public IEnumerator PlayAudio()
    {
        RocketTest.PlayAudio();
        yield return null;

        Assert.AreEqual(RocketTest._audioSource.isPlaying, true);
    }
    
    //Test ui
    [UnityTest]
    public IEnumerator StopAudio()
    {
        RocketTest.PlayAudio();
        yield return null;
        RocketTest.StopAudio();

        Assert.AreEqual(RocketTest._audioSource.isPlaying, false);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(RocketTest.gameObject);
    }
}
