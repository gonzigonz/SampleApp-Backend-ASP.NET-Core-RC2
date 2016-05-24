using System;

namespace Gonzigonz.SampleApp.Domain
{
	public class EntityBase : IEntityBase<int>
	{
		public int Id { get; set; }
		public DateTime CreatedTimeUTC { get; set; }
		public DateTime ModifiedTimeUTC { get; set; }
	}
}
