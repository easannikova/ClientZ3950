using ZClient.Abstract;

namespace ZClient.Logic
{
	public class CQLQuery : ICQLQuery
	{
		public CQLQuery(string query)
		{
			QueryString = query;
		}

		public string QueryString { get; set; }
	}
}
