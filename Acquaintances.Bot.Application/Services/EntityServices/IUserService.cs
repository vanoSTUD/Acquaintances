﻿using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.Models;
using CSharpFunctionalExtensions;

namespace Acquaintances.Bot.Application.Services.EntityServices
{
	public interface IUserService
	{
		Task<Result> AddProfileAsync(AppUser user, TempProfile profile);
		Task<AppUser> GetOrCreateAsync(long chatId, CancellationToken ct = default);
		Task UpdateAsync(AppUser user, CancellationToken ct = default);
		Task SetStateAsync(AppUser user, State state, CancellationToken ct = default);
		Task SetTempProfileAsync(AppUser user, TempProfile profile, CancellationToken ct = default);
	}
}