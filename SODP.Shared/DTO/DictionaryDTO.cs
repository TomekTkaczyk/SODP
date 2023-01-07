using System.Collections.Generic;

namespace SODP.Shared.DTO
{
	public class DictionaryDTO : BaseDTO
	{
		public string Master { get; set; }
		public string Sign { get; set; }
		public string Name { get; set; }
		public bool ActiveStatus { get; set; }
		public ICollection<DictionaryDTO> Slaves { get; set; }
	}
}
