using UnityEngine;
using System.Collections;


public class Board : MonoBehaviour
{

	// Tukšais kvadrāts, kas tiks instaiģēts, lai uzzimētu laukumu
	public Transform m_emptySprite;

	// laukuma augstums
	public int m_height = 30;

	// laukuma platums
	public int m_width = 10;

	// rindas, kurām nevajag zīmēties no augšas
	public int m_header = 8;

	// glabātuve neaktīvajām figūrām
	Transform[,] m_grid;


	void Awake()
	{
		m_grid = new Transform[m_width, m_height];
	}

	void Start()
	{
		DrawEmptyCells();
	}

	// Update is called once per frame
	void Update()
	{

	}

	bool IsWithinBoard(int x, int y) // Metode, kas pārbauda vai figūras kvadrāta elements atrodas uz laukuma.
	{
		return (x >= 0 && x < m_width && y >= 0);

	}

	bool IsOccupied(int x, int y, Shape shape) // Metode, kas pārbauda vai vieta ir aizņemta noteiktā pozīcijā uz laukuma.
	{

		return (m_grid[x, y] != null && m_grid[x, y].parent != shape.transform);
	}

	public bool IsValidPosition(Shape shape) // Metode, kas iziet cauri visiem figūras kvadrāta elementiem un pārbauda vai kvadrāts elements atrodas uz laukuma.
	{
		foreach (Transform child in shape.transform)
		{
			Vector2 pos = Vectorf.Round(child.position);

			if (!IsWithinBoard((int)pos.x, (int)pos.y))
			{
				return false;
			}

			if (IsOccupied((int)pos.x, (int)pos.y, shape))
			{
				return false;
			}
		}
		return true;
	}

	// Uzzīmē tukšo laukumu no tukšajiem kvadrātiem
	void DrawEmptyCells()
	{
		if (m_emptySprite)
		{
			for (int y = 0; y < m_height - m_header; y++)
			{
				for (int x = 0; x < m_width; x++)
				{
					Transform clone;
					clone = Instantiate(m_emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;

					// tukšo kvadrātu objekta nosaukumi hiarhijā
					clone.name = "Board Space ( x = " + x.ToString() + " , y =" + y.ToString() + " )";

					// noperento visus tukšos kvadrātus zem Board objekta
					clone.transform.parent = transform;
				}
			}
		}
	}

	public void StoreShapeInGrid(Shape shape) // Metode, kas saglabā figūras pozīciju uz laukuma.
	{
		if (shape == null) // Figūras eksistēšanas pārbaude
		{
			return;
		}

		foreach (Transform child in shape.transform)
		{
			Vector2 pos = Vectorf.Round(child.position); // Atrodam noapoļotās x un y kordinātes.
			m_grid[(int)pos.x, (int)pos.y] = child; // Saglabjām atrastās kordinātes uz laukuma.
		}
	}

	bool IsComplete(int y)
	{
		for (int x = 0; x < m_width; ++x)
		{
			if (m_grid[x, y] == null)
			{
				return false;
			}

		}
		return true;
	}

	void ClearRow(int y)
	{
		for (int x = 0; x < m_width; ++x)
		{
			if (m_grid[x, y] != null)
			{
				Destroy(m_grid[x, y].gameObject);

			}
			m_grid[x, y] = null;

		}

	}

	void ShiftOneRowDown(int y)
	{

		for (int x = 0; x < m_width; ++x)
		{
			if (m_grid[x, y] != null)
			{
				m_grid[x, y - 1] = m_grid[x, y];
				m_grid[x, y] = null;
				m_grid[x, y - 1].position += new Vector3(0, -1, 0);
			}
		}
	}

	void ShiftRowsDown(int startY)
	{
		for (int i = startY; i < m_height; ++i)
		{
			ShiftOneRowDown(i);
		}
	}

	public void ClearAllRows()
	{
		for (int y = 0; y < m_height; ++y)
		{
			if (IsComplete(y))
			{
				ClearRow(y);

				ShiftRowsDown(y + 1);

				y--;
			}

		}

	}

	public bool IsOverLimit(Shape shape)
	{
		foreach (Transform child in shape.transform)
		{
			if (child.transform.position.y >= m_height - m_header)
			{
				return true;
			}
		}
		return false;
	}


}
