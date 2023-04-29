
namespace Common
{
    public enum EventCode : byte
    {
        /// <summary>
        /// 新的用户添加进来
        /// </summary>
        NewPlayer,
        /// <summary>
        /// 在线用户的位置信息
        /// </summary>
        PlayerPosition,
        /// <summary>
        /// 用户离开了
        /// </summary>
        ClosePlayer
    }
}
