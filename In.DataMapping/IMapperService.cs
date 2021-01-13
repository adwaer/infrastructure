namespace In.DataMapping
{
    public interface IMapperService
    {
        TDest GetFrom<TFrom, TDest>(TFrom model);
    }
}