using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class TestFuelText
{
    private Rocket RocketTest;
    private TMP_Text fuelText;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Rocket"));
        GameObject gameObjectCanvas = Object.Instantiate(Resources.Load<GameObject>("Canvas"));
        RocketTest = gameObject.GetComponent<Rocket>();
        fuelText = gameObjectCanvas.GetComponentInChildren<TMP_Text>();

        RocketTest.energyText = fuelText;
        RocketTest.UpdateEnergyText();
    }

    //Test ui
    [UnityTest]
    public IEnumerator FuelUIChange()
    {
        string initialText = RocketTest.energyText.text;

        float timer = 0;
        while (timer < 0.1f)
        {
            RocketTest.Launch();
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Assert.AreNotEqual(initialText, RocketTest.energyText.text);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(RocketTest.gameObject);
        Object.Destroy(fuelText.transform.root.gameObject);
    }
}
