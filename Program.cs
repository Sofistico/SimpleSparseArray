namespace SimpleSparseArray;

public class SparseSet : IEnumerable
{
	private int size;
	private readonly int[] dense;
	private readonly int[] sparse;

	public int Count => size;

	public SparseSet(int maxValue)
	{
		size = 0;
		dense = new int[maxValue];
		sparse = new int[maxValue];
	}

	public void Add(int value)
	{
		if (!Contains(value))
		{
			dense[size] = value;
			sparse[value] = size;
			size++;
		}
	}

	public void Remove(int value)
	{
		if (Contains(value))
		{
			dense[sparse[value]] = dense[size - 1];
			sparse[dense[size - 1]] = sparse[value];
			size--;
		}
	}

	public int Index(int value) => sparse[value];

	public bool Contains(int value)
	{
		return sparse[value] < size && dense[sparse[value]] == value;
	}

	public void Clear() => size = 0;

	public IEnumerator<int> GetEnumerator()
	{
		for (var i = 0; i < size; i++)
		{
			yield return dense[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public override bool Equals(object obj) => throw new Exception("Why are you comparing SparseSets?");

	public override int GetHashCode() => System.HashCode.Combine(size, dense, sparse, Count);
}