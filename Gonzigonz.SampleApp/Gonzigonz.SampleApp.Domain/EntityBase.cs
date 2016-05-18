using System;

namespace Gonzigonz.SampleApp.Domain
{
	public class EntityBase
    {
		public int Id { get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime ModifiedTime { get; set; }
	}
}
