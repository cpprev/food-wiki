namespace Trie.DataModel
{
	public class TrieDescription
	{
		public char Key { get; set; }

		public bool IsWord { get; set; } = false;

		public TrieDescription[] Children { get; set; } = new TrieDescription[TrieConstants.NbCharSupported];

		/// <summary>
		/// Copy ctor
		/// </summary>
		/// <param name="trie"></param>
		public TrieDescription(TrieDescription trie)
		{
			if (trie == null)
				return;
			Key = trie.Key;
			IsWord = trie.IsWord;
			for (var i = 0; i < trie.Children.Length; ++i)
			{
				Children[i] = new TrieDescription(trie.Children[i]);
			}
		}

		public TrieDescription() { }
	}
}
