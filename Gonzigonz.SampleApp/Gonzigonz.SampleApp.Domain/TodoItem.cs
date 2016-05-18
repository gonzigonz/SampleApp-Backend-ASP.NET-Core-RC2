namespace Gonzigonz.SampleApp.Domain
{
	public class TodoItem : EntityBase
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsComplete { get; set; }
	}
}
