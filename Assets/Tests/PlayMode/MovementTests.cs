using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTests
{
    [UnityTest]
    public IEnumerator MoveLeft()
    {
        var gameObject = new GameObject();
        var shape = gameObject.AddComponent<Shape>();

        shape.MoveLeft();

        yield return null;

        Assert.AreEqual(expected: new Vector3(-1, 0, 0), actual: gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveRight()
    {
        var gameObject = new GameObject();
        var shape = gameObject.AddComponent<Shape>();

        shape.MoveRight();

        yield return null;

        Assert.AreEqual(expected: new Vector3(1, 0, 0), actual: gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveDown()
    {
        var gameObject = new GameObject();
        var shape = gameObject.AddComponent<Shape>();

        shape.MoveDown();

        yield return null;

        Assert.AreEqual(expected: new Vector3(0, -1, 0), actual: gameObject.transform.position);
    }

    [UnityTest]
    public IEnumerator MoveUp()
    {
        var gameObject = new GameObject();
        var shape = gameObject.AddComponent<Shape>();

        shape.MoveUp();

        yield return null;

        Assert.AreEqual(expected: new Vector3(0, 1, 0), actual: gameObject.transform.position);
    }
}
