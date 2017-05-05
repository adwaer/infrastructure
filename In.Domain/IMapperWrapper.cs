namespace In.Domain
{
    public interface IMapperWrapper
    {
        TReturn Map<TReturn>(object src);

        TReturn Map<TReturn>(object src, TReturn dest);
    }
}