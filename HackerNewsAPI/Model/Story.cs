namespace HackerNewsAPI.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Technically, Story is a DTO
	/// </summary>
	public class Story
	{
		[Required]
		public int Id { get; set; }
		public string By { get; set; }
        public bool Dead { get; set; }
        public bool Deleted { get; set; }
        public int Descendants { get; set; }
		public IList<int> Kids { get; set; }
        public IList<int> Parts { get; set; }
		public int Parent { get; set; }
        public int Poll { get; set; }
        public int Score { get; set; }
		public long Time { get; set; }
		public string Text { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
		public string Url { get; set; }
	}
}
