using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.ValueObjects.Profile;

namespace Acquaintances.Bot.Domain.Models;

public class TempProfile
{
	public TempProfile()
	{
	}

	public Name? Name { get; set; }
	public Description? Description { get; set; }
	public City? City { get; set; }
	public Age? Age { get; set; }
	public Gender? Gender { get; set; }
	public Gender? PreferredGender { get; set; }
	public List<Photo>? Photos { get; set; }

}
