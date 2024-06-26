﻿using GroceryFinder.BusinessLayer.Constants;
using Microsoft.Extensions.Configuration;

namespace GroceryFinder.BusinessLayer.Extensions;

public static class ConfigurationExtensions
{
    public static bool EmailConfirmationEnabled(this IConfiguration configuration)
    {
        return bool.TryParse(configuration[ConfigurationKeys.EmailConfirmationEnabled], out bool result) && result;
    }

    public static bool PriceUpdateEmailNotificationEnabled(this IConfiguration configuration)
    {
        return bool.TryParse(configuration[ConfigurationKeys.PriceUpdateEmailNotificationEnabled], out bool result) && result;
    }
}

