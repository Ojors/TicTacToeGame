using System;
using System.Collections;
using System.Collections.Generic;
using Data;

namespace Game.Model
{
	public struct Move
	{
		public int row, col;

		public Move(int row = 0, int col = 0)
		{
			this.row = row;
			this.col = col;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Move))
			{
				return false;
			}
        
			Move otherMove = (Move) obj;
			return row == otherMove.row && col == otherMove.col;
		}
	}

	public class Board : IDictionary<int, int[]>
	{
		private int[][] m_Data;
		private List<Move> m_EmptyMove = new List<Move>();

		public int Count => Define.BOARD_SIZE * Define.BOARD_SIZE;
		public bool IsReadOnly => false;
		public ICollection<int[]> Values => m_Data;

		public Board()
		{
			Init();
		}

		private void Init()
		{
			m_Data = new int[Define.BOARD_SIZE][];
			for (int i = 0; i < Define.BOARD_SIZE; i++)
			{
				m_Data[i] = new int[Define.BOARD_SIZE];
			}

			m_EmptyMove.Clear();
			for (int i = 0; i < Define.BOARD_SIZE; i++)
			{
				for (int j = 0; j < Define.BOARD_SIZE; j++)
				{
					m_EmptyMove.Add(new Move(i, j));
				}
			}
		}

		public void DoMove(int row, int col, int value)
		{
			if (IsLegal(row, col))
			{
				m_Data[row][col] = value;
				RemoveEmptyMove(row, col);
			}
		}

		private bool IsLegal(int row, int col)
		{
			return row >= 0 && row < Define.BOARD_SIZE && col >= 0 && col < Define.BOARD_SIZE;
		}

		private void RemoveEmptyMove(int row, int col)
		{
			for (int i = 0; i < m_EmptyMove.Count; i++)
			{
				Move m = m_EmptyMove[i];
				if (m.row == row && m.col == col)
				{
					m_EmptyMove.RemoveAt(i);
					return;
				}
			}
		}

		public List<Move> GetEmptyMoves()
		{
			return m_EmptyMove;
		}

		public void Clear()
		{
			Init();
		}

		public bool Contains(KeyValuePair<int, int[]> item)
		{
			if (IsLegal(item.Key, 0) && item.Value.Length == m_Data[item.Key].Length)
			{
				int[] rowData = m_Data[item.Key];
				for (int i = 0; i < rowData.Length; i++)
				{
					if (rowData[i] != item.Value[i])
						return false;
				}
				return true;
			}
			return false;
		}
		
		public bool TryGetValue(int col, out int[] rowData)
		{
			if (ContainsKey(col))
			{
				rowData = m_Data[col];
				return true;
			}
			rowData = null;
			return false;
		}

		public ICollection<int> Keys
		{
			get
			{
				int[] cols = new int[Define.BOARD_SIZE];
				for (int i = 0; i < Define.BOARD_SIZE; i++)
				{
					cols[i] = i;
				}
				return cols;
			}
		}
		
		public bool ContainsKey(int row)
		{
			return row >= 0 && row < Define.BOARD_SIZE;
		}
		
		public int[] this[int row]
		{
			get => IsLegal(row, 0) ? m_Data[row] : null;
			set
			{
				if (IsLegal(row, 0))
				{
					m_Data[row] = value;
				}
				else
				{
					throw new ArgumentException("Row data is Illegal.");
				}
			}
		}

		public void Add(KeyValuePair<int, int[]> item)
		{
			throw new InvalidOperationException("Illegal Operation.");
		}

		public void CopyTo(KeyValuePair<int, int[]>[] array, int arrayIndex)
		{
			throw new InvalidOperationException("Illegal Operation.");
		}

		public bool Remove(KeyValuePair<int, int[]> item)
		{
			throw new InvalidOperationException("Illegal Operation.");
		}

		public void Add(int key, int[] value)
		{
			throw new InvalidOperationException("Illegal Operation.");
		}
		

		public bool Remove(int key)
		{
			throw new InvalidOperationException("Illegal Operation.");
		}

		public IEnumerator<KeyValuePair<int, int[]>> GetEnumerator()
		{
			throw new InvalidOperationException("Illegal Operation.");
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}