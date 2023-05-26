﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace RedisDistributedCaching.Helper
{
    public static class DistributedCacheExtensions
    {


        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
        {

            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
            return cache.SetAsync(key, bytes, DistributedCacheOptions.GetDistributedCacheEntryOptions());





        }

        public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
        {
            var val = cache.Get(key);
            value = default;

            if (val == null) return false;

            value = JsonSerializer.Deserialize<T>(val, GetJsonSerializerOptions());

            return true;
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }
    }
}
