using NLog;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Configuration;
using Wp.Helpers.Helpers;

namespace Wp.Helpers.Redis
{
    /// <summary>
    /// ConnectionMultiplexer对象管理帮助类
    /// </summary>
    public class RedisCnnHelper
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        private static Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 系统自定义Key前缀
        /// </summary>
        public static readonly string SysCustomKey = ConfigurationManager.AppSettings["redisKey"] ?? "";

        private static readonly string RedisConnectionString = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString ?? "127.0.0.1:6379,allowadmin=true";

        private static readonly object Locker = new object();
        private static ConnectionMultiplexer _instance;
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// 单例获取
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!ConnectionCache.ContainsKey(connectionString))
            {
                ConnectionCache[connectionString] = GetManager(connectionString);
            }
            return ConnectionCache[connectionString];
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString ??= RedisConnectionString;
            var connect = ConnectionMultiplexer.Connect(connectionString);

            //注册如下事件
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;

            return connect;
        }

        #region 事件

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            _logger.Log(LogLevel.Info, $"Configuration changed:{e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.Log(LogLevel.Error, $"<br>ErrorMessage: {e.Message}<br>EndPoint:{e.EndPoint}");
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _logger.Log(LogLevel.Error, $"<br>ConnectionRestored:{e.EndPoint}<br>ExceptionDetail:{ExceptionHelper.GetExceptionDetailByWeb(e.Exception)}");
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _logger.Log(LogLevel.Info, $"<br>重新连接：Endpoint failed:{e.EndPoint},{e.FailureType }<br>ExceptionDetail:{ExceptionHelper.GetExceptionDetailByWeb(e.Exception)}");
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            _logger.Log(LogLevel.Info, $"<br>HashSlotMoved:NewEndPoint {e.NewEndPoint}, OldEndPoint {e.OldEndPoint}");
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            _logger.Log(LogLevel.Error, $"<br>InternalError:{ExceptionHelper.GetExceptionDetailByWeb(e.Exception)}");
        }

        #endregion 事件
    }
}