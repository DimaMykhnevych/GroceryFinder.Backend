using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Exceptions;
using GroceryFinder.BusinessLayer.Extensions;
using GroceryFinder.BusinessLayer.Services.EmailService;
using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace GroceryFinder.BusinessLayer.Services.UserService;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public UserService(
        IMapper mapper,
        UserManager<AppUser> userManager,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<AppUser> GetUserByUsername(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<AppUser> CreateUserAsync(CreateUserDto model)
    {
        AppUser existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null)
        {
            throw new UsernameAlreadyTakenException();
        }

        AppUser user = _mapper.Map<AppUser>(model);
        user.RegistryDate = DateTime.Now;
        IdentityResult addUserResult = await _userManager.CreateAsync(user, model.Password);

        ValidateIdentityResult(addUserResult);

        if (_configuration.EmailConfirmationEnabled())
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", user.Email }
            };
            string url = QueryHelpers.AddQueryString(model.ClientURIForEmailConfirmation, param);
            await _emailService.SendAccountConfirmationEmail(user, url);
        }

        return await GetUserByUsername(user.UserName);
    }

    public async Task<ConfirmEmailDto> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
    {
        AppUser user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
        if (user == null)
            return null;
        var confirmResult = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
        if (!confirmResult.Succeeded)
            return null;
        return confirmEmailDto;
    }

    public async Task DeleteUser(Guid userId)
    {
        AppUser userToDelete = await _userManager.FindByIdAsync(userId.ToString());
        IdentityResult deleteUserResult = await _userManager.DeleteAsync(userToDelete);

        ValidateIdentityResult(deleteUserResult);
    }

    private void ValidateIdentityResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var errorsMessage = result.Errors
                                     .Select(er => er.Description)
                                     .Aggregate((i, j) => i + ";" + j);
            throw new Exception(errorsMessage);
        }
    }
}

