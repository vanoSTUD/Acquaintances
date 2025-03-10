﻿using Acquaintances.Bot.Application.Abstractions;
using Acquaintances.Bot.Application.Helpers;
using Acquaintances.Bot.Domain.Entities;
using Acquaintances.Bot.Domain.Enums;
using Acquaintances.Bot.Domain.Models;
using CSharpFunctionalExtensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Acquaintances.Bot.Application.Services.EntityServices;

public class UserService : IUserService
{
	private readonly IUsersRepository _userRepository;
	private readonly IRepository<Profile> _profileRepository;

	public UserService(IUsersRepository userRepository, IRepository<Profile> profileRepository)
	{
		_userRepository = userRepository;
		_profileRepository = profileRepository;
	}

	public async Task<AppUser> GetOrCreateAsync(long chatId, CancellationToken ct = default)
	{
		var foundUser = await _userRepository.GetAsync(chatId, ct);

		if (foundUser != null)
			return foundUser;

		return await _userRepository.CreateAsync(AppUser.Create(chatId), ct);
	}

	public async Task<Result> AddProfileAsync(AppUser user, TempProfile profile, CancellationToken ct = default)
	{
		try
		{
			var profileResult = Profile.Create(
				user.ChatId,
				profile.Name!,
				profile.Description!,
				profile.City!,
				profile.Age!,
				profile.Gender!,
				profile.PreferredGender!,
				profile.Photos!
			);

			if (profileResult.IsFailure)
			{
				return Result.Failure($"Ошибка! {profileResult.Error}. \nПопробуй {CommandNames.Start}");
			}

			await _profileRepository.CreateAsync(profileResult.Value, ct);
			var oldProfile = user.Profile;
			user.SetProfile(profileResult.Value);

			await _userRepository.UpdateAsync(user, ct);

			if (oldProfile != null)
			{
				await _profileRepository.RemoveAsync(oldProfile, ct);
			}

			return Result.Success();
		}
		catch (Exception)
		{
			return Result.Failure($"Ошибка! \nПопробуй {CommandNames.Start}");
		}
	}

	public async Task UpdateAsync(AppUser user, CancellationToken ct = default)
	{
		await _userRepository.UpdateAsync(user, ct);
	}

	public async Task SetStateAndUpdateAsync(AppUser user, UserStates state, CancellationToken ct = default)
	{
		user.SetState(state);
		await _userRepository.UpdateAsync(user, ct);
	}

	public async Task SetTempProfileAsync(AppUser user, TempProfile? profile, CancellationToken ct = default)
	{
		user.SetTempProfile(profile);
		await _userRepository.UpdateAsync(user, ct);
	}

	public async Task ClearTempProfileAsync(AppUser user, CancellationToken ct = default)
	{
		user.SetTempProfile(null);
		await _userRepository.UpdateAsync(user, ct);
	}
}
