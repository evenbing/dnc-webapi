using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
namespace MyRestfulAPI.Infrastucture.Cache
{
    public class RedisCache : ICache
    {
        private static string _redisConnectionString;

        private static ConnectionMultiplexer RedisConnection;
        private static readonly object MLock = new object();
        private IConfiguration _configuration;

        public RedisCache(IConfiguration configuration)
        {
            _configuration = configuration;
            if (string.IsNullOrEmpty(_redisConnectionString))
            {
                var redisConnectionString = _configuration["RedisConnectionString"];
                if (string.IsNullOrWhiteSpace(redisConnectionString))
                {
                    throw new ArgumentException("redis连接字符串未找到");
                }
                _redisConnectionString = redisConnectionString;
            }

        }
        /// <summary>
        /// redis 连接对象
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            if (RedisConnection != null && RedisConnection.IsConnected)
            {
                return RedisConnection;
            }

            lock (MLock)
            {
                if (RedisConnection != null && RedisConnection.IsConnected)
                {
                    return RedisConnection;
                }
                else
                {
                    if (RedisConnection !=null)
                    {
                        RedisConnection.Dispose();
                    }
                    RedisConnection = ConnectionMultiplexer.Connect(_redisConnectionString);
                }
            }

            return RedisConnection;

        }


        public bool Delete(string key)
        {
           return RedisConnection.GetDatabase().KeyDelete(key);
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public bool GetExists(string key)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool Set(string Key, byte[] value, TimeSpan ts)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value, TimeSpan ts)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, object value, TimeSpan ts)
        {
            throw new NotImplementedException();
        }
    }
}
