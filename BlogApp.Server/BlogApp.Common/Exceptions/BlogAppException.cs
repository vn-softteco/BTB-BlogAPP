namespace BlogApp.Common.Exceptions
{
	public sealed class BlogAppException : Exception
	{
		public BlogAppException(string message) : base(message)
		{
		}

		public BlogAppException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
