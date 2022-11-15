using System;
using System.Diagnostics;
using System.IO;
using EasyCaching.Core.Configurations;
using FASTER.core;
using Microsoft.Extensions.Logging;

namespace EasyCaching.FasterKv.Configurations
{
    /// <summary>
    /// FasterKvCachingOptions
    /// for details, see https://microsoft.github.io/FASTER/docs/fasterkv-basics/#fasterkvsettings
    /// </summary>
    public class FasterKvCachingOptions : BaseProviderOptions
    {
        /// <summary>
        /// FasterKv index count
        /// Each bucket is 64 bits. So this define 65536 keys.
        /// Used 4MB memory
        /// </summary>
        public long IndexCount { get; set; } = 65536;

        /// <summary>
        /// FasterKv used memory size (default: 16MB)
        /// </summary>
        public int MemorySizeBit { get; set; } = 24;

        /// <summary>
        /// FasterKv page size (default: 1MB) 
        /// </summary>
        public int PageSizeBit { get; set; } = 20;

        /// <summary>
        /// FasterKv read cache used memory size (default: 16MB)
        /// </summary>
        public int ReadCacheMemorySizeBit { get; set; } = 24;

        /// <summary>
        /// FasterKv read cache page size (default: 16MB)
        /// </summary>
        public int ReadCachePageSizeBit { get; set; } = 20;
        
        /// <summary>
        /// FasterKv commit logs path
        /// </summary>
        public string LogPath { get; set; } = Path.Combine(Environment.CurrentDirectory, $"EasyCaching-FasterKv-{Process.GetCurrentProcess().Id}");
        
        /// <summary>
        /// Set Custom Store
        /// </summary>
        public FasterKV<SpanByte, SpanByte>? CustomStore { get; set; }

        internal LogSettings GetLogSettings(string name)
        {
            return new LogSettings
            {
                LogDevice = Devices.CreateLogDevice(Path.Combine(LogPath, name),
                    preallocateFile: true,
                    deleteOnClose: true),
                PageSizeBits = PageSizeBit,
                MemorySizeBits = MemorySizeBit,
                ReadCacheSettings = new ReadCacheSettings
                {
                    MemorySizeBits = ReadCacheMemorySizeBit,
                    PageSizeBits = ReadCachePageSizeBit,
                }
            };
        }
    }
}