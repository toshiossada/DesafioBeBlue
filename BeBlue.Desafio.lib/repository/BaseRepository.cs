namespace BeBlue.Desafio.lib.repository
{
    public class BaseRepository
    {
        protected readonly string _connetionString;

        public BaseRepository(string connetionString)
        {
            _connetionString = connetionString;
        }
    }
}