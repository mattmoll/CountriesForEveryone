﻿using CountriesForEveryone.Core.Exceptions;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CountriesForEveryone.Service
{
    public abstract class BaseService<T>
    {
        private readonly ILogger _logger;
        protected ILogger Logger => _logger;
        protected BaseService(ILogger<T> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        internal async Task<V> TryExecute<V>(Func<Task<V>> functionToExecute, [CallerMemberName] string memberName = "")
        {
            var signature = $"{GetType().Name}.{memberName}";
            using (Logger.BeginScope("{Method}", signature))
            {
                try
                {
                    return await functionToExecute();
                }
                catch (Exception ex)
                {
                    var message = $"{GetType().Name}.{memberName} => Exception: {ex.Message}";
                    _logger.LogError(ex, message);
                    throw;
                }
            }
        }

        internal async Task TryExecute(Func<Task> functionToExecute, [CallerMemberName] string memberName = "")
        {
            var signature = $"{GetType().Name}.{memberName}";
            using (Logger.BeginScope("{Method}", signature))
            {
                try
                {
                    await functionToExecute();
                }
                catch (Exception ex)
                {
                    var message = $"{GetType().Name}.{memberName} => Exception: {ex.Message}";
                    _logger.LogError(ex, message);
                    throw new CountriesForEveryoneException(message, ex);
                }
            }
        }
    }
}
