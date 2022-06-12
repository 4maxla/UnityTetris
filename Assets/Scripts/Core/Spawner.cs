using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	// masīvs ar visām figūrām
	public Shape[] m_allShapes;

	// Metode, kura pēc nejaušības principa izvēlas vienu no figūrām. Ja kāda no figūrām nebūs referencēta, tad tā tiks paziņota konsolē.
	Shape GetRandomShape() 
	{
		int i = Random.Range(0,m_allShapes.Length);
		if (m_allShapes[i])
		{
			return m_allShapes[i];
		}
		else
		{
			Debug.LogWarning("Invalid shape in spawner!");
			return null;
		}

	}

	// Atrod nejauši izvēlēto figūru, instantiģē figūru spawner objekta pozīcijā, bez rotācijām un identificē to kā figūru
	public Shape SpawnShape()
	{
		Shape shape = null;
		shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;

		if (shape)
		{
			return shape;
		}
		else
		{
			Debug.LogWarning("Invalid shape in spawner!");
			return null;
		}
	}

}
