using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Infrastucture.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 根据Key获取缓存实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 根据key获取缓存字符串数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 根据key判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool GetExists(string key);

        /// <summary>
        /// 根据Key删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string key);

        /// <summary>
        /// 设置byte[]类型缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set(string key, byte[] value);

        /// <summary>
        /// 设置byte[]类型缓存,并设置缓存过期时间
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        bool Set(string Key, byte[] value, TimeSpan ts);

        /// <summary>
        /// 设置T类型缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value);

        /// <summary>
        /// 设置T类型缓存并设置缓存过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, TimeSpan ts);

        /// <summary>
        /// 设置object类型缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set<T>(string key, object value);

        /// <summary>
        /// 设置object类型缓存并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        bool Set<T>(string key, object value, TimeSpan ts);
    }
}
