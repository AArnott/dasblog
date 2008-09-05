using System;

namespace newtelligence.DasBlog.Web.Core {
	/// <summary>
	/// Represents the statistics for this blog.
	/// </summary>
	internal class BlogStatistics {
	
		/// <summary>
		/// Creates a new instance of the <see cref="BlogStatistics" /> class.
		/// </summary>
		public BlogStatistics() {
			//...
		}

		// PROPERTIES

		private int allEntriesCount;

		public int AllEntriesCount{
			get{
				return this.allEntriesCount;
			}
			set{
				this.allEntriesCount = value;
			}
		}

		private int commentCount;

		public int CommentCount{
			get{
				return this.commentCount;
			}
			set{
				this.commentCount = value;
			}
		}

		private int monthPostCount;
	
		public int MonthPostCount {
			get{
				return this.monthPostCount;
			}
			set{
				this.monthPostCount = value;
			}
		}

		private int weekPostCount;

		public int WeekPostCount {
			get{
				return this.weekPostCount;
			}
			set{
				this.weekPostCount = value;
			}
		}

		private int yearPostCount;
		
		public int YearPostCount {
			get{
				return this.yearPostCount;
			}
			set{
				this.yearPostCount = value;
			}
		}
	}
}

