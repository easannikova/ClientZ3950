using ZClient.Abstract;

namespace ZClient.Logic
{
    // ReSharper disable once InconsistentNaming
	public class CQLQuery : ICQLQuery
	{
		public CQLQuery(string query)
		{
			QueryString = query;
		}

		public string QueryString { get; set; }
	}
}
