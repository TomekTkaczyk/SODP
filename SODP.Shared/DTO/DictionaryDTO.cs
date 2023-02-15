using SODP.Shared.Enums;
using System.Collections.Generic;

namespace SODP.Shared.DTO
{
	public class DictionaryDTO : BaseDTO
	{
		public int? ParentId { get; set; }
		public string Sign { get; set; }
		public string Name { get; set; }
		public bool ActiveStatus { get; set; }
		public ICollection<DictionaryDTO> Children { get; set; }

		public DictionaryDTO(string sign = "", string name = "", bool active = true) 
		{
			Sign = sign;
			Name = name;
			ActiveStatus = active;
		}
    }
}
