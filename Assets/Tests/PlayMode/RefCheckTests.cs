using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RefCheckTests
{
    [UnityTest]
    public IEnumerator ReCheckBoard()
    {
        var m_gameBoard = GameObject.FindWithTag("Board");

        Assert.IsNull(m_gameBoard);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ReCheckSpawner()
    {
        var m_spawner = GameObject.FindWithTag("Spawner");

        Assert.IsNull(m_spawner);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ReCheckScoreManager()
    {
        var m_scoreManager = GameObject.FindWithTag("ScoreManager");

        Assert.IsNull(m_scoreManager);

        yield return null;
    }
}
