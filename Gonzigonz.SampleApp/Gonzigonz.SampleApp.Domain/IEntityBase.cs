using System;

namespace Gonzigonz.SampleApp.Domain
{
	public interface IEntityBase<T> where T : struct
    {
		T Id { get; set; }
		DateTime CreatedTimeUTC { get; set; }
		DateTime ModifiedTimeUTC { get; set; }
    }
}
