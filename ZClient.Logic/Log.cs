namespace ZClient.Logic
{
    public class Log
    {
        public static void Init(int level, string prefix, string name)
        {
            Yaz.yaz_log_init(level, prefix, name);
        }
    }
}
