namespace In.DataMapping
{
    public interface IMapperService
    {
        TDest GetFrom<TDest, TFrom>(TFrom model);
    }
}